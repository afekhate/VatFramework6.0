using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VatFramework.Models.Domains;

namespace VatFramework.Models.DataObjects.Permission
{
         
    public class PermissionViewModel : BaseObjectDataIntegrity
    {

        [Required(ErrorMessage = "Permission Name is required")]
        public string PermissionName { get; set; }

        //[Required(ErrorMessage = "Code is required")]
        public string PermissionCode { get; set; }
        public string Icon { get; set; }
        [Required(ErrorMessage = "Url is required")]
        public string Url { get; set; }
        public int? ParentId { get; set; }

        public string Parent { get; set; }

        public string Action { get; set; }
        public string ActionTitle { get; set; }
        public List<SelectListItem> ParentMenus { get; set; }
    }
}
