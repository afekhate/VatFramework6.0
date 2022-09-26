using System;
using System.Collections.Generic;
using System.Text;
using VatFramework.Models.Domains;

namespace VatFramework.Models.DataObjects.EmailMessaging
{
    public class EmailTemplateViewModel :BaseObjectDataIntegrity
    {
        public long EmailTemplateId { get; set; }
        public string Subject { get; set; }
        public String MailBody { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }

    }
}
