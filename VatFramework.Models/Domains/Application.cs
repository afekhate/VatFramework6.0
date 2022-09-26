using System;

namespace VatFramework.Models.Domains
{
    public class Application : BaseObject
    {
        public string ApplicationName { get; set; }
        public string Description { get; set; }

        public string TermsAndConditions { get; set; }

        public Boolean HasAdminUserConfigured { get; set; }
    }
}
