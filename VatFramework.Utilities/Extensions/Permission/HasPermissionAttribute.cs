using Microsoft.AspNetCore.Authorization;
using System;
using VatFramework.Utilities.Enums;

namespace VatFramework.Utilities.Extensions.Permission
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(TranspecsPermissions permission) : base(permission.ToString())
        { }
    }
}
