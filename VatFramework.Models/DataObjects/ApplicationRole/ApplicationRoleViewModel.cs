
using System;

namespace VatFramework.Models.DataObjects.ApplicationRole
{
    public class ApplicationRoleViewModel
    {
        public string ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public bool? IsActive { get; set; }
        public string Name { get; set; }
        public string DataIntegrity { get; set; }
        public string ResponseMessage { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public string ConcurrencyStamp { get; set; }
        public bool IsSysAdmin { get; set; }
        public string ParentRoleId { get; set; }


    }
}
