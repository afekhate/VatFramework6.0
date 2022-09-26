using System;
using System.Collections.Generic;
using System.Linq;
using VatFramework.Utilities.Enums;

namespace VatFramework.Utilities.Extensions.Permission
{
    public static class PermissionPackers
    {
        public static string PackPermissionsIntoString(this IEnumerable<TranspecsPermissions> permissions)
        {
            return permissions.Aggregate("", (s, permission) => s + (char)permission);
        }

        public static IEnumerable<TranspecsPermissions> UnpackPermissionsFromString(this string packedPermissions)
        {
            if (packedPermissions == null)
                throw new ArgumentNullException(nameof(packedPermissions));
            foreach (var character in packedPermissions)
            {
                yield return ((TranspecsPermissions)character);
            }
        }

        public static TranspecsPermissions? FindPermissionViaName(this string permissionName)
        {
            return Enum.TryParse(permissionName, out TranspecsPermissions permission)
                ? (TranspecsPermissions?)permission
                : null;
        }
    }
}
