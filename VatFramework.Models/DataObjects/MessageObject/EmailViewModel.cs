using System;
using System.Collections.Generic;
using System.Text;
using VatFramework.Models.Domains;

namespace VatFramework.Models.DataObjects.MessageObject
{
    public class EmailViewModel : BaseObjectDataIntegrity
    {
        public string HostName { get; set; } 
        public int Port { get; set; }
        public bool Ssl { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
