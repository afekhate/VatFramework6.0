using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VatFramework.Models.Domains.EmailTemplate
{
    public class EmailAttachment :BaseObject
    {
        public long EmailLogId { get; set; }
         public string FolderOnServer { get; set; }
         public string FileNameOnServer { get; set; }
         public string EmailFileName { get; set; }

        public virtual EmailLog EmailLog { get; set; }
    }
}
