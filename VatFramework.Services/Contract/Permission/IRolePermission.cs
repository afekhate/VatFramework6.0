using System.Collections.Generic;
using System.Threading.Tasks;
using VatFramework.Models.DataObjects.Permission;

namespace VatFramework.Services.Contract.Permission
{
    public interface IRolePermission
    {
        Task<IEnumerable<PermissionViewModel>> GetRolePermissions(bool? status);
        Task<IEnumerable<PermissionViewModel>> GetParentPermissions(bool? status);

        string GetRoleNameByUserId(long userId);
    }
}
