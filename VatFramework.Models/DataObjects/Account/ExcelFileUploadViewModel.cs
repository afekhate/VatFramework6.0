using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VatFramework.Models.DataObjects.Account
{
    public class ExcelFileUploadViewModel
    {
        [Required(ErrorMessage = "Please select an excel file to upload")]
        [Display(Name = "Import Users")]
        public IFormFile DocumentUpload { get; set; }
    }
}
