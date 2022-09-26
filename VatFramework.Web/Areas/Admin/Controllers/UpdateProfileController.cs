

using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NToastNotify;
using VatFramework.Models;
using VatFramework.Models.DataObjects.Account;
using VatFramework.Models.Domains.Account;
using VatFramework.Services.Contract.Authenication;
using VatFramework.Services.Contract.EmailMessaging;
using VatFramework.Services.Contract.EntityService;
using VatFramework.Services.Contract.ErrorLogger;
using VatFramework.Services.Contract.UserService;
using VatFramework.Utilities.ExceptionUtility;
using VatFramework.Web.Controllers;
using VatFramework.Web.Filters;

namespace VatFramework.Web.Areas.Admin.Controllers
{

    [SessionTimeout]

    [Area("Admin")]
    public class UpdateProfileController : BaseController
    {

        private readonly ILogger _logger;
        private readonly APPContext _context;
        private readonly IErrorLogger _errorLogger;
        private readonly IToastNotification _toastNotification;
        private readonly IConfiguration _configuration;
        private readonly IRole _roleManager;
        private readonly IAuthenication _authManager;
        private readonly IUserService _userMyManager;

        private readonly IEmailMessaging _emailManager;

        protected readonly UserManager<ApplicationUser> _userInManager;
        protected readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IActivityLog _activityRepo;

        public UpdateProfileController(ILogger<UserManagementController> logger,
           APPContext context, IErrorLogger errorLogger, IToastNotification toastNotification, IUserService userManager,
           IRole roleManager, IConfiguration configuration, IEmailMessaging emailManager,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userInManager, IAuthenication authManager, IActivityLog activityRepo, IUserService userMyManager)
        {

            _logger = logger;
            _context = context;
            _errorLogger = errorLogger;
            _toastNotification = toastNotification;

            _configuration = configuration;
            _emailManager = emailManager;
            _roleManager = roleManager;
            _activityRepo = activityRepo;
            _signInManager = signInManager;
            _userInManager = userInManager;
            _authManager = authManager;
            _userMyManager = userMyManager;


        }

        //[AuthorizedAction]
        [HttpGet("UpdateMyProfile")]
        public async Task<IActionResult> Index()
        {
            try
            {

                if (CurrentUser.Email == null)
                {
                    _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.IllegalOperation, null);
                    return RedirectToAction("Login", "Account", new { area = "" });
                }


                EditViewBagParams();
                ViewBag.ControllerName = GetControllerName(this);


                var userInfo = await _userMyManager.GetUserAccountByEmail(CurrentUser.Email);

                if (userInfo == null)
                {
                    //TempData["MESSAGE"] = "Admin user not found";
                    _toastNotification.AddWarningToastMessage(ResponseErrorMessageUtility.RecordNotFound, null);
                    return RedirectToAction(nameof(Index));
                }

                var thisUserRoles = _roleManager.GetUserRoleByUserId(userInfo.Id);


                var model = new AdminUserSettingViewModel
                {
                    Id = userInfo.Id,
                    LastName = userInfo.LastName,
                    FirstName = userInfo.FirstName,
                    Email = userInfo.Email,
                    MobileNumber = userInfo.MobileNumber,
                    RoleId = userInfo.RoleId,
                    MiddleName = userInfo.MiddleName,
                    UserName = userInfo.UserName,
                    PhoneNumber = userInfo.PhoneNumber


                };
                return PartialView(model);
            }
            catch (Exception ex)
            {
                return systemError(ex);
            }
        }

        //https://qawithexperts.com/questions/123/how-to-change-password-in-aspnet-mvc


        [ValidateAntiForgeryToken()]
        [HttpPost("MyProfile")]
        public async Task<IActionResult> Update(AdminUserSettingViewModel model)
        {
            try
            {

                //Check the model state of the Request
                if (!ModelState.IsValid)
                {
                    ViewBag.ControllerName = GetControllerName(this);
                    CreateViewBagParams();
                    _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.IncompleteRequirement, null);
                    return View("Index", model);
                }


                ApplicationUser signedUser = await _userInManager.FindByEmailAsync(model.Email);

                signedUser.FirstName = model.FirstName;
                signedUser.LastName = model.LastName;
                signedUser.MiddleName = model.MiddleName;
                signedUser.MobileNumber = model.MobileNumber;


                var result = await _userInManager.UpdateAsync(signedUser);


                if (result.Succeeded)
                {

                    _toastNotification.AddSuccessToastMessage(ResponseErrorMessageUtility.ProfileUpdate, null);

                    _activityRepo.CreateActivityLog(string.Format("Update profile was successful for  : {0}",
                        signedUser.Email), getController(), getAction(), CurrentUser.UserId, signedUser);
                    return RedirectToAction("Index", "dashboard");
                }
                else
                {
                    _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.OperationFailed, null);
                    return RedirectToAction("index", "dashboard");
                }

            }
            catch (Exception ex)
            {
                return systemError(ex);

            }
        }

        public RedirectToActionResult systemError(Exception ex)
        {
            _errorLogger.LogError(ex, GetControllerAndActionName(this));
            _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.OperationFailed_Ex, null);

            ModelState.Clear();
            PasswordChangeViewModel model = new PasswordChangeViewModel();
            model.Email = CurrentUser.Email;

            return RedirectToAction(nameof(Index));
        }
    }
}