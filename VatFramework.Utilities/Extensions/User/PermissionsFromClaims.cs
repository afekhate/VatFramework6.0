using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using VatFramework.Utilities.Enums;
using VatFramework.Utilities.Extensions.Permission;

namespace FRSCInventory.Utilities.Extensions.User
{
    public static class PermissionsFromClaims
    {
        /// <summary>
        /// This gets the permissions for the currently logged in user (or null if no claim)
        /// </summary>
        /// <param name="usersClaims"></param>
        /// <returns></returns>
        public static IEnumerable<TranspecsPermissions> UserPermissionsFromClaims(this IEnumerable<Claim> usersClaims)
        {
            var permissionsClaim =
                usersClaims?.FirstOrDefault(c => c.Type == PermissionConstants.PackedPermissionClaimType);
            return permissionsClaim?.Value.UnpackPermissionsFromString();
        }
    }
}
