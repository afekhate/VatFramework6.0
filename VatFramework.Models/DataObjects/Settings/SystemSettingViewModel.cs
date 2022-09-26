using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VatFramework.Models.Domains;

namespace VatFramework.Models.DataObjects.Settings
{
    public class SystemSettingViewModel : BaseObjectDataIntegrity
    { 
        [Required]
        public string LookUpCode { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public string ItemValue { get; set; }
    }
}
