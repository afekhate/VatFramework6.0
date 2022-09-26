

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using NToastNotify;
using VatFramework.Models;
using VatFramework.Models.DataObjects.EmailMessaging;
using VatFramework.Models.DataObjects.icon;
using VatFramework.Services.Contract.EmailMessaging;
using VatFramework.Services.Contract.EntityService;
using VatFramework.Services.Contract.ErrorLogger;
using VatFramework.Services.Handler.DataAccess;
using VatFramework.Utilities.ExceptionUtility;
using VatFramework.Web.Controllers;
using VatFramework.Web.Filters;
using VatFramework.Web.Models;

namespace VatFramework.Web.Areas.Admin.Controllers
{

    [SessionTimeout]
    [Area("Admin")]
    public class EmailSetupController : BaseController
    {
        private IEmailMessaging _emailManager;
        private readonly ILogger _logger;
        private readonly APPContext _context;
        private readonly IErrorLogger _errorLogger;
        private readonly IToastNotification _toastNotification;
        public EmailSetupController(ILogger<EmailSetupController> logger,
            APPContext context, IErrorLogger errorLogger, IToastNotification toastNotification, IEmailMessaging emailManager)
        {
            _emailManager = emailManager;
            _logger = logger;
            _context = context;
            _errorLogger = errorLogger;
            _toastNotification = toastNotification;
        }

        [AuthorizedAction]
        [HttpGet("EmailSetup")]
        public async Task<IActionResult> Index()
        {

            try
            {
                ViewBag.ControllerName = GetControllerName(this);

                // Get List of items
                ListViewBagParams();

                var result = await _emailManager.GetAllEmailTemplates(null);

                if (result.Where(a => a.DataIntegrity == ResponseErrorMessageUtility.DataIntegrity_OK).Count() > 0)
                {
                    //_toastNotification.AddInfoToastMessage(ResponseErrorMessageUtility.RecordFetched, null);
                    return View(result);
                }
                else
                {

                    _toastNotification.AddInfoToastMessage(ResponseErrorMessageUtility.RecordNotFound, null);
                    result.Clear();
                    return View(result);
                }

            }
            catch (Exception ex)
            {
                return systemError(ex);

            }


        }



        #region Create Operation
        [HttpGet("EmailSetup/Create")]

        public IActionResult Create()
        {
            try
            {
                ViewBag.ControllerName = GetControllerName(this);
                CreateViewBagParams();

                return PartialView("_PartialAddEdit");
            }
            catch (Exception ex)
            {
                return systemError(ex);
            }
        }


        [HttpPost("EmailSetup/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmailTemplateViewModel model)
        {
            ViewBag.ControllerName = GetControllerName(this);
            try
            {
                //Check the model state of the Request
                if (!ModelState.IsValid)
                {

                    ListViewBagParams();
                    _toastNotification.AddWarningToastMessage(ResponseErrorMessageUtility.RequiredFields, null);
                    return PartialView("_PartialAddEdit", model);
                }

                model.CreatedBy = CurrentUser.UserId;
                model.CreatedDate = DateTime.Now;
                model.IsActive = true;
                var response = await _emailManager.CreateEmailTemplate(model, getController(), getAction());


                if (!string.IsNullOrEmpty(response) && response != ResponseErrorMessageUtility.SuccessfulAndUpdated)
                {

                    _toastNotification.AddWarningToastMessage(response, null);
                    return RedirectToAction(nameof(Index));
                }

                _toastNotification.AddSuccessToastMessage(ResponseErrorMessageUtility.succesful, null);

                ModelState.Clear();


                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return systemError(ex);
            }
        }


        #endregion


        #region Edit Operation
        [HttpPost("EmailSetup/Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmailTemplateViewModel model)
        {



            ViewBag.ControllerName = GetControllerName(this);
            //Check the model state of the Request
            if (!ModelState.IsValid)
            {
                EditViewBagParams();


                return PartialView("_PartialAddEdit", model);
            }


            //pass the values to be created to the servicedel
            model.LastModified = DateTime.Now;
            model.CreatedBy = CurrentUser.UserId;

            var response = await _emailManager.UpdateEmailTemplate(model, getController(), getAction());



            if (!string.IsNullOrEmpty(response) && response != ResponseErrorMessageUtility.SuccessfulAndUpdated)
            {
                _toastNotification.AddWarningToastMessage(response, null);
                return RedirectToAction(nameof(Index));
            }


            _toastNotification.AddSuccessToastMessage(ResponseErrorMessageUtility.UpdateRecord, null);

            ModelState.Clear();

            return RedirectToAction(nameof(Index));
        }




        [HttpGet("EmailSetup/Edit")]

        public async Task<IActionResult> Edit(string Id)
        {
            if (Id == "")
            {
                _toastNotification.AddWarningToastMessage(ResponseErrorMessageUtility.IllegalOperation, null);
                return RedirectToAction(nameof(Index));
            }

            string decryptBatchKey = Id.Decrypt();



            ViewBag.ControllerName = GetControllerName(this);
            try
            {
                EditViewBagParams();

                var response = await _emailManager.GetEmailTemplateById(Convert.ToInt64(decryptBatchKey), getController(), getAction());

                return PartialView("_PartialAddEdit", response);
            }
            catch (Exception ex)
            {
                return systemError(ex);
            }
        }
        #endregion

        #region Delete Operation
        [HttpGet("EmailSetup/Delete")]
        public async Task<ActionResult> Delete(long Id)
        {
            ViewBag.ControllerName = GetControllerName(this);

            try
            {
                DeleteViewBagParams();

                var response = await _emailManager.GetEmailTemplateById(Id, getController(), getAction());


                return PartialView("Delete", response);
            }
            catch (Exception ex)
            {

                return systemError(ex);

            }


        }


        [HttpPost("EmailSetup/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int ID)
        {

            if (!ModelState.IsValid)
            {
                DeleteViewBagParams();

                _toastNotification.AddWarningToastMessage(ResponseErrorMessageUtility.RecordNotFound, null);
                return RedirectToAction(nameof(Index));
            }
            string ModifiedBy = CurrentUser.UserId;
            bool response = await _emailManager.DeleteEmailTemplate(ID, getController(), getAction(), CurrentUser.UserId);


            if (response == false)
            {

                _toastNotification.AddWarningToastMessage(ResponseErrorMessageUtility.DeleteOperationFail, null);

                return RedirectToAction(nameof(Index));
            }



            _toastNotification.AddSuccessToastMessage(ResponseErrorMessageUtility.DeleteOperation, null);

            ModelState.Clear();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Helpers Operation
        public RedirectToActionResult systemError(Exception ex)
        {
            _errorLogger.LogError(ex, GetControllerAndActionName(this));
            _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.OperationFailed_Ex, null);

            ModelState.Clear();

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }

}