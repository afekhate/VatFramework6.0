using System.Collections.Generic;
using System.Security.Claims;

namespace VatFramework.Utilities.AuthenticationUtility.AuthUser
{
    public interface IAuthUser
    {
        string Name { get; }
        string EmailAddress { get; }
        string UserId { get; } 

        //long BranchID { get;  }
        string IPAddress { get; }
 
        string Browser { get; }
        bool Authenticated { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
