
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using NToastNotify;
using VatFramework.Models;
using VatFramework.Models.DataObjects.ApplicationRole;
using VatFramework.Models.DataObjects.Permission;
using VatFramework.Models.Domains.Account;
using VatFramework.Models.Domains.Permission;
using VatFramework.Services.Contract.EntityService;
using VatFramework.Services.Contract.ErrorLogger;
using VatFramework.Services.Handler.DataAccess;
using VatFramework.Utilities.ExceptionUtility;
using VatFramework.Web.Controllers;
using VatFramework.Web.Filters;
using VatFramework.Web.Models;
using static VatFramework.Web.Models.Util;

namespace VatFramework.Web.Areas.Admin.Controllers
{

    [SessionTimeout]
    [Area("Admin")]
    public class RoleManagementController : BaseController
    {
        private IRole _roleManager;
        private readonly ILogger _logger;
        private readonly APPContext _context;
        private readonly IErrorLogger _errorLogger;
        private readonly IToastNotification _toastNotification;
        private readonly IPermission _permissionManager;
        public RoleManagementController(IRole roleManager, ILogger<RoleManagementController> logger,
            APPContext context, IErrorLogger errorLogger, IToastNotification toastNotification, IPermission permissionManager)
        {
            _roleManager = roleManager;
            _logger = logger;
            _context = context;
            _errorLogger = errorLogger;
            _toastNotification = toastNotification;
            _permissionManager = permissionManager;
        }

        [AuthorizedAction]
        [HttpGet("RoleManagement")]
        public async Task<IActionResult> Index()
        {

            try
            {
                ViewBag.ControllerName = GetControllerName(this);
                ListViewBagParams();

                var result = await _roleManager.GetRoles(null);



                if (result.Where(a => a.DataIntegrity == ResponseErrorMessageUtility.DataIntegrity_OK).Count() > 0)
                {

                    //_toastNotification.AddInfoToastMessage(ResponseErrorMessageUtility.RecordFetched, null);
                    return View(result);
                }
                else
                {

                    _toastNotification.AddInfoToastMessage(ResponseErrorMessageUtility.RecordNotFound, null);
                    result.Clear();
                    return View(result);
                }

            }
            catch (Exception ex)
            {
                return systemError(ex);

            }


        }




        #region Create Operation
        [HttpGet("RoleManagement/Create")]

        public IActionResult Create()
        {
            ViewBag.ControllerName = GetControllerName(this);
            try
            {
                PreloadData();
                CreateViewBagParams();

                return PartialView("_PartialAddEdit");
            }
            catch (Exception ex)
            {
                return systemError(ex);
            }
        }


        [HttpPost("RoleManagement/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationRoleViewModel model, string Controller, string Action)
        {
            try
            {

                ViewBag.ControllerName = GetControllerName(this);
                //Check the model state of the Request
                if (!ModelState.IsValid)
                {
                    PreloadData();
                    ListViewBagParams();
                    _toastNotification.AddWarningToastMessage(ResponseErrorMessageUtility.RequiredFields, null);
                    return PartialView("_PartialAddEdit", model);
                }

                model.CreatedBy = CurrentUser.UserId;

                var response = await _roleManager.CreateRole(model, getController(), getAction());


                if (!string.IsNullOrEmpty(response) && response != ResponseErrorMessageUtility.SuccessfulAndUpdated)
                {

                    _toastNotification.AddWarningToastMessage(response, null);
                    return RedirectToAction(nameof(Index));
                }

                _toastNotification.AddSuccessToastMessage(ResponseErrorMessageUtility.succesful, null);

                ModelState.Clear();


                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return systemError(ex);
            }
        }


        #endregion


        #region Edit Operation


        [HttpPost("RoleManagement/Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationRoleViewModel model)
        {
            ViewBag.ControllerName = GetControllerName(this);
            //Check the model state of the Request
            if (!ModelState.IsValid)
            {
                EditViewBagParams();
                PreloadData();

                return PartialView("_PartialAddEdit", model);
            }


            //pass the values to be created to the servicedel
            model.LastModified = DateTime.Now;
            model.CreatedBy = CurrentUser.UserId;
            model.IsSysAdmin = false;
            model.Name = model.RoleName;
            model.ModifiedBy = CurrentUser.UserId;
            model.ParentRoleId = model.ParentRoleId;

            var response = await _roleManager.UpdateRole(model, getController(), getAction());



            if (!string.IsNullOrEmpty(response) && response != ResponseErrorMessageUtility.SuccessfulAndUpdated)
            {
                _toastNotification.AddWarningToastMessage(response, null);
                return RedirectToAction(nameof(Index));
            }


            _toastNotification.AddSuccessToastMessage(ResponseErrorMessageUtility.UpdateRecord, null);

            ModelState.Clear();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost("RoleManagement/Update")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(string RoleId, List<string> roles, string userid, string Controller, string ActionName)
        {

            try
            {
                if (roles == null)
                {
                    _toastNotification.AddWarningToastMessage("Select at least one permission!", null);
                }



                var resp = await _permissionManager.UpdateRolePermission(RoleId, roles, CurrentUser.UserId, getController(), getAction());

                if (!string.IsNullOrEmpty(resp) && resp != ResponseErrorMessageUtility.SuccessfulAndUpdated)
                {

                    _toastNotification.AddWarningToastMessage(ResponseErrorMessageUtility.RecordNotSaved, null);
                    return RedirectToAction(nameof(Index));
                }

                _toastNotification.AddSuccessToastMessage(ResponseErrorMessageUtility.UpdateRecord, null);
                ModelState.Clear();

                return RedirectToAction(nameof(Index));
           


            }
            catch (Exception ex)
            {
                return systemError(ex);
            }

        }



        // GET: ApplicationRoles/Edit/5
        [HttpGet("RoleManagement/AssignPermission")]
        public async Task<ActionResult> AssignPermission(string ID)
        {

            try
            {
                ViewBag.ControllerName = GetControllerName(this);
                EditViewBagParams();
                var applicationRole = await _roleManager.GetRoleById(ID);

                if (applicationRole == null)
                {
                    //TempData["MESSAGE"] = new AlertMessage { Message = "Role not found.", MessageType = MessageType.ErrorMessage };
                    return RedirectToAction("Index");
                }


                var listPermission = await _permissionManager.GetAllPermissions(true);
                var model = new ApplicationRolePermissionViewModel
                {
                    Id = applicationRole.ID,
                    Name = applicationRole.RoleName,
                    Permissions = GetPermissions(),
                    AllowedPermissionList = _context.Permissions.ToList().Where(a => a.IsActive == true).ToList(),
                    RolePermission = _context.RolePermissions.ToList().Where(c => c.RoleId == ID).ToList(),
                    SelectedPermissionId = _context.RolePermissions.ToList().Where(c => c.RoleId == ID).Select(c => c.PermissionId).ToList(),
                    RoleName = applicationRole.RoleName,
                    RoleDescription = applicationRole.RoleDescription,
                    IsActive = applicationRole.IsActive

                };

                var item = UpdateRoleViewModel(model);

                //return View(item);
                return PartialView("AssignPermission", item);

            }
            catch (Exception ex)
            {
                return systemError(ex);
            }
        }


        private IEnumerable<SelectListItem> GetPermissions()
        {


            var types = _context.Permissions.ToList().Select(x =>
                                  new SelectListItem
                                  {
                                      Value = x.ID.ToString(),
                                      Text = x.PermissionName

                                  }).AsEnumerable();
            return new SelectList(types, "Value", "Text");
        }

        [HttpGet("RoleManagement/Edit")]

        public async Task<IActionResult> Edit(string Id)
        {
            ViewBag.ControllerName = GetControllerName(this);
            try
            {
                EditViewBagParams();

                var response = await _roleManager.GetRoleById(Id);

                PreloadData();

                return PartialView("_PartialAddEdit", response);
            }
            catch (Exception ex)
            {
                return systemError(ex);
            }
        }
        #endregion

        #region Delete Operation
        [HttpGet("RoleManagement/Delete")]
        public async Task<ActionResult> Delete(string Id)
        {
            ViewBag.ControllerName = GetControllerName(this);
            try
            {
                DeleteViewBagParams();

                var response = await _roleManager.GetRoleById(Id);
                return PartialView("Delete", response);
            }
            catch (Exception ex)
            {

                return systemError(ex);

            }


        }

        public static List<NameAndValueObject> SystemRoles()
        {
            return Enum.GetValues(typeof(AppRole)).Cast<AppRole>().Select(x => new NameAndValueObject
            {
                Name = x.ToString().Replace('_', ' '),
                Id = (int)x
            }).ToList();
        }


        [HttpPost("RoleManagement/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([Bind("ID")] ApplicationRoleViewModel model)
        {
            ViewBag.ControllerName = GetControllerName(this);
            if (!ModelState.IsValid)
            {
                DeleteViewBagParams();

                _toastNotification.AddWarningToastMessage(ResponseErrorMessageUtility.RecordNotFound, null);
                return RedirectToAction(nameof(Index));
            }
            string ModifiedBy = CurrentUser.UserId;
            bool response = await _roleManager.DeleteRole(model.ID, ModifiedBy, getController(), getAction());


            if (response == false)
            {

                _toastNotification.AddWarningToastMessage(ResponseErrorMessageUtility.DeleteOperationFail, null);
                ModelState.Clear();
                return RedirectToAction(nameof(Index));
            }



            _toastNotification.AddSuccessToastMessage(ResponseErrorMessageUtility.DeleteOperation, null);
            ModelState.Clear();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Helpers Operation
        public RedirectToActionResult systemError(Exception ex)
        {
            _errorLogger.LogError(ex, GetControllerAndActionName(this));
            _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.OperationFailed_Ex, null);

            ModelState.Clear();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Helper

        /// <summary>
        /// This method is used for getting all other details for ApplicationRoleViewModel
        /// </summary>
        /// <returns></returns>
        private ApplicationRolePermissionViewModel UpdateRoleViewModel(ApplicationRolePermissionViewModel model)
        {

            var systRoles = Util.SystemRoles().ToList();

            List<string> menusId = _context.RolePermissions.ToList().Where(x => x.RoleId == model.Id).Select(p => p.PermissionId.ToString()).ToList();
            var menus = _context.Permissions.ToList().Where(slt => slt.PermissionName != "Permissions").ToList();
            var parentMenus = menus.Where(x => x.ParentId == 0).ToList();

            #region Permissions

            var sb = new StringBuilder();
            string unorderedList = GenerateUrl(parentMenus, menus, sb, menusId);

            #endregion

            model.Editable = false;
            model.UnorderedList = unorderedList;
            model.ParentMenus = parentMenus;
            return model;
        }

        private string GenerateUrl(List<Permission> parentMenus, List<Permission> menus, StringBuilder sb, List<string> menusId)
        {
            if (parentMenus.Count > 0)
            {
                foreach (var menu in parentMenus)
                {

                    // Get all Menu Components into variables:
                    string id = menu.ID.ToString();
                    string handler = menu.Url;
                    string menuText = menu.PermissionName;
                    string icon = menu.Icon;

                    var pid = menu.ID;
                    var parentId = menu.ParentId;

                    string status = menusId.Contains(id) ? "Checked" : "";

                    var subMenus = menus.FindAll(x => x.ParentId == pid);
                    if (subMenus.Count > 0 && !pid.Equals(parentId))
                    {
                        string line = string.Format(@"<li class=""has""><input type=""checkbox"" class=""custom - control - input bg - primary"" name=""MyPermissionIds[]"" id=""{5}"" value=""{1}"" {4}>
                            <input type=""hidden"" id=""{5}"" />  
                            <label>> {1}</label>",
                            handler, menuText, icon, "target", status, id);
                        sb.Append(line);

                        var subMenuBuilder = new StringBuilder();
                        sb.AppendLine(string.Format(@"<ul>"));
                        sb.Append(GenerateUrl(subMenus, menus, subMenuBuilder, menusId));
                        sb.Append("</ul>");
                    }
                    else
                    {
                        string line = string.Format(@"<li class=""""><input type=""checkbox"" name=""MyPermissionIds[]"" id=""{5}"" value=""{1}"" {4}>
                        <input type=""hidden"" id=""{5}"" />  
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<label>{1}</label>",
                        handler, menuText, icon, "target", status, id);
                        sb.Append(line);
                    }
                    sb.Append("</li>");
                }
            }
            return sb.ToString();
        }


        #endregion


        #region Preload Data

        public void PreloadData()
        {
            ViewBag.AdministratorList = _context.Roles.Where(a => a.IsSysAdmin == true).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),

            }).ToList();

        }
        #endregion


    }

}