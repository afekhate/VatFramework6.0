using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using VatFramework.Models;
using VatFramework.Models.Domains.ErrorLog;
using VatFramework.Services.Contract.EntityService;
using VatFramework.Services.Contract.ErrorLogger;
using VatFramework.Utilities.AuthenticationUtility.AuthUser;

namespace VatFramework.Services.Handler.ErrorLogger
{
   public class ErrorLoggerService : IErrorLogger
    {
        private readonly APPContext _context;
        

        public ErrorLoggerService(APPContext context
        )
        {
            _context = context;
          
        }

        public void LogError(Exception ex, string contollerOrMethodName)
        {
            ErrorLog newrecord = new ErrorLog
            {
                CreatedDate = DateTime.Now,
                MethodContoller = contollerOrMethodName,
                ErrorDetails = ex.InnerException == null ? ("<strong>Error Message:</strong><br/>" + ex.Message + "<br/><strong>StackTrace:</strong><br/>" + ex.StackTrace)
                : "<strong>Error Message:</strong><br/>" + ex.Message + "<br/><strong>StackTrace:</strong><br/>" + ex.StackTrace +
                "<br/><br/><strong>Inner Exception Message:</strong><br/>" + ex.InnerException.Message + "<br/><strong>Inner Exception StackTrace:</strong><br/>" + ex.InnerException.StackTrace
            };

            _context.ErrorLogs.Add(newrecord);
            _context.SaveChanges();

        }
    }
}
