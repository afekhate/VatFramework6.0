using Microsoft.AspNetCore.Mvc;

namespace VatFramework.Models.DataObjects.Account
{
    public class PasswordInAppResetDTO
    {
        public string CurrentPassword { get; set; }
        [Remote(action: "VerifyPassword", controller: "Json", AdditionalFields = nameof(ConfirmPassword))]
        public string Password { get; set; }
        [Remote(action: "VerifyPassword", controller: "Json", AdditionalFields = nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
