using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using VatFramework.Utilities;

namespace VatFramework.Models.Domains
{
    public class BaseObject
    {
        public long ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public string? IPAddress { get; set; } = Common.GetLocalIPAddress();
    }

    public class BaseObjectDataIntegrity
    {
        public long ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public bool IsActive { get; set; }
        public string DataIntegrity { get; set; }
        public string ResponseMessage { get; set; }

    }

    

   


}
