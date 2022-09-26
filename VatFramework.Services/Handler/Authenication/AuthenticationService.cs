using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VatFramework.Models;
using VatFramework.Models.DataObjects.Account;
using VatFramework.Models.Domains.Account;
using VatFramework.Services.Contract.EntityService;
using VatFramework.Services.Contract.UserService;
using VatFramework.Utilities.AuthenticationUtility.AuthUser;
using System.Security.Claims;
using global::VatFramework.Services.Contract.Authenication;
using Microsoft.AspNetCore.Identity;

namespace VatFramework.Services.Handler.Authenication
{
    public class AuthenticationService : IAuthenication
    {
        private readonly APPContext _Context;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IAuthUser _authUser;
        private readonly IActivityLog _activityLog;
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly SignInManager<ApplicationUser> _signInManager;
        protected readonly RoleManager<ApplicationRole> _roleManager;
        public AuthenticationService(APPContext Context, ILogger<AuthenticationService> logger, IAuthUser authUser, IActivityLog activityLog, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _Context = Context;
            _logger = logger;
            _authUser = authUser;
            _activityLog = activityLog;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<AdminUserSettingViewModel> AuthenticateUser(string username, string password)
        { 
                //throw new NotImplementedException();
                AdminUserSettingViewModel model = new AdminUserSettingViewModel();
            return null;
            
        }

        public Task<string> Confirmations(string username)
        {
            throw new NotImplementedException();
        }

        public Task<string> ConfirmEmail(string userid, string code)
        {
            throw new NotImplementedException();
        }

        public Task CreateLastLoginDate(string userid)
        {
            throw new NotImplementedException();
        }

        public Task CreateLoginHistory(UserLoginHistory obj)
        {
            throw new NotImplementedException();
        }

        public Task<string> GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            throw new NotImplementedException();
        }

        public Task<string> LogoutUser()
        {
            throw new NotImplementedException();
        }

        public Task ResetPassword(PasswordResetOuterDTO obj)
        {
            throw new NotImplementedException();
        }


        
        public Task UpdateLoginHistory(UserLoginHistory obj)
        {
            throw new NotImplementedException();
        }

        Task<Microsoft.AspNetCore.Identity.SignInResult> IAuthenication.AuthenticateUser(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
