

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VatFramework.Models.Domains.Account
{
    public class ApplicationRole : IdentityRole<string>
    {
        public ApplicationRole()
        {

        }
        public string Id { get; set; }

        public virtual ICollection<ApplicationUserRole> Users { get; } = new List<ApplicationUserRole>();

        public string RoleName { get; set; }
       public string RoleDescription { get; set; }

        public bool IsSysAdmin { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;   
       public string? CreatedBy { get; set; }
       public string? ModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public bool? IsActive { get; set; }
   
    }
}
