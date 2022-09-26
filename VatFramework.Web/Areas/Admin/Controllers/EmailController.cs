using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NToastNotify;
using VatFramework.Models;
using VatFramework.Services.Contract.ErrorLogger;
using VatFramework.Web.Controllers;
using VatFramework.Web.Filters;

namespace VatFramework.Web.Areas.Admin.Controllers
{

    [SessionTimeout]
    [Area("Admin")]
    public class EmailController : BaseController
    {
        private readonly log4net.Core.ILogger _logger;
        private readonly APPContext _context;
        private readonly IErrorLogger _errorLogger;
        private readonly IToastNotification _toastNotification;
        // private IEmail _emailService;

        public EmailController(ILogger<EmailController> logger,
            APPContext context, IErrorLogger errorLogger, IToastNotification toastNotification)
        {
            _context = context;
            _errorLogger = errorLogger;
            _toastNotification = toastNotification;
            // _emailService = emailService;
        }
    }
}