using System;
using System.Collections.Generic;
using System.Text;
using VatFramework.Models.DataObjects.Icon;
using VatFramework.Models.Domains.Account;

namespace VatFramework.Models.DataObjects.Account
{
    public class AuthModel
    {
        public string RoleId { get; set; }
        public string MenuString { get; set; }
        public string Menus { get; set; }
        public string token { get; set; }
        public List<DataObjects.ApplicationRole.ApplicationRoleViewModel> Role { get; set; }

        public DateTime ExpiryTime { get; set; }
        public List<SidebarMenuViewModel> DynamicMenu { get; set; }

        public int RolesId { get; set; }
        public ApplicationUser UserInformation { get; set; }
    }
}
