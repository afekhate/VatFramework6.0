using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VatFramework.Models.DataObjects.Permission;
using VatFramework.Services.Contract.Permission;

namespace VatFramework.Services.Handler.RolePermission
{
    public class RolePermissionService : IRolePermission
    {
        public Task<IEnumerable<PermissionViewModel>> GetParentPermissions(bool? status)
        {
            throw new NotImplementedException();
        }

        public string GetRoleNameByUserId(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PermissionViewModel>> GetRolePermissions(bool? status)
        {
            throw new NotImplementedException();
        }
    }
}
