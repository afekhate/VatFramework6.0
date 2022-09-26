using System;
using VatFramework.Models.Domains;

namespace VatFramework.Models.DataObjects.ActivityLog
{
    public class ActivityLogSearchViewModel : BaseObjectDataIntegrity
    {
        public Int64? UserId { get; set; }

        public string ModuleName { get; set; }
        public string ModuleAction { get; set; }

        public string Description { get; set; }

        public string Record { get; set; }

        public string IPAddress { get; set; }
    }
}
