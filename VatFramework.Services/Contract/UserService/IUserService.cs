using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VatFramework.Models.DataObjects.Account;
using VatFramework.Models.DataObjects.ApplicationRole;
using VatFramework.Models.Domains.Account;

namespace VatFramework.Services.Contract.UserService
{
    public interface IUserService
    {
        Task<List<AdminUserSettingViewModel>> GetAllUserAccounts(bool? status);
        
        Task<List<AdminUserSettingViewModel>> GetAllUserAccountsByRoleId(string RoleId);

        Task<string> CreateUser(AdminUserSettingViewModel obj, string ControllerName, string ActionName);

        Task<bool> CheckEixstingAccount(AdminUserSettingViewModel obj);

        Task<bool> CheckExistAccountByEmail(string EmailAddress);

        Task<bool> CheckExistAccountById(string UserId);
        Task<AdminUserSettingViewModel> GetUserAccountByEmail(string EmailAddress);

        Task<AdminUserSettingViewModel> GetUserAccountByUserId(string UserId);

        Task<AdminUserSettingViewModel> GetUserAccountById(string UserId);
        Task<string> UpdateUser(AdminUserSettingViewModel obj, List<ApplicationRoleViewModel> roles, string ControllerName, string ActionName);

        Task<bool> DeleteUser(string UserId, string ControllerName, string ActionName);

        Task<List<AdminUserSettingViewModel>> GetStoreApprovalsOfficers();

        Task<List<AdminUserSettingViewModel>> GetApprovalList(string ApprovalType, long? PlantId, long? MiniPlantId);


    }
}
