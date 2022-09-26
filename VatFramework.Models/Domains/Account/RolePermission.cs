using System;

namespace VatFramework.Models.Domains.Account
{
    public class RolePermission : BaseObject
    {
        public Int64 PermissionId { get; set; }
        public string RoleId { get; set; }
    }
}
