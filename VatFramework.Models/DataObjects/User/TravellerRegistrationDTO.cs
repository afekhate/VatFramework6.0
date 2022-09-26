using Microsoft.AspNetCore.Mvc;

namespace FRSCInventory.Models.DataObjects.User
{
    public class TravellerRegistrationDTO
    {
        public string phonenumber { get; set; }
        public string email { get; set; }
        public int stateid { get; set; }
        public int lgaid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        [Remote(action: "VerifyPassword", controller: "Json", AdditionalFields = nameof(confirmpassword))]
        public string password { get; set; }
        [Remote(action: "VerifyPassword", controller: "Json", AdditionalFields = nameof(password))]
        public string confirmpassword { get; set; }
        public string referedby { get; set; }
    }
}
