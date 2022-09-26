using System.Collections.Generic;
using System.Threading.Tasks;
using VatFramework.Models.DataObjects.ApplicationRole;

namespace VatFramework.Services.Contract.EntityService
{
    public interface IRole
    {
  
        Task<List<ApplicationRoleViewModel>> GetRoles(bool? status);
        
        Task<ApplicationRoleViewModel> GetRoleById(string RoleId);

        List<string> GetUserRoleByUserId(string UserId);

        Task<string> CreateRole(ApplicationRoleViewModel obj, string ControllerName, string ActionName);
        Task<string> UpdateRole(ApplicationRoleViewModel obj, string ControllerName, string ActionName);
        Task<bool> DeleteRole(string id, string ModifiedBy, string ControllerName, string ActionName);

        Task<string> MapUsersToRoles(string UserId, List<ApplicationRoleViewModel> userRoles, string ControllerName, string ActionName);
    }
}
