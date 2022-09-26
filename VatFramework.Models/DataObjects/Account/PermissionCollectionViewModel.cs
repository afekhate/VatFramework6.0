using System.Collections.Generic;
using VatFramework.Models.DataObjects.Permission;

namespace VatFramework.Models.DataObjects.Account
{
    public class PermissionCollectionViewModel
    {
        public string roleid { get; set; }
        public string rolename { get; set; }
        public string roledesc { get; set; }
        public IList<PermissionViewModel> assignedpermissions { get; set; }
        public IList<PermissionViewModel> unassignedpermissions { get; set; }
    }
}
