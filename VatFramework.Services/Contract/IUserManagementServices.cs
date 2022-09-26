using System.Collections.Generic;
using System.Threading.Tasks;
using VatFramework.Models.DataObjects.User;

namespace VatFramework.Services.Contract
{
    public interface IUserManagementServices
    {
        Task<IEnumerable<UserViewModel>> GetUsers(bool status = true);
        Task<UserViewModel> GetUser(string id, bool status = true);

        Task<bool> CheckSupervisor(string userid);
        Task<string> UpdateMyProfileAsync(UserViewModel obj);
    }
}
