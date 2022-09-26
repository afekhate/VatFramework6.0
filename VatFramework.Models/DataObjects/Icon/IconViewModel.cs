using System;
using System.ComponentModel.DataAnnotations;
using VatFramework.Models.Domains;

namespace VatFramework.Models.DataObjects.icon
{
    public class IconViewModel : BaseObject
    {

        [Required]
        public string IconName { get; set; }
        [Required]
        public string IconCode { get; set; }
        public DateTime DateCreated { get; set; }

 

        public string DataIntegrity { get; set; }
        public string ResponseMessage { get; set; }
    }
}
