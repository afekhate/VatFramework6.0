using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VatFramework.Models;
using VatFramework.Models.DataObjects.Account;
using VatFramework.Services.Contract.EntityService;
using VatFramework.Services.Contract.UserService;
using VatFramework.Utilities.AuthenticationUtility.AuthUser;
using VatFramework.Utilities.ExceptionUtility;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using VatFramework.Services.Handler.DataAccess;
using System.Data;
using VatFramework.Models.DataObjects.ApplicationRole;
using VatFramework.Models.Domains.Account;

namespace VatFramework.Services.Handler.UserService
{
    public class UserService : IUserService
    {
        private readonly APPContext _Context;
        private readonly ILogger<UserService> _logger;
        private readonly IAuthUser _authUser;
        private readonly IActivityLog _activityLog;
        private readonly IConfiguration _configuration;
        private readonly IRole _roleManager;
        protected readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        protected readonly Microsoft.AspNetCore.Identity.SignInManager<ApplicationUser> _signInManager;
      
        protected readonly Microsoft.AspNetCore.Identity.RoleManager<ApplicationRole> _roleSystemManager;
        public UserService(APPContext Context, ILogger<UserService> logger, IAuthUser authUser, 
            IActivityLog activityLog, IConfiguration  configuration, IRole roleManager, 
            Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager,
            Microsoft.AspNetCore.Identity.SignInManager<ApplicationUser> signInManager,
            Microsoft.AspNetCore.Identity.RoleManager<ApplicationRole> roleSystemManager)
        {
            _Context = Context;
            _logger = logger;
            _authUser = authUser;
            _activityLog = activityLog;
            _configuration = configuration;
            _roleManager = roleManager;
            _roleSystemManager = roleSystemManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<List<AdminUserSettingViewModel>> GetAllUserAccountsByRoleId(string RoleId)
        {

            List<AdminUserSettingViewModel> modelResponse = new List<AdminUserSettingViewModel>();
            AdminUserSettingViewModel model = new AdminUserSettingViewModel();

            try
            {
                
                    return await _Context.ApplicationUsers
                        .Where(x => x.IsActive == true && x.RoleId == RoleId)
                        .Select(modelResponse => new AdminUserSettingViewModel
                        {
                            Id = modelResponse.Id,
                            FirstName = modelResponse.FirstName,
                            LastName = modelResponse.LastName,
                            MiddleName = modelResponse.MiddleName,
                            UserName = modelResponse.UserName,
                            MobileNumber = modelResponse.MobileNumber,
                            PhoneNumber = modelResponse.PhoneNumber,
                            Email = modelResponse.Email,
                            ForcePwdChange = modelResponse.ForcePwdChange,
                            PwdChangedDate = modelResponse.PwdChangedDate,
                            ModifiedBy = modelResponse.ModifiedBy,
                            
                            DOB = modelResponse.DOB,
                            RoleId = modelResponse.RoleId,
                            CreatedDate = modelResponse.CreatedDate,
                            DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                            ResponseMessage = ResponseErrorMessageUtility.RecordFetched
                        }).ToListAsync();
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

        public async Task<List<AdminUserSettingViewModel>> GetAllUserAccounts(bool? status)
        {
            
            List<AdminUserSettingViewModel> modelResponse = new List<AdminUserSettingViewModel>();
            AdminUserSettingViewModel model = new AdminUserSettingViewModel();
            
            try
            {
                if(status == null)
                {
                    return await _Context.ApplicationUsers.

                        Select(modelResponse => new AdminUserSettingViewModel
                        {
                            Id = modelResponse.Id,
                            FirstName = modelResponse.FirstName,
                            LastName = modelResponse.LastName,
                            MiddleName = modelResponse.MiddleName,
                            UserName = modelResponse.UserName,
                            MobileNumber = modelResponse.MobileNumber,
                            PhoneNumber = modelResponse.PhoneNumber,
                            Email = modelResponse.Email,
                            ForcePwdChange = modelResponse.ForcePwdChange,
                            PwdChangedDate = modelResponse.PwdChangedDate,
                            ModifiedBy = modelResponse.ModifiedBy,
                            DOB= modelResponse.DOB,
                            RoleId = modelResponse.RoleId,
                            CreatedDate = modelResponse.CreatedDate,
                            IsActive = modelResponse.IsActive,
                            RoleName = _Context.ApplicationRoles.FirstOrDefault(a => a.Id == modelResponse.RoleId).RoleName,
                            DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                            ResponseMessage = ResponseErrorMessageUtility.RecordFetched
                        }).ToListAsync();
                        
                }
                else
                {
                    return await _Context.ApplicationUsers
                        .Where(x => x.IsActive == status)
                        .Select(modelResponse => new AdminUserSettingViewModel
                        {
                            Id = modelResponse.Id,
                            FirstName = modelResponse.FirstName,
                            LastName = modelResponse.LastName,
                            MiddleName = modelResponse.MiddleName,
                            UserName = modelResponse.UserName,
                            MobileNumber = modelResponse.MobileNumber,
                            PhoneNumber = modelResponse.PhoneNumber,
                            Email = modelResponse.Email,
                            ForcePwdChange = modelResponse.ForcePwdChange,
                            PwdChangedDate = modelResponse.PwdChangedDate,
                            ModifiedBy = modelResponse.ModifiedBy,
                            DOB = modelResponse.DOB,
                            RoleId = modelResponse.RoleId,
                            CreatedDate = modelResponse.CreatedDate,
                            DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                            ResponseMessage = ResponseErrorMessageUtility.RecordFetched
                        }).ToListAsync();
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


        public async Task<AdminUserSettingViewModel> GetUserAccountByUserId(string UserId)
        {
            //throw new NotImplementedException();
            AdminUserSettingViewModel model = new AdminUserSettingViewModel();
            try
            {
                return await _Context.ApplicationUsers.
                           Select(modelResponse => new AdminUserSettingViewModel
                           {
                               Id = modelResponse.Id,
                               FirstName = modelResponse.FirstName,
                               LastName = modelResponse.LastName,
                               MiddleName = modelResponse.MiddleName,
                               UserName = modelResponse.UserName,
                               MobileNumber = modelResponse.MobileNumber,
                               PhoneNumber = modelResponse.PhoneNumber,
                               Email = modelResponse.Email,
                               ForcePwdChange = modelResponse.ForcePwdChange,
                               PwdChangedDate = modelResponse.PwdChangedDate,
                               ModifiedBy = modelResponse.ModifiedBy,
                               DOB = modelResponse.DOB,
                               CreatedDate = modelResponse.CreatedDate,
                               
                               RoleId = modelResponse.RoleId,
                               IsActive = modelResponse.IsActive,
                               IsDeleted = modelResponse.IsDeleted,
                               DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                               ResponseMessage = ResponseErrorMessageUtility.RecordFetched,
                               UserToken = modelResponse.UserToken
                           }).FirstOrDefaultAsync(x => x.Id == UserId);
            }
            catch (Exception ex)
            {
                model.DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_Fail;
                model.ResponseMessage = ResponseErrorMessageUtility.RecordNotFetched + " " + ex.Message;
                _logger.LogError(ex.Message);
                return model;
            }
        }

        public async Task<AdminUserSettingViewModel> GetUserAccountByEmail(string EmailAddress)
        {
            //throw new NotImplementedException();
            AdminUserSettingViewModel model = new AdminUserSettingViewModel();
            try
            {
                return await _Context.ApplicationUsers.
                           Select(modelResponse => new AdminUserSettingViewModel
                           {
                               Id = modelResponse.Id,
                               FirstName = modelResponse.FirstName,
                               LastName = modelResponse.LastName,
                               MiddleName = modelResponse.MiddleName,
                               UserName = modelResponse.UserName,
                               MobileNumber = modelResponse.MobileNumber,
                               PhoneNumber = modelResponse.PhoneNumber,
                               Email = modelResponse.Email,
                               ForcePwdChange = modelResponse.ForcePwdChange,
                               PwdChangedDate = modelResponse.PwdChangedDate,
                               ModifiedBy = modelResponse.ModifiedBy,
                               DOB = modelResponse.DOB,
                               CreatedDate = modelResponse.CreatedDate,
                               IsActive = modelResponse.IsActive,
                               IsDeleted = modelResponse.IsDeleted,
                               
                               RoleId = modelResponse.RoleId,
                               DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                               ResponseMessage = ResponseErrorMessageUtility.RecordFetched
                           }).FirstOrDefaultAsync(x => x.Email.ToLower() == EmailAddress.ToLower());
            }
            catch (Exception ex)
            {
                model.DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_Fail;
                model.ResponseMessage = ResponseErrorMessageUtility.RecordNotFetched + " " + ex.Message;
                _logger.LogError(ex.Message);
                return model;
            }
        }

        public async Task<AdminUserSettingViewModel> GetUserAccountById(string UserId)
        {
            //throw new NotImplementedException();
            AdminUserSettingViewModel model = new AdminUserSettingViewModel();
            try
            {
                return await _Context.ApplicationUsers.
                           Select(modelResponse => new AdminUserSettingViewModel
                           {
                               Id = modelResponse.Id,
                  
                               FirstName = modelResponse.FirstName,
                               LastName = modelResponse.LastName,
                               MiddleName = modelResponse.MiddleName,
                               UserName = modelResponse.UserName,
                               MobileNumber = modelResponse.MobileNumber,
                               PhoneNumber = modelResponse.PhoneNumber,
                               Email = modelResponse.Email,
                               ForcePwdChange = modelResponse.ForcePwdChange,
                               PwdChangedDate = modelResponse.PwdChangedDate,
                               ModifiedBy = modelResponse.ModifiedBy,
                               DOB = modelResponse.DOB,
                               CreatedDate = modelResponse.CreatedDate,
                               // RoleList = modelResponse.RoleList,
                               IsActive = modelResponse.IsActive,
                               IsDeleted = modelResponse.IsDeleted,
                             
                               RoleId = modelResponse.RoleId,
                               // Roles = modelResponse.Roles,
                               DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                               ResponseMessage = ResponseErrorMessageUtility.RecordFetched
                           }).FirstOrDefaultAsync(x => x.Id == UserId);
            }
            catch (Exception ex)
            {
                model.DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_Fail;
                model.ResponseMessage = ResponseErrorMessageUtility.RecordNotFetched + " " + ex.Message;
                _logger.LogError(ex.Message);
                return model;
            }
        }


        public async Task<string> CreateUser(AdminUserSettingViewModel obj,string ControllerName, string ActionName)
        {
            //throw new NotImplementedException();
           
            string status = "";
            try
            {
               
                string[] breakEmail = obj.Email.Split("@");
                int TotalDays = Convert.ToInt32(_configuration["PasswordExpireDate"]);
                DateTime expireDate = DateTime.Now.AddDays(TotalDays);


                // this will be in Minutes
                int Mins =  Convert.ToInt32(_configuration["expiredConfirmationLinkDate"]);

                DateTime expiredConfirmationLinkDate = DateTime.Now.AddMinutes(Mins);

                obj.UserName = breakEmail[0];
              


                
                var checkexist = await CheckEixstingAccount(obj);


                
                if(checkexist == false)
                {


                    ApplicationUser newRecord = new ApplicationUser()
                    {
                        FirstName = obj.FirstName,
                        LastName = obj.LastName,
                        MiddleName = obj.MiddleName,
                        UserName = obj.UserName,
                        MobileNumber = obj.MobileNumber,
                        PhoneNumber = obj.MobileNumber,
                        CreatedBy = obj.CreatedBy,
                        Email = obj.Email,
                        PwdExpiryDate = expireDate,
                        // NormalizedEmail = obj.Email,
                        // NormalizedUserName = obj.UserName,
                        CreatedDate = DateTime.Now,
                        ExpirationTime = expireDate,
                        IsActive = true,
                        RoleId = obj.RoleId,
                        PhoneNumberConfirmed = true,
                        EmailConfirmed = false,
                        ModifiedBy = obj.ModifiedBy,
                        ForcePwdChange = true,            
                       
                        ConfirmationLinkExpireDate = expiredConfirmationLinkDate
                       
                       
                };

                   // newRecord.Id = Guid.NewGuid().ToString();

                    var result =  await _userManager.CreateAsync(newRecord);
                    
                    if (result.Succeeded)
                    {
                        // audit trail ----- string description, string moduleName, string moduleAction, Int64 userid, object record
                        await _activityLog.CreateActivityLogAsync("New User created", ControllerName, ActionName,obj.CreatedBy, obj);
                        status = ResponseErrorMessageUtility.RecordSaved;
                        return status;
                    }
                    else
                    {
                        status = ResponseErrorMessageUtility.RecordNotSaved;
                        return status;
                    }

                }
                status = string.Format(ResponseErrorMessageUtility.RecordExistBefore, obj.UserName.ToUpper());
                return status;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }
            
        }
    
      
        public async Task<string> UpdateUser(AdminUserSettingViewModel obj, List<ApplicationRoleViewModel> RoleId, string ControllerName, string ActionName)
        {
            //throw new NotImplementedException();
            ApplicationUser newRecord = new ApplicationUser();
            string status = "";
            try
            {
             
                var checkexist = await CheckExistAccountById(obj.Id);


                if (checkexist ==false)
                {
                    status = ResponseErrorMessageUtility.NotExistRecord;
                    return status;
                }


                var checkexist_ = await _Context.ApplicationUsers.AnyAsync(x => x.Id == obj.Id);

                // check if the record exist 
                if (checkexist_ != false)
                {
                    var model = await _Context.ApplicationUsers
                         .FirstOrDefaultAsync(x => x.Id == obj.Id);


                    string[] breakEmail = obj.Email.Split("@");


                    model.Id = obj.Id;
                    model.FirstName = obj.FirstName;
                    model.LastName = obj.LastName;
                    model.MiddleName = obj.MiddleName;
                    model.UserName = breakEmail[0];
                    model.MobileNumber = obj.MobileNumber;
                    model.PhoneNumber = obj.PhoneNumber;
                    model.Email = obj.Email;
                    model.ModifiedBy = obj.ModifiedBy;
                    model.LastModified = DateTime.Now;
                    model.RoleId = RoleId.FirstOrDefault().ID;
                    model.IsActive = obj.IsActive;
                    model.LastModified = DateTime.Now;
                    model.MiddleName = obj.MiddleName;
                    model.PhoneNumber = obj.PhoneNumber;
                    model.NormalizedEmail = obj.Email.ToUpper();
                    
                    model.RoleId = obj.RoleId;
                   
                    var result=  await _userManager.UpdateAsync(model);


                    //_Context.ApplicationUsers.Update(model);

                    if (result.Succeeded)
                    {
                        //DataAccess.AccessDataLayer accessDataLayer = new DataAccess.AccessDataLayer(_Context);
                        //DBManager dBManager = new DBManager(_Context);
                        //var parameters = new List<IDbDataParameter>();
                        //parameters.Add(dBManager.CreateParameter("@RoleId", RoleId.FirstOrDefault().ID, DbType.String));
                        //accessDataLayer.DeletePermissionByRoleID(parameters.ToArray(), "DeletePermissionByRoleID");


                        //  .. string roleResult = await _roleManager.MapUsersToRoles(model.Id, RoleId,ControllerName,ActionName);

                        await  _activityLog.CreateActivityLogAsync("Updated User Record", ControllerName,ActionName, obj.Id, obj);


                        status = ResponseErrorMessageUtility.SuccessfulAndUpdated;
                        return status;
                    }
                    else
                    {
                        status = ResponseErrorMessageUtility.RecordNotSaved;
                        return status;
                    }
                }
                else
                {
                    status = ResponseErrorMessageUtility.RecordNotSaved;
                    return status;
                }
                
                //status = string.Format(ResponseErrorMessageUtility.RecordExistBefore, obj.UserName.ToUpper());
               //return status;
            }
            catch (Exception ex)
            { 
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }
        }

        public async Task<bool> DeleteUser(string UserId, string ControllerName, string ActionName)
        {
            //throw new NotImplementedException();
            try
            {
                var model = await _Context.ApplicationUsers
                    .FirstOrDefaultAsync(x => x.Id == UserId);

                model.IsActive = false;
                model.ModifiedBy = _authUser.Name;
                model.LastModified = DateTime.Now;
                model.IsDeleted = true;

                if (await _Context.SaveChangesAsync() > 0)
                {
                    await _activityLog.CreateActivityLogAsync("Deleted User Record",ControllerName,ActionName, UserId, model);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> CheckEixstingAccount(AdminUserSettingViewModel obj)
        {
            //throw new NotImplementedException();
            var checkexist = await _Context.ApplicationUsers
                  .AnyAsync(x => x.Email.ToLower() == obj.Email.ToLower()
                   
                  );

            return checkexist;
        }

        public async Task<bool> CheckExistAccountByEmail(string EmailAddress)
        {
            //throw new NotImplementedException();
            var checkexist = await _Context.ApplicationUsers
                  .AnyAsync(x => x.Email.ToLower() != EmailAddress.ToLower());
            return checkexist;
        }

        public async Task<bool> CheckExistAccountById(string UserId)
        {
            //throw new NotImplementedException();
            var checkexist = await _Context.ApplicationUsers
                  .AnyAsync(x => x.Id == UserId);
            return checkexist;
        }

        public async Task<List<AdminUserSettingViewModel>> GetStoreApprovalsOfficers()
        {
           
                //throw new NotImplementedException();
                AdminUserSettingViewModel model = new AdminUserSettingViewModel();
                try
                {
                var storeAdmins =  from p in _Context.ApplicationUsers
                                   join r in _Context.Roles.Where(x => x.RoleName== " Main Store Officer" ||
                                   x.RoleName == "Central Administrator") on p.RoleId equals r.Id
                                  
                                   select new AdminUserSettingViewModel
                                   {
                                       Id = p.Id,
                                       FirstName = p.FirstName,
                                       LastName = p.LastName,
                                       MiddleName = p.MiddleName,
                                       UserName = p.UserName,
                                       MobileNumber = p.MobileNumber,
                                       PhoneNumber = p.PhoneNumber,
                                       Email = p.Email,
                                       IsActive = p.IsActive,

                                   };

                return await storeAdmins.ToListAsync();
            }
                catch (Exception ex)
                {
                    model.DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_Fail;
                    model.ResponseMessage = ResponseErrorMessageUtility.RecordNotFetched + " " + ex.Message;
                    _logger.LogError(ex.Message);
                    return new List<AdminUserSettingViewModel>();
                }
        }


        public async Task<List<AdminUserSettingViewModel>> GetApprovalList(string ApprovalType, long? PlantId, long? MiniPlantId)
        {

            //throw new NotImplementedException();
            AdminUserSettingViewModel model = new AdminUserSettingViewModel();

            if (ApprovalType == "MINI_PLANT_REQUEST")
            {
                try
                {
                    var storeAdmins = from p in _Context.ApplicationUsers
                                      join r in _Context.Roles on p.RoleId equals r.Id
                                      where p.RoleId == ResponseErrorMessageUtility.PlantAdmin
                                          


                                      select new AdminUserSettingViewModel
                                      {
                                          Id = p.Id,
                                          FirstName = p.FirstName,
                                          LastName = p.LastName,
                                          MiddleName = p.MiddleName,
                                          UserName = p.UserName,
                                          MobileNumber = p.MobileNumber,
                                          PhoneNumber = p.PhoneNumber,
                                          Email = p.Email,
                                          IsActive = p.IsActive,

                                      };

                    return await storeAdmins.ToListAsync();
                }
                catch (Exception ex)
                {
                    model.DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_Fail;
                    model.ResponseMessage = ResponseErrorMessageUtility.RecordNotFetched + " " + ex.Message;
                    _logger.LogError(ex.Message);
                    return new List<AdminUserSettingViewModel>();
                }

                
            }
            return new List<AdminUserSettingViewModel>();
        }

    }
}
