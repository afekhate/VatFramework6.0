using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VatFramework.Models.Domains.Account
{
    public class ApplicationUserPasswordHistory : BaseObject
    {
        public string UserId { get; set; }
        public string HashPassword { get; set; }
    }
}
