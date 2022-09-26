
using System;
using System.Collections.Generic;
using System.IO;
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
using VatFramework.Services.Handler.EmailMessaging;
using VatFramework.Utilities.ExceptionUtility;
using VatFramework.Web.Filters;
using VatFramework.Web.Models;
using Microsoft.AspNetCore.Http;
using VatFramework.Services.FileHandler;
using System.Data;
using Newtonsoft.Json;
using VatFramework.Models.DataObjects.Icon;
using System.Text;
using VatFramework.Models.DataObjects.ApplicationRole;
using VatFramework.Services.Handler.DataAccess;
using VatFramework.Models.Domains.Permission;
using VatFramework.Models.Domains;
using VatFramework.Web.Controllers;

namespace VatFramework.Web.Areas.Admin.Controllers
{

    [SessionTimeout]
    [Area("Admin")]
    public class UserManagementController : BaseController
    {
        private IIconService _iconService;
        private readonly UserManager<ApplicationUser> _userManagerPassword;
        private readonly ILogger _logger;
        private readonly APPContext _context;
        private readonly IErrorLogger _errorLogger;
        private readonly IToastNotification _toastNotification;
        private readonly IUserService _userManager;
        private readonly IConfiguration _configuration;
        private readonly IRole _roleManager;
        private readonly IAuthenication _authManager;
        private readonly IActivityLog _activityRepo;
        private readonly IFileHandler _filehandler;
        private readonly IEmailMessaging _emailManager;

        protected readonly UserManager<ApplicationUser> _userInManager;

        protected readonly SignInManager<ApplicationUser> _signInManager;
        public UserManagementController(IIconService iconService, ILogger<UserManagementController> logger,
            APPContext context, IErrorLogger errorLogger, IToastNotification toastNotification, IUserService userManager,
            UserManager<ApplicationUser> userManagerPassword,
            IRole roleManager, IConfiguration configuration, IEmailMessaging emailManager,
             SignInManager<ApplicationUser> signInManager,
             UserManager<ApplicationUser> userInManager, IAuthenication authManager, IActivityLog activityRepo,
             IFileHandler fileHandler)
        {
            _iconService = iconService;
            _userManagerPassword = userManagerPassword;
            _logger = logger;
            _context = context;
            _errorLogger = errorLogger;
            _toastNotification = toastNotification;
            _userManager = userManager;
            _configuration = configuration;
            _emailManager = emailManager;
            _roleManager = roleManager;

            _signInManager = signInManager;
            _userInManager = userInManager;

            _authManager = authManager;
            _activityRepo = activityRepo;
            _filehandler = fileHandler;


        }

        [AuthorizedAction]
        [HttpGet("UserManagement")]
        public async Task<IActionResult> Index()
        {


            try
            {

                // Get List of items
                ListViewBagParams();
                ViewBag.ControllerName = GetControllerName(this);

                var d = CurrentUser.RoleId.ToLower();
                var result = await _userManager.GetAllUserAccounts(null);

                if (CurrentUser.RoleId == ResponseErrorMessageUtility.SystemAdministrator)
                {
                    return View(result);
                }

                else if (CurrentUser.RoleId == ResponseErrorMessageUtility.CentralAdmin)
                {


                    return View(result.Where(a => a.RoleId == ResponseErrorMessageUtility.CentralAdmin || a.RoleId == ResponseErrorMessageUtility.PlantAdmin).ToList());
                }

                else if (CurrentUser.RoleId == ResponseErrorMessageUtility.PlantAdmin)
                {


                    var data = result.Where(a => a.PlantId == Convert.ToInt64(CurrentUser.PlantId) && a.MiniPlantIdd != 1000).ToList();


                    return View(data);
                }

                else if (CurrentUser.RoleId == ResponseErrorMessageUtility.MiniAdmin)
                {
                    return View(result.Where(a => a.Id == CurrentUser.UserId || a.PlantId == Convert.ToInt64(CurrentUser.PlantId) && a.MiniPlantIdd == Convert.ToInt64(CurrentUser.MiniPlantId)).ToList());
                }
                else
                {
                    return View(null);
                }
            }
            catch (Exception ex)
            {
                return systemError(ex);

            }


        }



        [HttpGet("DeletedUserManagement")]
        public async Task<IActionResult> GetDeleted()
        {

            try
            {

                // Get List of items
                ListViewBagParams();

                var result = await _iconService.GetDeletedIncons();



                if (result.Where(a => a.DataIntegrity == ResponseErrorMessageUtility.DataIntegrity_OK).Count() > 0)
                {

                    //_toastNotification.AddInfoToastMessage(ResponseErrorMessageUtility.RecordFetched, null);
                    return View(nameof(Index), result);
                }
                else
                {

                    _toastNotification.AddInfoToastMessage(ResponseErrorMessageUtility.RecordNotFound, null);
                    result.Clear();
                    return View(nameof(Index), result);
                }

            }
            catch (Exception ex)
            {
                return systemError(ex);

            }


        }

        #region Create Operation
        [HttpGet("UserManagement/Create")]

        public async Task<IActionResult> Create()
        {
            try
            {

                CreateViewBagParams();
                ViewBag.ControllerName = GetControllerName(this);

                var viewList = await LoadSpecificData();



                // var viewList = await _roleManager.GetRoles(null);

                var model = new AdminUserSettingViewModel
                {
                    RoleList = viewList.Select(u => new SelectListItem { Text = u.Name, Value = u.ID.ToString() }).ToList(),
                    LoggedRoleName = CurrentUser.RoleId
                };

                return PartialView("_PartialAddEdit", model);
            }
            catch (Exception ex)
            {
                return systemError(ex);
            }
        }


        [HttpPost("UserManagement/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminUserSettingViewModel model)
        {
            try
            {
                ViewBag.ControllerName = GetControllerName(this);
                //Check the model state of the Request
                if (!ModelState.IsValid)
                {

                    ListViewBagParams();
                    _toastNotification.AddWarningToastMessage(ResponseErrorMessageUtility.RequiredFields, null);



                    return PartialView("_PartialAddEdit", model);
                }

                if (string.Compare(model.Password, model.ConfirmPassword,
                       StringComparison.InvariantCultureIgnoreCase) != 0)
                {

                    _toastNotification.AddWarningToastMessage(ResponseErrorMessageUtility.PasswordNotMatch, null);
                    return PartialView("_PartialAddEdit", model);

                }



                var userRoles = await _roleManager.GetRoles(null);







                // the 1000 is for central store admin
                if (CurrentUser.RoleId == ResponseErrorMessageUtility.SystemAdministrator)
                {
                    model.PlantId = 1000;
                    model.MiniPlantIdd = 1000;

                };

                if (CurrentUser.RoleId == ResponseErrorMessageUtility.CentralAdmin)
                {
                    if (model.RoleId != ResponseErrorMessageUtility.PlantAdmin)
                    {
                        model.MiniPlantIdd = Convert.ToInt64(CurrentUser.MiniPlantId);
                        model.PlantId = Convert.ToInt64(CurrentUser.PlantId);
                    }
                    else
                    {
                        model.MiniPlantIdd = 0;
                    }

                };

                if (CurrentUser.RoleId == ResponseErrorMessageUtility.PlantAdmin)
                {
                    if (model.RoleId != ResponseErrorMessageUtility.MiniAdmin)
                    {
                        model.MiniPlantIdd = Convert.ToInt64(CurrentUser.MiniPlantId);
                        model.PlantId = Convert.ToInt64(CurrentUser.PlantId);
                    }
                    else
                    {
                        model.MiniPlantIdd = model.PlantId;
                        model.PlantId = Convert.ToInt64(CurrentUser.PlantId);
                    }

                };

                if (CurrentUser.RoleId == ResponseErrorMessageUtility.MiniAdmin)
                {
                    model.MiniPlantIdd = Convert.ToInt64(CurrentUser.MiniPlantId);
                    model.PlantId = Convert.ToInt64(CurrentUser.PlantId);

                };

                model.Password = Utilities.PasswordGenerator.GenerateRandomPassword(null);
                model.CreatedBy = CurrentUser.UserId;
                string response = await _userManager.CreateUser(model, getController(), getAction());


                if (!string.IsNullOrEmpty(response) && response == ResponseErrorMessageUtility.SuccessfulAndUpdated)
                {

                    var userInfo = await _userManager.GetUserAccountByEmail(model.Email.ToLower());

                    ApplicationUserPasswordHistory passwordModel = new ApplicationUserPasswordHistory();
                    passwordModel.UserId = userInfo.Id;
                    passwordModel.CreatedDate = DateTime.Now;
                    passwordModel.HashPassword = model.Password.Encrypt();
                    passwordModel.CreatedBy = CurrentUser.UserId;

                    _context.ApplicationUserPasswordHistorys.Add(passwordModel);
                    _context.SaveChanges();

                    var resut = userRoles.ToList().Where(r => r.ID.ToString() == model.RoleId.ToString()).ToList();
                    string roleResult = await _roleManager.MapUsersToRoles(userInfo.Id, resut, getController(), getAction());


                    // SEND EMAIL 
                    var emailtemplate = await _emailManager.GetEmailTemplateByCode(EmailTemplateCode.ACCOUNT_VERIFICATION);

                    var getUpdateUserDetails = await _userInManager.FindByEmailAsync(model.Email);

                    string confirmationToken = _userInManager.GenerateEmailConfirmationTokenAsync(getUpdateUserDetails).Result;

                    string confirmationLink = Url.Action("ConfirmEmail", "Account", new { userid = getUpdateUserDetails.Id, token = confirmationToken }, protocol: HttpContext.Request.Scheme);

                    var emailTokens = new List<EmailTokenViewModel>
                        {
                            new EmailTokenViewModel {  Token = EmailTokenConstants.FULLNAME,  TokenValue = model.FirstName +  ' ' + model.LastName },
                            new EmailTokenViewModel {  Token = EmailTokenConstants.PORTALNAME,  TokenValue = _configuration["PortalName"] },
                            new EmailTokenViewModel {  Token = EmailTokenConstants.ACCOUNT_VERIFICATION_LINK,  TokenValue = confirmationLink },
                            new EmailTokenViewModel {  Token = EmailTokenConstants.LogoURL,  TokenValue = _configuration["LogoURL"] },
                            new EmailTokenViewModel {  Token = EmailTokenConstants.DURATION,  TokenValue = model.ExpirationTime.ToString() }


                        };

                    bool mailresponse = await _emailManager.PrepareEmailLog(EmailTemplateCode.ACCOUNT_VERIFICATION, model.Email,
                                                        _configuration["cc"], "", emailTokens, CurrentUser.UserId, false);


                    _toastNotification.AddSuccessToastMessage(ResponseErrorMessageUtility.succesful, null);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toastNotification.AddWarningToastMessage(response, null);
                    return RedirectToAction(nameof(Index));

                }


            }
            catch (Exception ex)
            {
                return systemError(ex);
            }
        }


        #endregion




        #region Edit Operation
        [HttpPost("UserManagement/Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminUserSettingViewModel model)
        {

            try
            {


                //Check the model state of the Request
                if (!ModelState.IsValid)
                {
                    ViewBag.ControllerName = GetControllerName(this);
                    EditViewBagParams();
                    return RedirectToAction(nameof(Index));
                }

                var applicationUser = await _userManager.GetUserAccountById(model.Id);
                var applicationEmailCheck = await _userManager.GetUserAccountByEmail(model.Email);

                if (applicationUser == null)
                {
                    //TempData["MESSAGE"] = "Admin user not found";
                    _toastNotification.AddWarningToastMessage(ResponseErrorMessageUtility.RecordNotFound, null);
                    return RedirectToAction(nameof(Index));
                }


                var content = _context.ApplicationUsers.Where(a => a.Email.ToLower() == model.Email.ToLower() && a.Id != model.Id);



                if (content.Count() > 0)
                {

                    _toastNotification.AddWarningToastMessage(ResponseErrorMessageUtility.RecordExistBefore.Replace("{0}", model.Email), null);
                    return RedirectToAction(nameof(Index));
                }



                // update the records 
                var userRoles = await _roleManager.GetRoles(null);
                var roles = userRoles.ToList().Where(r => r.ID.ToString() == model.RoleId.ToString()).ToList();

                model.ModifiedBy = CurrentUser.UserId;
                model.PhoneNumber = model.MobileNumber;
                model.UserName = applicationUser.UserName;
                model.CreatedBy = CurrentUser.UserId;
                string response = await _userManager.UpdateUser(model, roles, getController(), getAction());


                if (!string.IsNullOrEmpty(response) && response != ResponseErrorMessageUtility.SuccessfulAndUpdated)
                {

                    _toastNotification.AddWarningToastMessage(response, null);
                    return RedirectToAction(nameof(Index));
                }


                _toastNotification.AddSuccessToastMessage(ResponseErrorMessageUtility.UpdateRecord, null);

                ModelState.Clear();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return RedirectToAction(nameof(Index));
            }
        }



        [HttpGet("UserManagement/Edit")]

        public async Task<IActionResult> Edit(string Id)
        {
            try
            {
                EditViewBagParams();
                ViewBag.ControllerName = GetControllerName(this);



                var userInfo = await _userManager.GetUserAccountByUserId(Id);

                var viewList = await LoadSpecificData();

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
                    RoleList = viewList.Select(u => new SelectListItem { Text = u.Name, Value = u.ID.ToString() }).ToList(),

                    IsActive = userInfo.IsActive,
                    MiddleName = userInfo.MiddleName,
                    UserName = userInfo.UserName,
                    PhoneNumber = userInfo.PhoneNumber,
                    LoggedRoleName = CurrentUser.RoleId

                };
                return PartialView("_PartialAddEdit", model);
            }
            catch (Exception ex)
            {
                return systemError(ex);
            }
        }
        #endregion

        #region Delete Operation
        [HttpGet("UserManagement/Delete")]
        public async Task<ActionResult> Delete(long Id)
        {

            try
            {
                DeleteViewBagParams();
                ViewBag.ControllerName = GetControllerName(this);
                var response = await _iconService.GetIconById(Id, getController(), getAction());


                return PartialView("Delete", response);
            }
            catch (Exception ex)
            {

                return systemError(ex);

            }


        }


        [HttpPost("UserManagement/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int ID)
        {

            if (!ModelState.IsValid)
            {
                DeleteViewBagParams();

                _toastNotification.AddWarningToastMessage(ResponseErrorMessageUtility.RecordNotFound, null);
                return RedirectToAction(nameof(Index));
            }
            string ModifiedBy = "";
            bool response = await _iconService.DeleteIcon(ID, ModifiedBy, getController(), getAction());


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

        #region Reset User Password
        [HttpGet("UserManagement/ResetPassword")]
        public async Task<ActionResult> ResetPassword(string Id)
        {

            try
            {
                ResetPasswordViewBagParams();
                ViewBag.ControllerName = GetControllerName(this);

                var response = await _userManager.GetUserAccountById(Id);


                return PartialView("ResetPassword", response);
            }
            catch (Exception ex)
            {

                return systemError(ex);

            }
        }


        [HttpPost("UserManagement/ResetPassword")]
        public async Task<IActionResult> ResetPassword(string Id, string Email)
        {

            //Check the model state of the Request
            if (!ModelState.IsValid)
            {
                _toastNotification.AddInfoToastMessage(ResponseErrorMessageUtility.RecordNotFound, null);
                return RedirectToAction(nameof(Index));


            }

            // confirm password check



            //check old password with email address
            ApplicationUser signedUser = await _userManagerPassword.FindByEmailAsync(Email);


            if (signedUser == null)
            {
                _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.IllegalOperation, null);
                return RedirectToAction(nameof(Index));

            }



            string confirmationLink = await _userManagerPassword.GeneratePasswordResetTokenAsync(signedUser);


            if (confirmationLink != null)
            {

                // SEND EMAIL 
                var emailtemplate = await _emailManager.GetEmailTemplateByCode(EmailTemplateCode.FORGOT_PASSWORD);

                var expiredDate = Convert.ToInt32(_configuration["PwdExpireDate"]);
                string ExpirationTime = DateTime.Now.AddMinutes(expiredDate).ToString();

                ExpirationTime = ExpirationTime.Encrypt();
                string resetConfirmationLink = Url.Action("ResetPassword", "Account", new { userid = signedUser.Id, token = confirmationLink, data = ExpirationTime }, protocol: HttpContext.Request.Scheme);

                var emailTokens = new List<EmailTokenViewModel>
                        {
                            new EmailTokenViewModel {  Token = EmailTokenConstants.FULLNAME,  TokenValue = signedUser.FirstName +  ' ' + signedUser.LastName },
                            new EmailTokenViewModel {  Token = EmailTokenConstants.PORTALNAME,  TokenValue = _configuration["PortalName"] },
                            new EmailTokenViewModel {  Token = EmailTokenConstants.USERNAME,  TokenValue = signedUser.Email },
                            new EmailTokenViewModel {  Token = EmailTokenConstants.ACCOUNT_VERIFICATION_LINK,  TokenValue = resetConfirmationLink },
                            new EmailTokenViewModel {  Token = EmailTokenConstants.LogoURL,  TokenValue = _configuration["LogoURL"] }
                        };

                bool mailresponse = await _emailManager.PrepareEmailLog(EmailTemplateCode.FORGOT_PASSWORD, signedUser.Email,
                                                    _configuration["cc"], "", emailTokens, CurrentUser.UserId, false);

                if (mailresponse == true)
                {

                    _toastNotification.AddSuccessToastMessage(ResponseErrorMessageUtility.PasswordChanged, null);

                    _activityRepo.CreateActivityLog(string.Format("Password Reset link successfully  for  : {0} was successfully generated by super administrator",
                        signedUser.Email), getController(), getAction(), CurrentUser.UserId, signedUser);

                    _toastNotification.AddInfoToastMessage(ResponseErrorMessageUtility.RecordNotFound, null);
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.OperationFailed, null);


                    _toastNotification.AddInfoToastMessage(ResponseErrorMessageUtility.RecordNotFound, null);
                    return RedirectToAction(nameof(Index));

                }

            }
            else
            {

                _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.Errorconfirming, null);
                return RedirectToAction(nameof(Index));

            }
        }
        #endregion

        #region FileUpload & Template download




        [HttpGet("UserManagement/Import")]

        //public async Task<IActionResult> Import()
        //{
        //    try
        //    {
        //        var model = new ExcelFileUploadViewModel();

        //        ImportViewBagParams();
        //        ViewBag.ControllerName = GetControllerName(this);
        //        return PartialView("Import", model);
        //    }
        //    catch (Exception ex)
        //    {
        //        return systemError(ex);
        //    }
        //}


        [HttpGet("ImportUser")]
        public async Task<IActionResult> ImportUser()
        {


            try
            {
                // Get List of items
                ListViewBagParams();
                ViewBag.ControllerName = GetControllerName(this);

                var result = await _userManager.GetAllUserAccounts(null);

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



        [HttpGet("UserManagement/Upload")]
        public async Task<IActionResult> Upload(string BatchKey)
        {
            var model = new ExcelFileUploadViewModel();

            try
            {

                if (BatchKey == "" || BatchKey == null)
                {

                    _toastNotification.AddWarningToastMessage(ResponseErrorMessageUtility.IllegalOperation, null);
                    ImportViewBagParams();
                    ViewBag.ControllerName = GetControllerName(this);
                    return PartialView("Import", model);
                }

                string decryptBatchKey = BatchKey.Decrypt();


                var getBatchItems = HttpContext.Session.GetObjectFromJson<List<ApplicationBulkUploadViewModel>>("bulkList");
                var successfulRecords = getBatchItems.Where(a => a.BatchKey == decryptBatchKey && a.successful == ResponseErrorMessageUtility.SUCCESS).ToList();


                var UploadedRecords = await ProcessBulkUserCreation(successfulRecords);


                ImportViewBagParams();
                ViewBag.ControllerName = GetControllerName(this);

                return View(UploadedRecords);
            }
            catch (Exception ex)
            {
                return systemError(ex);
            }
        }


        [HttpGet("UserManagement/Final")]
        public async Task<IActionResult> Final(List<ApplicationBulkUploadViewModel> model)
        {


            try
            {
                // Get List of items

                return View(model);


            }
            catch (Exception ex)
            {
                return systemError(ex);

            }


        }

        private async Task<List<ApplicationBulkUploadViewModel>> ProcessBulkUserCreation(List<ApplicationBulkUploadViewModel> models)
        {
            var UploadResult = new List<ApplicationBulkUploadViewModel>();

            var userRoles = await _roleManager.GetRoles(null);


            try
            {

                foreach (var model in models)
                {
                    var Roles = userRoles.Where(a => a.RoleName.ToLower() == model.Role.ToLower()).ToList();
                    var RoleId = userRoles.Where(a => a.RoleName.ToLower() == model.Role.ToLower()).FirstOrDefault().ID;
                    var expiredDate = Convert.ToInt32(_configuration["PwdExpireDate"]);
                    string ExpirationTime = DateTime.Now.AddMinutes(expiredDate).ToString();

                    var user = new ApplicationUser
                    {
                        FirstName = model.FirstName.Trim(),
                        LastName = model.LastName.Trim(),
                        PhoneNumber = model.MobileNumber,
                        Email = model.Email.Trim(),
                        MobileNumber = model.MobileNumber,
                        EmailConfirmed = false,
                        PhoneNumberConfirmed = true,
                        TwoFactorEnabled = false,
                        LockoutEnabled = false,
                        AccessFailedCount = 0,
                        CreatedBy = CurrentUser.UserId,
                        CreatedDate = DateTime.Now,
                        ForcePwdChange = true,
                        RoleId = RoleId,
                        IsActive = true,

                        //  ResponderId = model.ResponderId,
                        // LGAID = model.LGAId
                    };

                    var Password = Utilities.PasswordGenerator.GenerateRandomPassword(null);

                    var userModel = new AdminUserSettingViewModel
                    {
                        FirstName = model.FirstName.Trim(),
                        LastName = model.LastName.Trim(),
                        PhoneNumber = model.MobileNumber,
                        Email = model.Email.Trim(),
                        MobileNumber = model.MobileNumber,
                        CreatedBy = CurrentUser.UserId,
                        CreatedDate = DateTime.Now,
                        ForcePwdChange = true,
                        RoleId = RoleId,
                        MiddleName = model.MiddleName,
                        PwdExpiryDate = DateTime.Now.AddDays(90),
                        RoleName = model.Role,
                        IsActive = true,


                    };


                    string response = await _userManager.CreateUser(userModel, getController(), getAction());


                    if (!string.IsNullOrEmpty(response) && response == ResponseErrorMessageUtility.SuccessfulAndUpdated)
                    {

                        var userInfo = await _userManager.GetUserAccountByEmail(model.Email.ToLower());

                        ApplicationUserPasswordHistory passwordModel = new ApplicationUserPasswordHistory();
                        passwordModel.UserId = userInfo.Id;
                        passwordModel.CreatedDate = DateTime.Now;
                        passwordModel.HashPassword = Password.Encrypt();
                        passwordModel.CreatedBy = CurrentUser.UserId;

                        _context.ApplicationUserPasswordHistorys.Add(passwordModel);
                        _context.SaveChanges();

                        var resut = userRoles.ToList().Where(r => r.ID.ToString() == RoleId.ToString()).ToList();
                        string roleResult = await _roleManager.MapUsersToRoles(userInfo.Id, resut, getController(), getAction());


                        // SEND EMAIL 
                        var emailtemplate = await _emailManager.GetEmailTemplateByCode(EmailTemplateCode.ACCOUNT_VERIFICATION);


                        var getUpdateUserDetails = await _userInManager.FindByEmailAsync(user.Email);


                        string confirmationToken = _userInManager.GenerateEmailConfirmationTokenAsync(getUpdateUserDetails).Result;

                        string confirmationLink = Url.Action("ConfirmEmail", "Account", new { userid = getUpdateUserDetails.Id, token = confirmationToken }, protocol: HttpContext.Request.Scheme);

                        var emailTokens = new List<EmailTokenViewModel>
                        {
                            new EmailTokenViewModel {  Token = EmailTokenConstants.FULLNAME,  TokenValue = model.FirstName +  ' ' + model.LastName },
                            new EmailTokenViewModel {  Token = EmailTokenConstants.PORTALNAME,  TokenValue = _configuration["PortalName"] },
                            new EmailTokenViewModel {  Token = EmailTokenConstants.ACCOUNT_VERIFICATION_LINK,  TokenValue = confirmationLink },
                            new EmailTokenViewModel {  Token = EmailTokenConstants.LogoURL,  TokenValue = _configuration["LogoURL"] },
                            new EmailTokenViewModel {  Token = EmailTokenConstants.DURATION,  TokenValue = ExpirationTime }


                        };

                        bool mailresponse = await _emailManager.PrepareEmailLog(EmailTemplateCode.ACCOUNT_VERIFICATION, model.Email, _configuration["cc"], "", emailTokens, CurrentUser.UserId, false);

                        if (mailresponse)
                        {
                            model.successful = ResponseErrorMessageUtility.SUCCESS;

                        }
                        else
                        {
                            model.successful = ResponseErrorMessageUtility.FAIL;

                        }
                        UploadResult.Add(model);
                    }
                    else
                    {
                        model.successful = ResponseErrorMessageUtility.FAIL;
                        UploadResult.Add(model);

                    }


                }

                return UploadResult;
            }
            catch (Exception ex)
            {
                return UploadResult;
            }
        }





        public void reloadPermission(ApplicationUser user, List<ApplicationRoleViewModel> roles)
        {

            var role = roles.ToList().Where(r => r.ID.Equals(user.RoleId ?? "")).FirstOrDefault();

            if (role != null)
            {
                var roleId = role.ID;
                // Get User's Role Permissions:
                AccessDataLayer accessDataLayer = new AccessDataLayer(_context);
                DBManager dBManager = new DBManager(_context);

                var parameters = new List<IDbDataParameter>();


                parameters.Add(dBManager.CreateParameter("@RoleId", user.RoleId, DbType.String));
                DataTable menuList = accessDataLayer.
                    FetchRolePermissionsByRoleId(parameters.ToArray(), ResponseErrorMessageUtility.FetchUserPermissionAndRole);

                List<Permission> menus = new List<Permission>();

                foreach (DataRow dataRow in menuList.Rows)
                {
                    var permission = new Permission
                    {
                        CreatedBy = dataRow["CreatedBy"].ToString(),
                        Icon = dataRow["IconName"].ToString(),
                        ParentId = Convert.ToInt32(dataRow["ParentId"]),
                        PermissionCode = dataRow["PermissionCode"].ToString(),
                        PermissionName = dataRow["PermissionName"].ToString(),
                        Url = dataRow["Url"].ToString(),
                        ID = Convert.ToInt64(dataRow["ID"])

                    };

                    menus.Add(permission);

                }


                var parentMenus = menuList.Select("ParentId = 0").ToList(); ;
                List<Permission> SelectedParentMenus = new List<Permission>();
                List<Permission> SelectedParentMenusOrder = new List<Permission>();

                foreach (DataRow dataRow in parentMenus)
                {
                    var permission = new Permission
                    {
                        //  CreatedBy = dataRow["CreatedBy"].ToString();
                        Icon = dataRow["IconName"].ToString(),
                        ParentId = Convert.ToInt32(dataRow["ParentId"]),
                        PermissionCode = dataRow["PermissionCode"].ToString(),
                        PermissionName = dataRow["PermissionName"].ToString(),
                        Url = dataRow["Url"].ToString(),
                        ID = Convert.ToInt64(dataRow["PermissionId"])
                    };

                    SelectedParentMenus.Add(permission);

                }


                SelectedParentMenusOrder = SelectedParentMenus.OrderBy(a => a.PermissionName).ToList();

                var sb = new StringBuilder();
                List<SidebarMenuViewModel> sidebarMenus = DynamicMenu.GenerateUrl(SelectedParentMenusOrder, menus, sb);


                Filters.SessionExtensions.SetObjectAsJson(HttpContext.Session, "MenuString", "");
                Filters.SessionExtensions.SetObjectAsJson(HttpContext.Session, "MenuString", sidebarMenus);
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("RoleName", role.RoleName.ToLower());
                HttpContext.Session.SetString("Menus", JsonConvert.SerializeObject(menus));


            }
        }



        [HttpPost("UserManagement/PreviewUpload")]

        public async Task<IActionResult> UploadExcelPreview(ExcelFileUploadViewModel uploadData)
        {
            var file = uploadData.DocumentUpload;
            var model = new ExcelFileUploadViewModel();
            try
            {
                //  Get all files from Request object  


                string data = "";

                var bulkList = new List<ApplicationBulkUploadViewModel>();


                if (file != null)
                {

                    if (file.ContentType == "application/vnd.ms-excel" || file.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        string filename = file.FileName;

                        if (filename.EndsWith(".xlsx") || filename.EndsWith(".xls"))
                        {

                            var fileName = await _filehandler.UploadFile(file, _configuration["temp_upload"],
                            _configuration["AllExtensionImageExcel"], Convert.ToInt32(_configuration["oneMegaByte"]), Convert.ToInt32(_configuration["_fileMaxSize"]));


                            if (!fileName.Contains(ResponseErrorMessageUtility.FILE_UPLOAD_FAILED))
                            {
                                string fullPath = Directory.GetCurrentDirectory() + _configuration["temp_upload"] + fileName;

                                DataSet myDataset = Utilities.Common.ExcelToDataSet(fullPath, true);

                                if (!myDataset.Tables[0].Columns.Contains("FIRSTNAME") || !myDataset.Tables[0].Columns.Contains("LASTNAME")
                                    || !myDataset.Tables[0].Columns.Contains("MIDDLENAME") ||
                                    !myDataset.Tables[0].Columns.Contains("EMAIL") || !myDataset.Tables[0].Columns.Contains("MOBILENUMBER") ||
                                    !myDataset.Tables[0].Columns.Contains("ROLE"))
                                {
                                    _toastNotification.AddWarningToastMessage(ResponseErrorMessageUtility.InvalidExcelHeader, null);



                                    ImportViewBagParams();
                                    ViewBag.ControllerName = GetControllerName(this);
                                    return PartialView("Import", model);

                                }


                                DataTable dt = myDataset.Tables[0];

                                string BatchKey = Utilities.Common.RandomizePassword();

                                if (dt.Rows.Count == 0)
                                {

                                    _toastNotification.AddWarningToastMessage(ResponseErrorMessageUtility.EmptyExcel, null);

                                    ImportViewBagParams();
                                    ViewBag.ControllerName = GetControllerName(this);
                                    return PartialView("Import", model);


                                }




                                foreach (DataRow dr in dt.Rows)
                                {
                                    var singleRecord = new ApplicationBulkUploadViewModel();

                                    singleRecord.FirstName = GetStringValue(dr["FirstName"]).Trim();
                                    singleRecord.LastName = GetStringValue(dr["LastName"]).Trim();
                                    singleRecord.MobileNumber = GetStringValue(dr["MobileNumber"]).Trim();
                                    singleRecord.PhoneNumber = GetStringValue(dr["PhoneNumber"]).Trim();
                                    singleRecord.Email = GetStringValue(dr["Email"]).Trim();
                                    singleRecord.Role = GetStringValue(dr["Role"]).Trim();
                                    singleRecord.MiddleName = GetStringValue(dr["MiddleName"]).Trim();
                                    singleRecord.BatchKey = BatchKey;

                                    data = await ValidateEntry(dr);

                                    if (data == ResponseErrorMessageUtility.SUCCESS)
                                    {
                                        singleRecord.successful = ResponseErrorMessageUtility.SUCCESS;

                                    }
                                    else
                                    {
                                        singleRecord.successful = data;
                                    }
                                    bulkList.Add(singleRecord);
                                }



                                HttpContext.Session.SetObjectAsJson("bulkList", bulkList);

                                ViewBag.BatchKey = BatchKey.Encrypt();

                                return View("PreviewUpload", bulkList);




                            }
                            else
                            {
                                // return an error message when a document upload fails
                                var errorMessage = fileName.Split('|')[1];
                                _toastNotification.AddWarningToastMessage(errorMessage, null);
                                ImportViewBagParams();
                                ViewBag.ControllerName = GetControllerName(this);
                                return PartialView("Import", model);


                            }
                        }
                        else
                        {

                            _toastNotification.AddWarningToastMessage(ResponseErrorMessageUtility.SelectExcelFile, null);
                            ImportViewBagParams();
                            ViewBag.ControllerName = GetControllerName(this);
                            return PartialView("Import", model);

                        }
                    }

                }
                else
                {

                    _toastNotification.AddWarningToastMessage(ResponseErrorMessageUtility.SelectExcelFile, null);
                    ImportViewBagParams();
                    ViewBag.ControllerName = GetControllerName(this);
                    return PartialView("Import", model);

                }


                HttpContext.Session.SetObjectAsJson("bulkList", bulkList);
                return View("PreviewUpload", bulkList);
            }


            catch (Exception ex)
            {

                _toastNotification.AddWarningToastMessage(ex.Message, null);
                ImportViewBagParams();
                ViewBag.ControllerName = GetControllerName(this);
                return PartialView("Import", model);
            }
        }



        public static string GetStringValue(object obj)
        {
            string s = "";
            try
            {
                s = !(obj is DBNull) ? obj.ToString() : "";
            }
            catch
            {
            }
            return s;
        }
        private async Task<string> ValidateEntry(DataRow dr)
        {
            try
            {

                string msg = ResponseErrorMessageUtility.SUCCESS;

                string email = GetStringValue(dr["Email"]).Trim();
                string phonenumber = GetStringValue(dr["PhoneNumber"]).Trim();
                string Role = GetStringValue(dr["Role"]).Trim();

                var checkEmail = await _userManagerPassword.FindByEmailAsync(email);


                var spoolResult = await _userManager.GetAllUserAccounts(null); ;

                var checkNumber = spoolResult.Where(a => a.MobileNumber == phonenumber || a.PhoneNumber == phonenumber).Count();


                var roles = await _roleManager.GetRoles(true);


                var checkRoleStatus = roles.Where(a => a.RoleName.ToLower() == Role.ToLower());

                if (checkEmail != null)
                {
                    msg = "Existing Email Address";

                }
                else if (checkNumber > 0)
                {

                    msg = "Existing Phone Number";
                }

                else if (checkRoleStatus.Count() == 0)
                {
                    msg = "Role don't exist";

                }

                return msg;
            }
            catch (Exception ex)
            {

                return "Error While Processing Data";
            }
        }

        #endregion



        #region  Helper


        #endregion





        public async Task<List<ApplicationRoleViewModel>> LoadSpecificData()
        {

            var getRoles = await _roleManager.GetRoles(true);

            if (CurrentUser.RoleId == ResponseErrorMessageUtility.SystemAdministrator)
            {
                return getRoles.Where(a => a.ID == ResponseErrorMessageUtility.CentralAdmin).ToList();
            }

            else if (CurrentUser.RoleId == ResponseErrorMessageUtility.CentralAdmin)
            {
                return getRoles.Where(a => a.ID == ResponseErrorMessageUtility.CentralAdmin || a.ParentRoleId == ResponseErrorMessageUtility.CentralAdmin || a.ID == ResponseErrorMessageUtility.PlantAdmin).ToList().OrderBy(a => a.Name).ToList();
            }

            else if (CurrentUser.RoleId == ResponseErrorMessageUtility.PlantAdmin)
            {
                return getRoles.Where(a => a.ID == ResponseErrorMessageUtility.PlantAdmin || a.ParentRoleId == ResponseErrorMessageUtility.PlantAdmin || a.ID == ResponseErrorMessageUtility.MiniAdmin).ToList();
            }

            else if (CurrentUser.RoleId == ResponseErrorMessageUtility.MiniAdmin)
            {
                return getRoles.Where(a => a.ParentRoleId == ResponseErrorMessageUtility.MiniAdmin).ToList();
            }
            else
            {
                return getRoles.Where(a => a.ID == "N/A").ToList();
            }
        }


    }

    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }


}
