using System.Collections.Generic;
using System.Threading.Tasks;
using VatFramework.Models.DataObjects.Permission;

namespace VatFramework.Services.Contract.EntityService
{
    public interface IPermission
    {
        Task<List<PermissionViewModel>> GetAllPermissions(bool? status);


        Task<PermissionViewModel> GetPermissionByID(long id);
        Task<string> CreatePermission(PermissionViewModel obj,string Controller, string ActionName);
        Task<string> UpdatePermission(PermissionViewModel obj, string Controller, string ActionName);
        Task<bool> DeletePermission(long id, string ModifiedBy, string Controller, string ActionName);
        Task<List<PermissionViewModel>> GetAllParent(bool? status);

        Task<string> UpdateRolePermission(string id, List<string> roles, string UserId, string Controller, string ActionName);
        

    }
}
