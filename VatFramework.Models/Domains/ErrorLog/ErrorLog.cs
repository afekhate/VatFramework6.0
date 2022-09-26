using System;
using System.Collections.Generic;
using System.Text;

namespace VatFramework.Models.Domains.ErrorLog
{
    public class ErrorLog :BaseObject
    {
        public string ErrorDetails { get; set; }
        public string MethodContoller { get; set; }


    }
}
