using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using VatFramework.Models;
using VatFramework.Models.DataObjects.ApplicationRole;
using VatFramework.Models.Domains.Account;
using VatFramework.Services.Contract.EntityService;
using VatFramework.Services.Handler.DataAccess;
using VatFramework.Utilities.AuthenticationUtility.AuthUser;
using VatFramework.Utilities.ExceptionUtility;

namespace VatFramework.Services.Handler.RoleServices
{
    public class RoleServices : IRole
    {
        private readonly APPContext _context;
        private readonly IConfiguration _config;
        private readonly ILogger<RoleServices> _logger;
        private readonly IAuthUser _authUser;
        private readonly IActivityLog _activityRepo;



        public RoleServices(APPContext context,
          IAuthUser authUser, ILogger<RoleServices> logger, IActivityLog activityRepo)
        {
            _context = context;
            _authUser = authUser;
            _logger = logger;
            _activityRepo = activityRepo;
        }


        public async Task<string> CreateRole(ApplicationRoleViewModel obj,string ControllerName, string ActionName)
        {
            string status = "";
            try
            {

                var checkexist = await _context.ApplicationRoles
                    .AnyAsync(x => x.RoleName.ToUpper() == obj.RoleName.ToUpper()
                   );

                if (checkexist == false)
                {
                    ApplicationRole newrecord = new VatFramework.Models.Domains.Account.ApplicationRole
                    {
                        RoleName = obj.RoleName,
                        RoleDescription = obj.RoleDescription,
                        Name = obj.RoleName,
                        IsSysAdmin = obj.IsSysAdmin,
                        CreatedDate = DateTime.Now,
                        CreatedBy = obj.CreatedBy,
                        IsActive = true,
                        Id = Utilities.GeneralUtility.GenericUtil.generatePrimaryId()
                    };

                    _context.ApplicationRoles.Add(newrecord);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        _activityRepo.CreateActivityLog(string.Format("Created Portal permission with Name : {0}", newrecord.RoleName),
                            ControllerName,ActionName, obj.CreatedBy, newrecord);

                        status = ResponseErrorMessageUtility.RecordSaved;
                        return status;
                    }
                    else
                    {
                        status = ResponseErrorMessageUtility.RecordNotSaved;
                        return status;
                    }
                }
                status = string.Format(ResponseErrorMessageUtility.RecordExistBefore, obj.RoleName.ToUpper());
                return status;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }
        }



        public async Task<ApplicationRoleViewModel> GetRoleById(string RoleId)
        {
            ApplicationRoleViewModel model = new ApplicationRoleViewModel();

            try
            {



                return await _context.ApplicationRoles.Where(a => a.Id == RoleId)

                    .Select(modelResponse => new ApplicationRoleViewModel
                    {
                        RoleName = modelResponse.RoleName,
                        ID = modelResponse.Id,
                        RoleDescription = modelResponse.RoleDescription,
                        CreatedDate = DateTime.Now,
                        IsActive = (bool)modelResponse.IsActive,
                        ConcurrencyStamp = modelResponse.ConcurrencyStamp,
                        CreatedBy = modelResponse.CreatedBy,
                        DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                        ResponseMessage = ResponseErrorMessageUtility.RecordFetched

                    })
                     .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                model.DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_Fail;
                model.ResponseMessage = ResponseErrorMessageUtility.RecordNotFetched + " " + ex.Message;

                _logger.LogError(ex.Message);
                return model;
            }

        }




        public async Task<string> UpdateRole(ApplicationRoleViewModel obj, string ControllerName, string ActionName)
        {
            string status = "";
            try
            {


                var checkexist = await _context.ApplicationRoles
                   .AnyAsync(x => x.Id != obj.ID.ToString() &&
                   (x.RoleName.ToUpper() == obj.RoleName.ToUpper()));

                if (checkexist == false)
                {
                    var model = await _context.ApplicationRoles
                    .FirstOrDefaultAsync(x => x.Id == obj.ID);
                    model.RoleName = obj.RoleName;
                    model.RoleDescription = obj.RoleDescription;
                    model.ModifiedBy = obj.ModifiedBy;
                    model.LastModified = DateTime.Now;
                    model.IsActive = obj.IsActive;
                    model.Name = obj.RoleName;
                    model.IsSysAdmin = obj.IsSysAdmin;
                    model.Name = obj.RoleName;

                    if (await _context.SaveChangesAsync() > 0)
                    {
                        status = ResponseErrorMessageUtility.SuccessfulAndUpdated;
                        _activityRepo.CreateActivityLog(string.Format("Updated Portal Role with Name:{0}", model.Name), ControllerName, ActionName, obj.CreatedBy, checkexist);

                        return status;
                    }
                    else
                    {
                        status = ResponseErrorMessageUtility.RecordNotSaved;
                        return status;
                    }
                }
                status = string.Format(ResponseErrorMessageUtility.RecordExistBefore, obj.RoleName.ToUpper());
                return status;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }
        }


        public async Task<bool> DeleteRole(string id, string ModifiedBy, string ControllerName, string ActionName)
        {
            try
            {
                var model = await _context.ApplicationRoles
                    .FirstOrDefaultAsync(x => x.Id == id);

                model.IsActive = false;
                model.ModifiedBy = ModifiedBy;
                model.LastModified = DateTime.Now;

                if (await _context.SaveChangesAsync() > 0)
                {
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

        public async Task<List<ApplicationRoleViewModel>> GetApplicationRolesById(long RoleId)
        {



            List<ApplicationRoleViewModel> modelResponse = new List<ApplicationRoleViewModel>();

            ApplicationRoleViewModel model = new ApplicationRoleViewModel();

            try
            {

                return await _context.ApplicationRoles

                     .Where(x => x.IsActive == true && x.Id == RoleId.ToString())
                     .Select(modelResponse => new ApplicationRoleViewModel
                     {
                         RoleName = modelResponse.RoleName,
                         ID = modelResponse.Id,
                         RoleDescription = modelResponse.RoleDescription,
                         CreatedDate = DateTime.Now,
                         IsActive = (bool)modelResponse.IsActive,
                         CreatedBy = modelResponse.CreatedBy,
                         DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                         ResponseMessage = ResponseErrorMessageUtility.RecordFetched

                     })
                     .ToListAsync();

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

        public async Task<IEnumerable<ApplicationRoleViewModel>> GetApplicationRoles(bool? status)
        {



            List<ApplicationRoleViewModel> modelResponse = new List<ApplicationRoleViewModel>();

            ApplicationRoleViewModel model = new ApplicationRoleViewModel();

            try
            {

                if (status == null)
                {
                    return await _context.ApplicationRoles
                    .Select(modelResponse => new ApplicationRoleViewModel
                    {
                        RoleName = modelResponse.RoleName,
                        ID = modelResponse.Id,
                        RoleDescription = modelResponse.RoleDescription,
                        CreatedDate = DateTime.Now,
                        IsActive = (bool)modelResponse.IsActive,
                        CreatedBy = modelResponse.CreatedBy,
                        DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                        ResponseMessage = ResponseErrorMessageUtility.RecordFetched

                    })
                    .ToListAsync();
                }
                else
                {
                    return await _context.ApplicationRoles

                    .Where(x => x.IsActive == status)
                    .Select(modelResponse => new ApplicationRoleViewModel
                    {
                        RoleName = modelResponse.RoleName,
                        ID = modelResponse.Id,
                        RoleDescription = modelResponse.RoleDescription,
                        CreatedDate = DateTime.Now,
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








        public async Task<List<ApplicationRoleViewModel>> GetRoles(bool? status)
        {



            List<ApplicationRoleViewModel> modelResponse = new List<ApplicationRoleViewModel>();

            ApplicationRoleViewModel model = new ApplicationRoleViewModel();

            try
            {

                if (status == null)
                {
                    return await _context.ApplicationRoles 
                    .Select(modelResponse => new ApplicationRoleViewModel
                    {
                        RoleName = modelResponse.RoleName,
                        ID = modelResponse.Id,
                        RoleDescription = modelResponse.RoleDescription,
                        CreatedDate = DateTime.Now,
                        IsActive = (bool)modelResponse.IsActive,
                        CreatedBy = modelResponse.CreatedBy,
                        Name = modelResponse.RoleName,
                        ConcurrencyStamp = modelResponse.ConcurrencyStamp,
                        DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                        ResponseMessage = ResponseErrorMessageUtility.RecordFetched

                    }).OrderBy(a => a.Name).ToListAsync();
                }
                else
                {
                    return await _context.ApplicationRoles

                    .Where(x => x.IsActive == status)
                    .Select(modelResponse => new ApplicationRoleViewModel
                    {
                        RoleName = modelResponse.RoleName,
                        ID = modelResponse.Id,
                        RoleDescription = modelResponse.RoleDescription,
                        CreatedDate = DateTime.Now,
                        IsActive = (bool)modelResponse.IsActive,
                        CreatedBy = modelResponse.CreatedBy,
                        Name = modelResponse.RoleName,
                        ConcurrencyStamp = modelResponse.ConcurrencyStamp,
                        DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                        ResponseMessage = ResponseErrorMessageUtility.RecordFetched

                    }).OrderBy(a => a.Name).ToListAsync();
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

        public async Task<string> MapUsersToRoles(string UserId, List<ApplicationRoleViewModel> userRoles,string ControllerName, string ActionName)
        {
            string status = "";
            try
            {
                List<ApplicationUserRole> roles = new List<ApplicationUserRole>();

                foreach (var item in userRoles)
                {
                    ApplicationUserRole applicationUserRole = new ApplicationUserRole
                    {
                        RoleId = item.ID,
                        UserId = UserId
                    };
                    roles.Add(applicationUserRole);
                }


                _context.ApplicationUserRoles.AddRange(roles);

                if (await _context.SaveChangesAsync() > 0)
                {
                    status = ResponseErrorMessageUtility.RecordSaved;

                    _activityRepo.CreateActivityLog(string.Format("Mapped User To Role  Name : {0}", roles.FirstOrDefault().Role),
                         ControllerName, ActionName, UserId, roles);

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

        public List<string> GetUserRoleByUserId(string UserId)
        {
            List<string> roleList = new List<string>();

            string status = "";
            try
            {

                // call store procedure to query for the registerd roles 

                DBManager dBManager = new DBManager(_context);
                var parameters = new List<IDbDataParameter>();
                parameters.Add(dBManager.CreateParameter("@UserId", UserId, DbType.AnsiString));

                DataTable getRecords = dBManager.GetDataTable("GetUserRoleByUserId", CommandType.StoredProcedure, parameters.ToArray());


                foreach (DataRow row in getRecords.Rows)
                {
                    roleList.Add(row["RoleId"].ToString());
                }
                return roleList;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return null;
            }
        }
    }
}