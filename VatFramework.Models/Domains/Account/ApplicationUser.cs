
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VatFramework.Models.Domains.Account
{
    public class ApplicationUser : IdentityUser<string>
    {

       
       
        public ApplicationUser()
        {
            CreatedDate = DateTime.Now;
            this.ForcePwdChange = true;
            this.IsDeleted = false;
            this.IsActive = true;
        }
        public ApplicationUser(string username)
        {
            this.UserName = username;
        }

        public string FirstName { get; set; }

        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public List<ApplicationUserRole> Roles { get; set; }

        public DateTime? DOB { get; set; }

        public string MobileNumber { get; set; }


        public DateTime? ExpirationTime { get; set; }
        public DateTime? LastLoginDate { get; set; }

        public DateTime PwdExpiryDate { get; set; }
        public DateTime? PwdChangedDate { get; set; }
        public bool ForcePwdChange { get; set; }
        public DateTime? LastModified { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsActive { get; set; } = true;  
        public string RoleId { get; set; }

        public DateTime ConfirmationLinkExpireDate { get; set; }
     
    
        public string? UserToken { get; set; }

       
    }


}
