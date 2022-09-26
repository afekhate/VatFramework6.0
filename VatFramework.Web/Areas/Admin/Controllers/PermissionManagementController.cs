using System;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using NToastNotify;
using VatFramework.Models;
using VatFramework.Services.Contract.EntityService;
using VatFramework.Services.Contract.ErrorLogger;
using VatFramework.Utilities.ExceptionUtility;
using VatFramework.Models.DataObjects.Permission;
using VatFramework.Web.Filters;
using VatFramework.Web.Controllers;

namespace VatFramework.Web.Areas.Admin.Controllers
{
    [SessionTimeout]
    [Area("Admin")]


    public class PermissionManagementController : BaseController
    {
        private IPermission _permissionManager;
        private readonly ILogger _logger;
        private readonly APPContext _context;
        private readonly IErrorLogger _errorLogger;
        private readonly IToastNotification _toastNotification;
        private readonly IIconService _iconManager;

        public PermissionManagementController(IPermission permissionManager, ILogger<PermissionManagementController> logger,
            APPContext context, IErrorLogger errorLogger, IToastNotification toastNotification, IIconService iconManager)
        {
            _permissionManager = permissionManager;
            _logger = logger;
            _context = context;
            _errorLogger = errorLogger;
            _toastNotification = toastNotification;
            _iconManager = iconManager;
        }

        [AuthorizedAction]
        [HttpGet("PermissionManagement")]
        public async Task<IActionResult> Index()
        {


            try
            {
                ViewBag.ControllerName = GetControllerName(this);
                ListViewBagParams();

                var result = await _permissionManager.GetAllPermissions(null);



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
        [HttpGet("PermissionManagement/Create")]

        public async Task<IActionResult> Create()
        {
            ViewBag.ControllerName = GetControllerName(this);
            try
            {

                CreateViewBagParams();
                var response = await _permissionManager.GetAllParent(true);

                var iconrespinse = await _iconManager.GetIcons(true);

                ViewBag.IconList = iconrespinse.ToList().Select(u => new SelectListItem { Text = u.IconName, Value = u.ID.ToString() });


                return PartialView("_PartialAddEdit", new PermissionViewModel
                {
                    ParentMenus = response.Select(u => new SelectListItem { Text = u.PermissionName, Value = u.ID.ToString() }).ToList(),
                });
            }
            catch (Exception ex)
            {
                return systemError(ex);
            }
        }


        [HttpPost("PermissionManagement/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PermissionViewModel model)
        {
            try
            {

                ViewBag.ControllerName = GetControllerName(this);
                //Check the model state of the Request
                if (!ModelState.IsValid)
                {

                    ListViewBagParams();
                    _toastNotification.AddWarningToastMessage(ResponseErrorMessageUtility.RequiredFields, null);
                    return PartialView("_PartialAddEdit", model);
                }

                model.CreatedBy = CurrentUser.UserId;
                model.IsActive = true;

                var response = await _permissionManager.CreatePermission(model, getController(), getAction());


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
        [HttpPost("PermissionManagement/Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PermissionViewModel model)
        {
            ViewBag.ControllerName = GetControllerName(this);
            //Check the model state of the Request
            if (!ModelState.IsValid)
            {
                EditViewBagParams();


                return PartialView("_PartialAddEdit", model);
            }


            //pass the values to be created to the servicedel
            model.LastModified = DateTime.Now;
            model.CreatedBy = CurrentUser.UserId;


            var response = await _permissionManager.UpdatePermission(model, getController(), getAction());



            if (!string.IsNullOrEmpty(response) && response != ResponseErrorMessageUtility.SuccessfulAndUpdated)
            {
                _toastNotification.AddWarningToastMessage(response, null);
                return RedirectToAction(nameof(Index));
            }


            _toastNotification.AddSuccessToastMessage(ResponseErrorMessageUtility.UpdateRecord, null);

            ModelState.Clear();

            return RedirectToAction(nameof(Index));
        }




        [HttpGet("PermissionManagement/Edit")]

        public async Task<IActionResult> Edit(long Id)
        {
            ViewBag.ControllerName = GetControllerName(this);
            try
            {
                EditViewBagParams();


                var response = await _permissionManager.GetPermissionByID(Id);



                var iconrespinse = await _iconManager.GetIcons(true);

                ViewBag.IconList = iconrespinse.ToList().Select(u => new SelectListItem { Text = u.IconName, Value = u.ID.ToString() });

                return PartialView("_PartialAddEdit", response);
            }
            catch (Exception ex)
            {
                return systemError(ex);
            }
        }
        #endregion

        #region Delete Operation
        [HttpGet("PermissionManagement/Delete")]
        public async Task<ActionResult> Delete(long Id)
        {
            ViewBag.ControllerName = GetControllerName(this);
            try
            {
                DeleteViewBagParams();

                var response = await _permissionManager.GetPermissionByID(Id);
                return PartialView("Delete", response);
            }
            catch (Exception ex)
            {

                return systemError(ex);

            }


        }


        [HttpPost("PermissionManagement/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([Bind("ID", "PermissionName", "PermissionCode", "Icon", "ParentId", "Url")] PermissionViewModel model)
        {
            ViewBag.ControllerName = GetControllerName(this);
            //FIX THIS LATER  //if (!ModelState.IsValid)
            //{
            //    DeleteViewBagParams();

            //    _toastNotification.AddWarningToastMessage(ResponseErrorMessageUtility.RecordNotFound, null);
            //    return RedirectToAction(nameof(Index));
            //}
            string ModifiedBy = CurrentUser.UserId;
            bool response = await _permissionManager.DeletePermission(model.ID, ModifiedBy, getController(), getAction());


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
    }

}