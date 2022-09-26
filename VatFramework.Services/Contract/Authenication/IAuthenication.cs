using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VatFramework.Models.DataObjects.Account;
using VatFramework.Models.Domains.Account;

namespace VatFramework.Services.Contract.Authenication
{
    public interface IAuthenication
    {
        #region Authentication
       

        Task<SignInResult> AuthenticateUser(string username, string password);
        Task<string> GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        Task<string> LogoutUser();
        Task<string> ConfirmEmail(string userid, string code);
        Task<string> Confirmations(string username);
        


        Task CreateLoginHistory(UserLoginHistory obj);
        Task CreateLastLoginDate(string userid);

        Task UpdateLoginHistory(UserLoginHistory obj);

        Task ResetPassword(PasswordResetOuterDTO obj);

      
        #endregion
    }
}
