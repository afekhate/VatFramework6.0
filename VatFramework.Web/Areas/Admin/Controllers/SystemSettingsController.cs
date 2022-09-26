using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NToastNotify;
using VatFramework.Models;
using VatFramework.Models.DataObjects.Settings;
using VatFramework.Services.Contract.ErrorLogger;
using VatFramework.Services.Contract.Settings;
using VatFramework.Utilities.ExceptionUtility;
using VatFramework.Web.Controllers;
using VatFramework.Web.Filters;

namespace VatFramework.Web.Areas.Admin.Controllers
{
    [SessionTimeout]
    [Area("Admin")]
    public class SystemSettingsController : BaseController
    {
        private ISystemSetting _systemSettingService;
        private readonly ILogger _logger;
        private readonly APPContext _context;
        private readonly IErrorLogger _errorLogger;
        private readonly IToastNotification _toastNotification;
        public SystemSettingsController(ISystemSetting systemSettingService, ILogger<SystemSettingsController> logger,
            APPContext context, IErrorLogger errorLogger, IToastNotification toastNotification)
        {
            _systemSettingService = systemSettingService;

            _context = context;
            _errorLogger = errorLogger;
            _toastNotification = toastNotification;
        }


        #region Index
        [AuthorizedAction]
        [HttpGet("SystemSettings")]
        public async Task<IActionResult> Index()
        {

            try
            {
                ViewBag.ControllerName = GetControllerName(this);
                // Get List of items
                ListViewBagParams();


                var result = await _systemSettingService.GetSystemSettings(null);



                if (result.Where(a => a.DataIntegrity == ResponseErrorMessageUtility.DataIntegrity_OK).Count() > 0)
                {

                    //_toastNotification.AddInfoToastMessage(ResponseErrorMessageUtility.RecordFetched, null);
                    return View(result.OrderBy(x => x.LookUpCode));
                }
                else
                {

                    _toastNotification.AddInfoToastMessage(ResponseErrorMessageUtility.RecordNotFound, null);
                    // result.Clear();
                    return View(result.OrderBy(x => x.LookUpCode));
                }

            }
            catch (Exception ex)
            {
                return systemError(ex);

            }


        }
        #endregion


        #region Create Operation
        [HttpGet("SystemSettings/Create")]

        public IActionResult Create()
        {
            ViewBag.ControllerName = GetControllerName(this);
            try
            {

                CreateViewBagParams();

                return PartialView("_PartialAddEdit");
            }
            catch (Exception ex)
            {
                return systemError(ex);
            }
        }


        [HttpPost("SystemSettings/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LookUpCode,ItemName,ItemValue,ID,IsActive")] SystemSettingViewModel model)
        {
            ViewBag.ControllerName = GetControllerName(this);
            try
            {
                //Check the model state of the Request
                if (!ModelState.IsValid)
                {

                    ListViewBagParams();
                    string msg = ModelState.FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors.FirstOrDefault().ErrorMessage;
                    string displayMsg = msg.Replace("'", "");
                    _toastNotification.AddWarningToastMessage(displayMsg, null);
                    //_toastNotification.AddWarningToastMessage(ResponseErrorMessageUtility.RequiredFields, null);
                    //return PartialView("_PartialAddEdit", model);
                    return RedirectToAction(nameof(Index));
                }


                var response = await _systemSettingService.CreateSystemSetting(model, getController(), getAction());


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
        [HttpPost("SystemSettings/Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("LookUpCode,ItemName,ItemValue,ID,IsActive")] SystemSettingViewModel model)
        {
            ViewBag.ControllerName = GetControllerName(this);
            //Check the model state of the Request
            if (!ModelState.IsValid)
            {
                EditViewBagParams();
                string msg = ModelState.FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors.FirstOrDefault().ErrorMessage;
                string displayMsg = msg.Replace("'", "");
                _toastNotification.AddWarningToastMessage(displayMsg, null);
                return RedirectToAction(nameof(Index));
                //return PartialView("_PartialAddEdit", model);
            }


            //pass the values to be created to the servicedel
            model.LastModified = DateTime.Now;
            model.CreatedBy = CurrentUser.UserId;

            var response = await _systemSettingService.UpdateSystemSetting(model, (int)model.ID, getController(), getAction());



            if (!string.IsNullOrEmpty(response) && response != ResponseErrorMessageUtility.SuccessfulAndUpdated)
            {
                _toastNotification.AddWarningToastMessage(response, null);
                return RedirectToAction(nameof(Index));
            }


            _toastNotification.AddSuccessToastMessage(ResponseErrorMessageUtility.UpdateRecord, null);

            ModelState.Clear();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet("SystemSettings/Edit")]

        public async Task<IActionResult> Edit(long Id)
        {
            try
            {
                EditViewBagParams();
                ViewBag.ControllerName = GetControllerName(this);
                var response = await _systemSettingService.GetSystemSettingById(Id, getController(), getAction());

                return PartialView("_PartialAddEdit", response);
            }
            catch (Exception ex)
            {
                return systemError(ex);
            }
        }
        #endregion


        #region Delete Operation
        [HttpGet("SystemSettings/Delete")]
        public async Task<ActionResult> Delete(long Id)
        {
            ViewBag.ControllerName = GetControllerName(this);
            try
            {
                DeleteViewBagParams();

                var response = await _systemSettingService.GetSystemSettingById(Id, getController(), getAction());


                return PartialView("Delete", response);
            }
            catch (Exception ex)
            {

                return systemError(ex);

            }


        }


        [HttpPost("SystemSettings/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int ID)
        {
            ViewBag.ControllerName = GetControllerName(this);
            if (!ModelState.IsValid)
            {
                DeleteViewBagParams();

                _toastNotification.AddWarningToastMessage(ResponseErrorMessageUtility.RecordNotFound, null);
                return RedirectToAction(nameof(Index));
            }
            string ModifiedBy = "";
            bool response = await _systemSettingService.DeleteSystemSetting(ID, ModifiedBy, getController(), getAction());


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