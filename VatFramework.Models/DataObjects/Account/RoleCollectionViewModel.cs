using System.Collections.Generic;
using FRSCInventory.Models.DataObjects.User;
using VatFramework.Models.DataObjects.User;

namespace VatFramework.Models.DataObjects.Account
{
    public class RoleCollectionViewModel
    {
        public string roleid { get; set; }
        public string rolename { get; set; }
        public string roledesc { get; set; }

        public IList<UserViewModel> assignedusers { get; set; }
        public IList<UserViewModel> unassignedusers { get; set; }
    }
}
