using System.Linq;
using System.Security.Claims;
using VatFramework.Utilities.Enums;

namespace VatFramework.Utilities.Extensions.Permission
{
    public static class UserHasThisPermission
    {
        /// <summary>
        /// This returns true if the current user has the permission
        /// </summary>
        /// <param name="user"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        public static bool FRSCInventoryUserHasThisPermission(this ClaimsPrincipal user, TranspecsPermissions permission)
        {
            var permissionClaim =
                user?.Claims.FirstOrDefault(x => x.Type == PermissionConstants.PackedPermissionClaimType);
            return permissionClaim?.Value.UnpackPermissionsFromString().ToArray().UserHasThisPermission(permission) == true;
        }
    }
}
