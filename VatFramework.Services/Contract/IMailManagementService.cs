using System.Threading.Tasks;
using VatFramework.Models.Domains.Messaging;

namespace VatFramework.Services.Contract
{
    public interface IMailManagementService
    {
        Task<int> SendWelcomeEmail(string emailadd, string username, string password, string loginurl);
        Task<int> SendPasswordResetEmail(string emailadd, string username, string reseturl, long merchantid);
        Task<TEmailtemplate> GetSystemEmailTemplate(string code);
        Task<int> SendAffiliateWelcomeEmail(string emailadd, string fullname, string code, string affilaiteurl);
    }
}
