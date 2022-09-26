using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VatFramework.Models.Domains.Account;
using VatFramework.Models.Domains.Permission;
namespace VatFramework.Models.DataObjects.Permission
{
    public class ApplicationRolePermissionViewModel
    {
        public ApplicationRolePermissionViewModel()
        {
            RolePermission = new List<RolePermission>();
            ParentMenus = new List<VatFramework.Models.Domains.Permission.Permission>();
            AllowedPermissionList = new List<VatFramework.Models.Domains.Permission.Permission>();
        }

        public string Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter a name for the Role.")]
        [StringLength(256, ErrorMessage = "The role name must be 256 characters or shorter.")]
        [Display(Name = "Role Name")]
        public string Name { get; set; }

        public string RoleName { get; set; }

        public bool? IsActive { get; set; }
        public string RoleDescription { get; set; }

        public bool Editable { get; set; }
        public bool IsSuper { get; set; }

        public string UnorderedList { get; set; }
        public long[] MyPermissionIds { get; set; }
        public List<RolePermission> RolePermission { get; set; }
        public List<VatFramework.Models.Domains.Permission.Permission> ParentMenus { get; set; }
        public List<VatFramework.Models.Domains.Permission.Permission> AllowedPermissionList { get; set; }

        [Display(Name = "Permission(s)")]
        public IEnumerable<Int64> SelectedPermissionId { get; set; }
        public IEnumerable<SelectListItem> Permissions { get; set; }

    }
}
