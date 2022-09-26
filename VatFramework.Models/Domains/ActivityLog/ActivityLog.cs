using System;
using System.ComponentModel.DataAnnotations.Schema;
using VatFramework.Models.Domains.Account;

namespace VatFramework.Models.Domains.ActivityLog
{
    public class ActivityLog : BaseObject
    {
        public string? UserId { get; set; }

         public string ModuleName { get; set; }
         public string ModuleAction { get; set; }

         public string Description { get; set; }

         public string Record { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
     
    public class ActivityInfo
    {
        public Int64 Id { get; set; }
        public string FullName { get; set; }
        public Int64? UserID { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public string ModuleName { get; set; }
        public string ModuleAction { get; set; }
        public string Record { get; set; }
    }

}