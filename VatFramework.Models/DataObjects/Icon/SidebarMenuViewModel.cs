using System;
using System.Collections.Generic;
using System.Text;

namespace VatFramework.Models.DataObjects.Icon
{
    public class SidebarMenuViewModel
    {
        public string MenuText { get; set; }
        public string Alias { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string PID { get; set; }
        public string ParentId { get; set; }
        public List<Domains.Permission.Permission> SubMenus { get; set; }

        public string action { get; set; }
        public string controller { get; set; }



    }
}
