
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VatFramework.Models.DataObjects.Account;
using VatFramework.Models.Domains.Account;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using VatFramework.Utilities.ExceptionUtility;
using NToastNotify;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using VatFramework.Services.Contract.Permission;
using System.Data;
using VatFramework.Services.Handler.DataAccess;
using VatFramework.Models;
using VatFramework.Models.Domains.Permission;
using System;
using VatFramework.Models.DataObjects.Icon;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using VatFramework.Services.Contract.ErrorLogger;
using Microsoft.EntityFrameworkCore.Internal;
using VatFramework.Models.DataObjects.ApplicationRole;
using VatFramework.Services.Contract.EmailMessaging;
using VatFramework.Services.Handler.EmailMessaging;
using VatFramework.Services.Contract.Authenication;
using VatFramework.Web.Models;
using VatFramework.Services.Contract.EntityService;
using Microsoft.AspNetCore.Mvc.Rendering;
using VatFramework.Services.Contract.UserService;

namespace VatFramework.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly APPContext _context;
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        protected readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IToastNotification _toastNotification;
        private readonly IRole _roleSignManager;
        private readonly IRolePermission _rolePermissionManager;
        private readonly IErrorLogger _errorLogger;
        private readonly IEmailMessaging _emailManager;
    
        private readonly IAuthenication _authManager;
        private readonly IActivityLog _activityRepo;
        private readonly IUserService _UserManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager, IUserService UserManager,
            IConfiguration configuration, IToastNotification toastNotification,
            IRole roleSignManager, IRolePermission rolePermissionManager,
            APPContext context, IErrorLogger errorLogger, IEmailMessaging emailManager,
           IAuthenication authManager, IActivityLog activityRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
            
            _toastNotification = toastNotification;
            _roleSignManager = roleSignManager;
            _UserManager = UserManager;
            _rolePermissionManager = rolePermissionManager;
            _context = context;
            _errorLogger = errorLogger;
            _emailManager = emailManager;
            _roleManager = roleManager;
            _authManager = authManager;
            _activityRepo = activityRepo;
            // _language = language;
            //
        }
        public IActionResult Login()
        {

            LoginDTO model = new LoginDTO();


            HttpContext.Session.Clear();

            if (_configuration["LoadMyCredentails"] == "Yes")
            {
                //model.password = "Security135.1";
                // model.username = "bolajiworld@gmail.com";
            }

            return View(model);


        }

       
      
        public IActionResult ForgetPassword()
        {
            return View(new ForgetPasswordViewModel());
        }


        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> forgetpassword(ForgetPasswordViewModel model)
        {

            //Check the model state of the Request
            if (!ModelState.IsValid)
            {
                ViewBag.ControllerName = GetControllerName(this);
                CreateViewBagParams();
                return PartialView("ForgetPassword", model);
            }

            // confirm password check



            //check old password with email address
            ApplicationUser signedUser = await _userManager.FindByEmailAsync(model.Email);


            if (signedUser == null)
            {
                _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.IllegalOperation, null);
                return PartialView("ForgetPassword", model);

            }



            string confirmationLink = await _userManager.GeneratePasswordResetTokenAsync(signedUser);


            if (confirmationLink != null)
            {

                // SEND EMAIL 
                var emailtemplate = await _emailManager.GetEmailTemplateByCode(EmailTemplateCode.FORGOT_PASSWORD);
                var expiredDate = Convert.ToInt32(_configuration["PwdExpireDate"]);
                string ExpirationTime = DateTime.Now.AddMinutes(expiredDate).ToString();

                ExpirationTime = ExtentionUtility.Encrypt(ExpirationTime);



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

                    _activityRepo.CreateActivityLog(string.Format("Password Reset link successfully  for  : {0} was successfully generated",
                        signedUser.Email), getController(), getAction(), signedUser.Id, signedUser);
                    //signedUser.Email),getController(),getAction(), CurrentUser.UserId, signedUser);
                    return RedirectToAction("Login");
                }
                else
                {
                    _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.OperationFailed, null);
                    return PartialView("ForgetPassword", model);
                }

            }
            else
            {

                _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.Errorconfirming, null);
                return PartialView("ForgetPassword", model);

            }
        }


        [HttpPost]
        [AllowAnonymous]
        //https://qawithexperts.com/questions/123/how-to-change-password-in-aspnet-mvc

        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {

            //Check the model state of the Request
            if (!ModelState.IsValid)
            {
                ViewBag.ControllerName = GetControllerName(this);
                CreateViewBagParams();
                return PartialView("ChangePassword", model);
            }

            // confirm password check

            if (model.ConfirmPassword != model.NewPassword)
            {
                _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.PasswordMatchError, null);
                return PartialView("ChangePassword", model);

            }

            //check old password with email address
            ApplicationUser signedUser = await _userManager.FindByEmailAsync(model.username);


            if (signedUser == null)
            {
                _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.IllegalOperation, null);
                return PartialView("ChangePassword", model);

            }



            var result = await _userManager.ChangePasswordAsync(signedUser, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {

                // update the pwd change date to enable the user login
                signedUser.PwdChangedDate = DateTime.Now;
                signedUser.LastModified = DateTime.Now;
                await _userManager.UpdateAsync(signedUser);


                ApplicationUserPasswordHistory passwordModel = new ApplicationUserPasswordHistory();
                passwordModel.UserId = signedUser.Id;
                passwordModel.CreatedDate = DateTime.Now;
                passwordModel.HashPassword = ExtentionUtility.Encrypt(model.NewPassword);
                passwordModel.CreatedBy = CurrentUser.UserId;

                _context.ApplicationUserPasswordHistorys.Add(passwordModel);
                _context.SaveChanges();

                // update the password and send the login credentails 
                // SEND EMAIL 
                var emailtemplate = await _emailManager.GetEmailTemplateByCode(EmailTemplateCode.PASSWORD_UPDATE);



                var emailTokens = new List<EmailTokenViewModel>
                        {
                            new EmailTokenViewModel {  Token = EmailTokenConstants.FULLNAME,  TokenValue = signedUser.FirstName +  ' ' + signedUser.LastName },
                            new EmailTokenViewModel {  Token = EmailTokenConstants.PORTALNAME,  TokenValue = _configuration["PortalName"] },
                            new EmailTokenViewModel {  Token = EmailTokenConstants.USERNAME,  TokenValue = signedUser.Email },
                            new EmailTokenViewModel {  Token = EmailTokenConstants.PASSWORD,  TokenValue = model.NewPassword },
                            new EmailTokenViewModel {  Token = EmailTokenConstants.LogoURL,  TokenValue = _configuration["LogoURL"] }
                        };

                bool mailresponse = await _emailManager.PrepareEmailLog(EmailTemplateCode.PASSWORD_UPDATE, signedUser.Email,
                                                    _configuration["cc"], "", emailTokens, CurrentUser.UserId, false);

                if (mailresponse == true)
                {

                    _toastNotification.AddSuccessToastMessage(ResponseErrorMessageUtility.PasswordChanged, null);

                    _activityRepo.CreateActivityLog(string.Format("Password change was successful for  : {0}",
                        signedUser.Email), getController(), getAction(), signedUser.Id, signedUser);
                    //signedUser.Email), getController(), getAction(), CurrentUser.UserId, signedUser);
                    return RedirectToAction("Login");
                }
                else
                {
                    _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.OperationFailed, null);
                    return PartialView("ChangePassword", model);
                }

            }
            else
            {

                _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.SystemPasswordNotMatch, null);
                return PartialView("ChangePassword", model);

            }
        }


        public async Task<IActionResult> ConfirmEmail(string userid, string token)
        {
            string newPasssword = "";

            if (userid == null || token == null)
            {
                LoginDTO model = new LoginDTO();
                _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.IllegalOperation, null);
                return RedirectToAction("login");
            }

            var user = _userManager.FindByIdAsync(userid).Result;

            if (user == null)
            {
                LoginDTO model = new LoginDTO();
                _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.IllegalOperation, null);
                return PartialView("Login", model);
            }

            var expiredate = user.ConfirmationLinkExpireDate;

            if (DateTime.Now > expiredate)
            {
                LoginDTO model = new LoginDTO();
                _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.ExpiredActivationLink, null);
                return RedirectToAction("login");
            }

            user.IsActive = true;

            IdentityResult result = _userManager.ConfirmEmailAsync(user, token).Result;

            if (result.Succeeded)
            {

                newPasssword = VatFramework.Utilities.PasswordGenerator.GenerateRandomPassword(null);


                await _userManager.AddPasswordAsync(user, newPasssword);

                ApplicationUserPasswordHistory passwordModel = new ApplicationUserPasswordHistory();
                passwordModel.UserId = user.Id;
                passwordModel.CreatedDate = DateTime.Now;
                passwordModel.HashPassword = ExtentionUtility.Encrypt(newPasssword);
                passwordModel.CreatedBy = CurrentUser.UserId;

                _context.ApplicationUserPasswordHistorys.Add(passwordModel);
                _context.SaveChanges();

                // update the password and send the login credentails 
                // SEND EMAIL 
                var emailtemplate = await _emailManager.GetEmailTemplateByCode(EmailTemplateCode.ACCOUNT_CREATION);



                var emailTokens = new List<EmailTokenViewModel>
                        {
                            new EmailTokenViewModel {  Token = EmailTokenConstants.FULLNAME,  TokenValue = user.FirstName +  ' ' + user.LastName },
                            new EmailTokenViewModel {  Token = EmailTokenConstants.PORTALNAME,  TokenValue = _configuration["PortalName"] },
                            new EmailTokenViewModel {  Token = EmailTokenConstants.USERNAME,  TokenValue = user.Email },
                            new EmailTokenViewModel {  Token = EmailTokenConstants.PASSWORD,  TokenValue = newPasssword },
                            new EmailTokenViewModel {  Token = EmailTokenConstants.LogoURL,  TokenValue = _configuration["LogoURL"] }
                        };

                bool mailresponse = await _emailManager.PrepareEmailLog(EmailTemplateCode.ACCOUNT_CREATION, user.Email,
                                                    _configuration["cc"], "", emailTokens, CurrentUser.UserId, false);

                if (mailresponse == true)
                {
                    LoginDTO model = new LoginDTO();
                    _toastNotification.AddSuccessToastMessage(ResponseErrorMessageUtility.ValidateAccount, null);

                    _activityRepo.CreateActivityLog(string.Format("Email account validation was successful for  : {0}",
                        // user.Email), getController(), getAction(), CurrentUser.UserId, user);
                        user.Email), getController(), getAction(), user.Id, user);
                    return RedirectToAction("login");
                }
                else
                {
                    LoginDTO model = new LoginDTO();
                    _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.OperationFailed, null);
                    return RedirectToAction("login");
                }

            }
            else
            {
                LoginDTO model = new LoginDTO();
                _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.Errorconfirming, null);
                return RedirectToAction("login");

            }
        }


        [HttpGet("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string userid, string token, string data)
        {

            LoginDTO model = new LoginDTO();

            if (userid == null || token == null || data == null)
            {
                _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.IllegalOperation, null);
                return RedirectToAction("login");
            }

            var ExpirationTime = ExtentionUtility.Decrypt(data);


            if (DateTime.Now > Convert.ToDateTime(ExpirationTime))
            {

                _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.ExpiredResetPasswordLink, null);
                return RedirectToAction("login");
            }

            var user = await _userManager.FindByIdAsync(userid);
            // this me talking


            if (user == null)
            {
                _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.IllegalOperation, null);
                return PartialView("Login", model);
            }

            ResetChangePasswordViewModel modelResponse = new ResetChangePasswordViewModel();
            modelResponse.Email = user.Email;
            modelResponse.token = token;
            modelResponse.userid = userid;

            return PartialView(modelResponse);
        }


        [HttpPost("UpdatePassword")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePassword(ResetChangePasswordViewModel model)
        {

            //Check the model state of the Request
            if (!ModelState.IsValid)
            {
                ViewBag.ControllerName = GetControllerName(this);
                CreateViewBagParams();
                return PartialView("ResetPassword", model);
            }

            // confirm password check

            if (model.ConfirmPassword != model.NewPassword)
            {
                _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.PasswordMatchError, null);
                return PartialView("ResetPassword", model);

            }

            var hashPwd = ExtentionUtility.Encrypt(model.NewPassword);

            if (_context.ApplicationUserPasswordHistorys.Where(a => a.HashPassword == hashPwd && a.UserId == model.userid).Count() > 0)
            {
                _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.ExistingPassword, null);
                return PartialView("ResetPassword", model);
            }


            //check old password with email address
            ApplicationUser signedUser = await _userManager.FindByEmailAsync(model.Email);


            if (signedUser == null)
            {
                _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.IllegalOperation, null);
                return PartialView("ResetPassword", model);

            }

            var result = await _userManager.ResetPasswordAsync(signedUser, model.token, model.NewPassword);

            if (result.Succeeded)
            {

                // update the pwd change date to enable the user login
                signedUser.PwdChangedDate = DateTime.Now;
                signedUser.LastModified = DateTime.Now;
                await _userManager.UpdateAsync(signedUser);


                ApplicationUserPasswordHistory passwordModel = new ApplicationUserPasswordHistory();
                passwordModel.UserId = signedUser.Id;
                passwordModel.CreatedDate = DateTime.Now;
                passwordModel.HashPassword = ExtentionUtility.Encrypt(model.NewPassword);
                passwordModel.CreatedBy = CurrentUser.UserId;

                _context.ApplicationUserPasswordHistorys.Add(passwordModel);
                _context.SaveChanges();

                // update the password and send the login credentails 
                // SEND EMAIL 
                var emailtemplate = await _emailManager.GetEmailTemplateByCode(EmailTemplateCode.USER_CHANGE_PASSWORD);



                var emailTokens = new List<EmailTokenViewModel>
                        {
                            new EmailTokenViewModel {  Token = EmailTokenConstants.FULLNAME,  TokenValue = signedUser.FirstName +  ' ' + signedUser.LastName },
                            new EmailTokenViewModel {  Token = EmailTokenConstants.PORTALNAME,  TokenValue = _configuration["PortalName"] },
                            new EmailTokenViewModel {  Token = EmailTokenConstants.USERNAME,  TokenValue = signedUser.Email },
                            new EmailTokenViewModel {  Token = EmailTokenConstants.PASSWORD,  TokenValue = model.NewPassword },
                            new EmailTokenViewModel {  Token = EmailTokenConstants.LogoURL,  TokenValue = _configuration["LogoURL"] }
                        };

                bool mailresponse = await _emailManager.PrepareEmailLog(EmailTemplateCode.USER_CHANGE_PASSWORD, signedUser.Email,
                                                    _configuration["cc"], "", emailTokens, CurrentUser.UserId, false);

                if (mailresponse == true)
                {

                    _toastNotification.AddSuccessToastMessage(ResponseErrorMessageUtility.PasswordChanged, null);

                    _activityRepo.CreateActivityLog(string.Format("Password change was successful for  : {0}",
                        //signedUser.Email), getController(), getAction(), CurrentUser.UserId, signedUser);
                        signedUser.Email), getController(), getAction(), signedUser.Id, signedUser);
                    return RedirectToAction("Login");
                }
                else
                {
                    _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.OperationFailed, null);
                    return PartialView("ChangePassword", model);
                }

            }
            else
            {

                _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.SystemPasswordNotMatch, null);
                return PartialView("ChangePassword", model);

            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO loginDTO, string? returnUrl = null)
        {

           
            try
            {

                //Check the model state of the Request
                if (!ModelState.IsValid)
                {
                    CreateViewBagParams();
                    ViewBag.ControllerName = GetControllerName(this);
                    string msg = ModelState.FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors.FirstOrDefault().ErrorMessage;
                    string displayMsg = msg.Replace("'", "");
                    _toastNotification.AddWarningToastMessage(displayMsg, null);
                    return PartialView("Login", loginDTO);

                }


                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDTO.username);

                if (user == null)
                {
                    _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.InvalidAccount, null);
                    return PartialView("Login", loginDTO);
                }


                //if (user.PwdChangedDate != null)
                //{
                //    if (user.RoleId == ResponseErrorMessageUtility.TensionManager)
                //    {
                //        _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.LoginNotAllowed, null);
                //        return PartialView("Login", loginDTO);
                //    }
                //}

                //if (user.RoleId == ResponseErrorMessageUtility.TensionManager)
                //{
                //    _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.LoginNotAllowed, null);
                //    return PartialView("Login", loginDTO);
                //}


                if (user.IsDeleted == true)
                {
                    _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.DeletedAccount, null);
                    return PartialView("Login", loginDTO);

                }

                if (user.IsActive == false)
                {
                    _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.disabledAccount, null);
                    return PartialView("Login", loginDTO);
                }

                if (user.EmailConfirmed == false)
                {
                    _toastNotification.AddWarningToastMessage(ResponseErrorMessageUtility.PendingEmailConfirmation, null);
                    return PartialView("Login", loginDTO);
                }

               

                ApplicationUser signedUser = await _userManager.FindByEmailAsync(user.Email);




                if (!(await _userManager.IsEmailConfirmedAsync(signedUser)))
                {
                    _toastNotification.AddWarningToastMessage(ResponseErrorMessageUtility.InvalidAccount, null);
                    return PartialView("Login", loginDTO);
                }




                bool checkPassword = await _userManager.CheckPasswordAsync(signedUser, loginDTO.password);

                if (checkPassword == false)
                {
                    _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.InvalidAccount, null);
                    return PartialView("Login", loginDTO);
                }

                if (signedUser.PwdChangedDate == null)
                {
                    // force the user to change this or her password
                    _toastNotification.AddInfoToastMessage(ResponseErrorMessageUtility.ChangeSystemGeneratedPassword, null);
                    return PartialView("ChangePassword");

                }



                Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, loginDTO.password, false, false);


                if (signInResult.Succeeded)
                {

                    var roles = await _roleSignManager.GetRoles(true);

                    PrepareUserMenu(user, roles);
                    await PrepareSignInClaims(user);
                    var s = await _userManager.GetClaimsAsync(user);
                    return RedirectToLocal(returnUrl);

                }

                else
                {
                    ViewBag.ControllerName = GetControllerName(this);
                    CreateViewBagParams();
                    _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.InvalidAccount, null);
                    return PartialView("Login", loginDTO);
                }


            }

            catch (System.Exception ex)
            {
                return systemError(ex);

            }
        }

        [HttpGet("Account/Register")]
        public async Task<IActionResult> Register()
        {
            try
            {

                CreateViewBagParams();
                ViewBag.ControllerName = GetControllerName(this);

                var userRoles = await _roleSignManager.GetRoles(null);
                var userRoleId = userRoles.ToList().Where(r => r.RoleName.ToString().ToLower() == ResponseErrorMessageUtility.Reviewer.ToLower()).FirstOrDefault();
                ViewBag.RoleId = userRoleId.ID;

               
                var model = new AdminUserSettingViewModel
                {
                    

                };

                return View(model);
            }
            catch (Exception ex)
            {
                return systemError(ex);
            }
        }


        [HttpPost("Account/Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AdminUserSettingViewModel model)
        {
            try
            {

                ViewBag.ControllerName = GetControllerName(this);

                //get user role id
                var userRoles = await _roleSignManager.GetRoles(null);
                var userRoleId = userRoles.ToList().Where(r => r.RoleName.ToString().ToLower() == ResponseErrorMessageUtility.Reviewer.ToLower()).FirstOrDefault();
                //model.RoleId = userRoleId.ID;
                ViewBag.RoleId = userRoleId.ID;
               

                //Check the model state of the Request
                if (!ModelState.IsValid)
                {

                    ListViewBagParams();
                    string msg = ModelState.FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors.FirstOrDefault().ErrorMessage;
                    string displayMsg = msg.Replace("'", "");
                    _toastNotification.AddWarningToastMessage(displayMsg, null);
                    return View(model);
                }

                if (string.Compare(model.Password, model.ConfirmPassword,
                       StringComparison.InvariantCultureIgnoreCase) != 0)
                {

                    _toastNotification.AddWarningToastMessage(ResponseErrorMessageUtility.PasswordNotMatch, null);
                    return View(model);

                }

               

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
                    RoleId = model.RoleId
                    

                };

                model.Password = Utilities.PasswordGenerator.GenerateRandomPassword(null);
                model.CreatedBy = CurrentUser.UserId;

                string response = await _UserManager.CreateUser(model, getController(), getAction());


                if (!string.IsNullOrEmpty(response) && response == ResponseErrorMessageUtility.SuccessfulAndUpdated)
                {

                    var userInfo = await _UserManager.GetUserAccountByEmail(model.Email.ToLower());


                    ApplicationUserPasswordHistory passwordModel = new ApplicationUserPasswordHistory();
                    passwordModel.UserId = userInfo.Id;
                    passwordModel.CreatedDate = DateTime.Now;
                    passwordModel.HashPassword = ExtentionUtility.Encrypt(model.Password);
                    passwordModel.CreatedBy = CurrentUser.UserId;

                    _context.ApplicationUserPasswordHistorys.Add(passwordModel);
                    _context.SaveChanges();

                    //var resut = userRoles.ToList().Where(r => r.RoleName.ToString() == ResponseErrorMessageUtility.Reporter.ToLower()).ToList();
                    var resut = userRoles.ToList().Where(r => r.ID.ToString() == model.RoleId.ToString()).ToList();

                    string roleResult = await _roleSignManager.MapUsersToRoles(userInfo.Id, resut, getController(), getAction());


                    // SEND EMAIL 
                    var emailtemplate = await _emailManager.GetEmailTemplateByCode(EmailTemplateCode.ACCOUNT_VERIFICATION);

                    var getUpdateUserDetails = await _userManager.FindByEmailAsync(user.Email);

                    string confirmationToken = _userManager.GenerateEmailConfirmationTokenAsync(getUpdateUserDetails).Result;

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
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    _toastNotification.AddWarningToastMessage(response, null);
                    //return RedirectToAction(nameof(Index));
                    return View(model);
                }


            }
            catch (Exception ex)
            {
                // return systemError(ex);
                return View(model);
            }
        }


        // POST: /Account/ResetPassword

        public async Task<IActionResult> LogOff()
        {

            HttpContext.Session.Clear();
            await _signInManager.SignOutAsync();



            _toastNotification.AddInfoToastMessage(ResponseErrorMessageUtility.LogoutSuccessful, null);
            return RedirectToAction("Login", "Account");
        }


        #region Helpers Operation
        private async Task PrepareSignInClaims(ApplicationUser user)
        {

            var userClaims = await _userManager.GetClaimsAsync(user);

            var _claims = userClaims.ToList();
            string roles = string.Empty;
            IList<string> role = await _userManager.GetRolesAsync(user);

            if (role.Any())
            {
                roles = String.Join(",", role); /*role.Join("",);*/
            }

            string USERPERMISSION = SetUserPermissions(user.Id);

            var RoleName = await _context.ApplicationRoles.Where(a => a.Id == user.RoleId).FirstOrDefaultAsync();


            var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.FirstName + ' '  + user.LastName),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Role, RoleName.Name),
                            new Claim(ClaimTypes.Surname, user.LastName),
                            new Claim(ClaimTypes.MobilePhone, "08033011022"),
                            new Claim(ClaimTypes.GivenName, user.FirstName + ' '  + user.LastName),
                            new Claim(ClaimTypes.Anonymous, RoleName.RoleName),
                            new Claim(ClaimTypes.PostalCode, RoleName.Id)

                        }.Union(userClaims);

            foreach (var r in role)
            {
                claims = claims.Append(new Claim(ClaimTypes.Role, r));
            }

            _claims = claims.ToList();

            User.AddIdentity(new ClaimsIdentity(_claims));

            await _signInManager.SignInWithClaimsAsync(user, false, _claims);
        }

        public void PrepareUserMenu(ApplicationUser user, List<ApplicationRoleViewModel> roles)
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
                List<SidebarMenuViewModel> sidebarMenus = Filters.DynamicMenu.GenerateUrl(SelectedParentMenusOrder, menus, sb);


                Filters.SessionExtensions.SetObjectAsJson(HttpContext.Session, "MenuString", sidebarMenus);
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("RoleName", role.RoleName.ToLower());
                HttpContext.Session.SetString("Menus", JsonConvert.SerializeObject(menus));






            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }



            return RedirectToAction("Index", "Dashboard");
        }

        private string SetUserPermissions(string UserId)
        {
            try
            {

                AccessDataLayer accessDataLayer = new AccessDataLayer(_context);
                DBManager dBManager = new DBManager(_context);

                var parameters = new List<IDbDataParameter>();

                parameters.Add(dBManager.CreateParameter("@UserId", UserId, DbType.String));
                DataTable mUserPremissionRolemodel = accessDataLayer.FetchUserPermissionAndRole(parameters.ToArray(), "FetchUserPermissionAndRole");



                string userPermissions = "";
                if (mUserPremissionRolemodel != null)
                {
                    int i = 0;
                    foreach (DataRow item in mUserPremissionRolemodel.Rows)
                    {
                        i = i + 1;
                        if (i == 0)
                        {
                            userPermissions = item["PermissionCode"] + ",";
                        }
                        else
                        {
                            userPermissions = userPermissions + item["PermissionCode"].ToString() + ",";
                        }
                    }
                }
                return userPermissions;
            }
            catch (Exception ex)
            {
                //  _log.Error(ex);
                return string.Empty;
            }
        }

        //private List<SidebarMenuViewModel> GenerateUrl(List<Permission> parentMenus, List<Permission> menus, StringBuilder sb)
        //{
        //    List<SidebarMenuViewModel> sidebarMenus = null;
        //    if (parentMenus.Count > 0)
        //    {
        //        string alias = _configuration["PortalAlias"];

        //        // Looping through Parent Menu
        //        sidebarMenus = new List<SidebarMenuViewModel>();

        //        foreach (var menu in parentMenus)
        //        {
        //            // Get all Menu Components into variables:
        //            string url = menu.Url;
        //            string menuText = menu.PermissionName;
        //            string icon = menu.Icon;


        //            // Get out the current Parent menu Id & ParentId inside the loop
        //            var pid = menu.ID;
        //            var parentId = menu.ParentId;
        //            //var subMenus = menus.FindAll(x => x.ParentId == pid);

        //            sidebarMenus.Add(new SidebarMenuViewModel
        //            {

        //                Icon = menu.Icon,
        //                MenuText = menu.PermissionName,
        //                Alias = alias,
        //                Url = menu.Url,
        //                PID = menu.ID.ToString(),
        //                ParentId = menu.ParentId.ToString(),
        //                SubMenus = menus.FindAll(x => x.ParentId == pid).ToList(),


        //            }); ;


        //        }
        //    }
        //    return sidebarMenus;
        //}



        public RedirectToActionResult systemError(Exception ex)
        {
            _errorLogger.LogError(ex, GetControllerAndActionName(this));
            _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.OperationFailed_Ex, null);

            ModelState.Clear();

            return RedirectToAction(nameof(Login));
        }
        #endregion
    }
}