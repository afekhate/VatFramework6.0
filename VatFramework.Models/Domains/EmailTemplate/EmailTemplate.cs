using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VatFramework.Models.Domains;

namespace VatFramework.Models.Domains.EmailTemplate
{
   public  class EmailTemplate :BaseObject
    {
       
        [Required(ErrorMessage = "Subject is required")]
        [StringLength(500)]
        public string Subject { get; set; }

      
        [Required(ErrorMessage = "Body is required")]
        public string MailBody { get; set; }


  
        public string Description { get; set; }

 
        public string Code { get; set; }
    }
}
