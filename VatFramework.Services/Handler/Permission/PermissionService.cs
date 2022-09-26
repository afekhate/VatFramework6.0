
using VatFramework.Models;
using VatFramework.Services.Contract;
using VatFramework.Utilities.AuthenticationUtility.AuthUser;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using VatFramework.Utilities.ExceptionUtility;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

using VatFramework.Services.Contract.EntityService;
using VatFramework.Models.DataObjects.ApplicationRole;
using VatFramework.Models.DataObjects.Permission;
using VatFramework.Models.Domains.Permission;
using Microsoft.AspNetCore.Mvc.Rendering;
using VatFramework.Models.Domains.Account;
using System.Transactions;
using VatFramework.Services.Handler.DataAccess;
using System.Data;

namespace VatFramework.Services.Handler.Permission
{

    public class PermissionServices : IPermission
    {
        private readonly APPContext _context;
        private readonly IConfiguration _config;
        private readonly ILogger<PermissionServices> _logger;
        private readonly IActivityLog _activityRepo;

       
       

        public PermissionServices(APPContext context,
          IAuthUser authUser, ILogger<PermissionServices> logger, IActivityLog activityRepo)
        {
            _context = context;
            _logger = logger;
            _activityRepo = activityRepo;

        }

     
        public async Task<string> CreatePermission(PermissionViewModel obj,string Controller, string ActionName)
        {
            string status = "";
            try
            {

                var inComing = obj.PermissionName.Trim();
                var parentId = obj.ParentId.GetValueOrDefault();
                var url = obj.Url.Trim();

                string msg = string.Empty;
                var roleExists = new List<Models.Domains.Permission.Permission>();


                var permissions = await _context.Permissions.ToListAsync();


                var check = permissions.Where(s => s.PermissionName.Trim() == inComing).ToList();

                if (check.Count() > 0)
                {
                    roleExists = check.Where(x => x.ParentId == parentId).ToList();
                    if (roleExists.Count > 0)
                    {
                        status = string.Format(ResponseErrorMessageUtility.RecordExistBefore, obj.PermissionName);
                        return status;
                    }
                }

                var check2 = permissions.Where(x => x.Url.Trim() == url).ToList();


                if (check2.Count() > 0)
                {
                    msg = "Url for Permission " + obj.PermissionName + " already exist";
                    status = string.Format(msg, obj.PermissionName);
                    return status;
                }

                var checkexist = await _context.Permissions
                    .AnyAsync(x => x.PermissionName.ToUpper() == obj.PermissionName.ToUpper()
                   );

                if (checkexist == false)
                {
                    var  newrecord = new VatFramework.Models.Domains.Permission.Permission
                    {
                        PermissionName = inComing,
                        Url = url,
                        ParentId = parentId,
                        Icon = (string.IsNullOrEmpty(obj.Icon) ? "" : obj.Icon.Trim()),
                        CreatedBy = obj.CreatedBy,
                        IsActive = obj.IsActive,
                        PermissionCode = obj.PermissionCode,
                        CreatedDate = DateTime.Now,
                        IPAddress = VatFramework.Utilities.Common.GetLocalIPAddress(),
                      
                    };

                    _context.Permissions.Add(newrecord);

                    if (await _context.SaveChangesAsync() > 0)
                    {
                        _activityRepo.CreateActivityLog(string.Format("Created Portal permission with Name : {0}", newrecord.PermissionName),
                            Controller, ActionName, obj.CreatedBy, newrecord);

                        status = ResponseErrorMessageUtility.RecordSaved;
                        return status;
                    }
                    else
                    {
                        status = ResponseErrorMessageUtility.RecordNotSaved;
                        return status;
                    }
                }
                status = string.Format(ResponseErrorMessageUtility.RecordExistBefore, obj.PermissionName.ToUpper());
                return status;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }
        }





        public async Task<string> UpdateRolePermission(string RoleId, List<string> roles, string UserId, string Controller,string ActionName)
        {
            string status = "";
            var rolePermissions = new List<VatFramework.Models.Domains.Account.RolePermission>();

            try
            {


                foreach (var role in roles)
                {
                    var rolePermission = new Models.Domains.Account.RolePermission
                    {
                        PermissionId = Convert.ToInt64(role),
                        RoleId = RoleId
                    };
                    rolePermissions.Add(rolePermission);
                }

              
                // call a store procedure
                DBManager dBManager = new DBManager(_context);
                var parameters = new List<IDbDataParameter>();
                parameters.Add(dBManager.CreateParameter("@RoleId", RoleId, DbType.AnsiString));
                AccessDataLayer.access dbManager = new AccessDataLayer.access(_context);
                dbManager.DeletePermissionByRoleID(parameters.ToArray(), "DeletePermissionByRoleID");


              
                _context.RolePermissions.AddRange(rolePermissions);

                if (await _context.SaveChangesAsync() > 0)
                {
                    string RoleName = _context.Roles.Where(a => a.Id == RoleId).FirstOrDefault().RoleName;

                    string permissionName = "";

                    foreach (var item in rolePermissions)
                    {
                        var parametersList = new List<IDbDataParameter>();
                        parametersList.Add(dBManager.CreateParameter("@permissionId", item.ID, DbType.Int64));

                        DataTable dt = dBManager.GetDataTable("getPermission", CommandType.StoredProcedure, parametersList.ToArray());

                        if (dt.Rows.Count > 0)
                        {
                            permissionName = permissionName + dt.Rows[0]["PermissionName"].ToString() + "|";
                        }

                    }

                    _activityRepo.CreateActivityLog(string.Format("Mapped Permission to Role  Portal : {0}", RoleName), Controller, ActionName, UserId, permissionName);

                    status = ResponseErrorMessageUtility.RecordSaved;
                }
                else
                {
                    status = ResponseErrorMessageUtility.RecordNotSaved;


                }

                


                return status;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }
        }
        public async Task<string> UpdatePermission(PermissionViewModel obj, string Controller, string ActionName)
        {
            string status = "";
            try
            {
                var inComing = obj.PermissionName.Trim().ToLower();

                var permissions = _context.Permissions.ToList();


                var check = permissions.Where(s => s.PermissionName.Trim().ToLower() == inComing && s.ID != obj.ID && 
                s.ParentId == obj.ParentId && obj.ParentId != 0).ToList();

                if (check.Count > 0)
                {
                    string msg = "Permission " + obj.PermissionName + " already exist";

                    status = string.Format(msg, obj.PermissionName);
                    return status;
                }



                var permissionmodel = await _context.Permissions.FirstOrDefaultAsync(x => x.ID == obj.ID);

                permissionmodel.PermissionName = obj.PermissionName.Trim();
                permissionmodel.Url = obj.Url.Trim();
                permissionmodel.ParentId = obj.ParentId.GetValueOrDefault();
                permissionmodel.Icon = (string.IsNullOrEmpty(obj.Icon) ? "" : obj.Icon.Trim());
                permissionmodel.ModifiedBy = obj.CreatedBy;
                permissionmodel.LastModified = DateTime.Now;
                permissionmodel.IsActive = obj.IsActive;
                permissionmodel.Icon = obj.Icon;

                if (await _context.SaveChangesAsync() > 0)
                {
                    _activityRepo.CreateActivityLog(string.Format("Update Portal permission with Name : {0}", permissionmodel.PermissionName), Controller, ActionName, obj.CreatedBy, permissionmodel);

                    status = ResponseErrorMessageUtility.SuccessfulAndUpdated;
                    return status;
                }
                else
                {
                    status = ResponseErrorMessageUtility.RecordNotSaved;
                    return status;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }
        }





        public async Task<bool> DeletePermission(long id, string ModifiedBy, string Controller, string ActionName)
        {
            try
            {
                var model = await _context.Permissions
                    .FirstOrDefaultAsync(x => x.ID == id);

                model.IsActive = false;

                model.ModifiedBy = ModifiedBy;
                model.LastModified = DateTime.Now;
                model.IsDeleted = true;
                if (await _context.SaveChangesAsync() > 0)
                {
                    _activityRepo.CreateActivityLog(string.Format("Deleted Portal Permission with Name:{0}", model.PermissionName), Controller,ActionName,model.CreatedBy, model);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }


        public async Task<List<PermissionViewModel>> GetAllPermissions(bool? status)
        {



            List<PermissionViewModel> modelResponse = new List<PermissionViewModel>();

            Models.DataObjects.Permission.PermissionViewModel model = new PermissionViewModel();

            try
            {
                var parentList = await GetAllParent(true);

                if (status == null)
                {

                    return await _context.Permissions.Where(a => a.IsDeleted != true)
                    .Select(modelResponse => new Models.DataObjects.Permission.PermissionViewModel
                    {
                        PermissionName = modelResponse.PermissionName,
                        ID = modelResponse.ID,
                        PermissionCode = modelResponse.PermissionCode,
                        ActionTitle = "",
                        Url = modelResponse.Url,
                        ParentId = modelResponse.ParentId,
                        CreatedDate = DateTime.Now,
                      //  Parent= parentList.FirstOrDefault(a=>a.ParentId == modelResponse.ParentId).PermissionName,
                        ParentMenus = parentList.ToList().Select(u => new SelectListItem { Text = u.PermissionName, Value = u.ID.ToString() }).ToList(),

                        IsActive = (bool)modelResponse.IsActive,
                        CreatedBy = modelResponse.CreatedBy,
                        DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                        ResponseMessage = ResponseErrorMessageUtility.RecordFetched
                    })


                    .ToListAsync();
                }
                else
                {
                    return await _context.Permissions
                    .Where(x => x.IsActive == status)
                    .Select(modelResponse => new Models.DataObjects.Permission.PermissionViewModel
                    {
                        PermissionName = modelResponse.PermissionName,
                        ID = modelResponse.ID,
                        PermissionCode = modelResponse.PermissionCode,
                        ActionTitle = "",
                        Url = modelResponse.Url,
                        ParentId = modelResponse.ParentId,
                        CreatedDate = DateTime.Now,
                        ParentMenus = parentList.ToList().Select(u => new SelectListItem { Text = u.PermissionName, Value = u.ID.ToString() }).ToList(),

                        IsActive = (bool)modelResponse.IsActive,
                        CreatedBy = modelResponse.CreatedBy,
                        DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                        ResponseMessage = ResponseErrorMessageUtility.RecordFetched

                    })
                    .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                model.DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_Fail;
                model.ResponseMessage = ResponseErrorMessageUtility.RecordNotFetched + " " + ex.Message;
                modelResponse.Add(model);
                return modelResponse;
            }
        }

        public async Task<List<PermissionViewModel>> GetAllParent(bool? status)
        {



            List<PermissionViewModel> modelResponse = new List<Models.DataObjects.Permission.PermissionViewModel>();

            Models.DataObjects.Permission.PermissionViewModel model = new Models.DataObjects.Permission.PermissionViewModel();

            try
            {

                if (status == null)
                {

                    return await _context.Permissions.Where(a => a.ParentId == ResponseErrorMessageUtility.PermissionParentId && a.IsDeleted != true).OrderBy(a => a.PermissionName)
                    .Select(modelResponse => new Models.DataObjects.Permission.PermissionViewModel
                    {
                        PermissionName = modelResponse.PermissionName,
                        ID = modelResponse.ID,
                        DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                        ResponseMessage = ResponseErrorMessageUtility.RecordFetched
                    })


                    .ToListAsync();
                }
                else
                {
                    return await _context.Permissions.Where(a => a.ParentId == ResponseErrorMessageUtility.PermissionParentId && a.IsActive == status)
                     .Select(modelResponse => new Models.DataObjects.Permission.PermissionViewModel
                     {
                         PermissionName = modelResponse.PermissionName,
                         ID = modelResponse.ID,
                         DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                         ResponseMessage = ResponseErrorMessageUtility.RecordFetched
                     })


                     .ToListAsync();

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                model.DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_Fail;
                model.ResponseMessage = ResponseErrorMessageUtility.RecordNotFetched + " " + ex.Message;
                modelResponse.Add(model);
                return modelResponse;
            }
        }


        public async Task<PermissionViewModel> GetPermissionByID(long RoleId)
        {

            PermissionViewModel model = new PermissionViewModel();
            var parentList = await GetAllParent(true);


            try
            {

                return await _context.Permissions
                    .Where(x => x.ID == RoleId)




                    .Select(modelResponse => new PermissionViewModel
                    {
                        PermissionName = modelResponse.PermissionName,
                        ID = modelResponse.ID,
                        PermissionCode = modelResponse.PermissionCode,

                        Url = modelResponse.Url,
                        ParentId = modelResponse.ParentId,

                        CreatedDate = DateTime.Now,
                        IsActive = (bool)modelResponse.IsActive,
                        CreatedBy = modelResponse.CreatedBy,
                        Icon = modelResponse.Icon,
                        ParentMenus = parentList.Select(u => new SelectListItem { Text = u.PermissionName, Value = u.ID.ToString() }).ToList(),
                        DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                        ResponseMessage = ResponseErrorMessageUtility.RecordFetched

                    })
                    .FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                model.DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_Fail;
                model.ResponseMessage = ResponseErrorMessageUtility.RecordNotFetched + " " + ex.Message;
                return model;
            }
        }


    }
}
