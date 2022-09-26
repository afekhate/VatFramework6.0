using System;
using System.Collections.Generic;
using System.Text;

namespace VatFramework.Models.Domains.Account
{
    public class ApplicationBulkUploadViewModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string successful { get; set; } = "Fail";

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Role { get; set; }

        public string BatchKey { get; set; }
        public string UploadStatus { get; set; } = "SUCCESS";



    }
}
