using System.Collections.Generic;
using System.Threading.Tasks;
using VatFramework.Models.DataObjects.MessageObject;

namespace VatFramework.Services.Contract
{
    public interface IMessageManagementService
    {
        #region Email Template
        Task<IEnumerable<EmailTemplateViewModel>> GetEmailTemplates(bool status = true);
        Task<EmailTemplateViewModel> GetEmailTemplate(int id, bool status = true);
        Task<string> CreateEmailTemplate(EmailTemplateDTO obj);
        Task<string> UpdateEmailTemplate(EmailTemplateDTO obj, int id);
        Task<bool> DeleteEmailTemplate(int id, bool status);
        #endregion

        #region Email Token
        Task<IEnumerable<EmailTokensViewModel>> GetEmailTokens(bool status = true);
        #endregion
    }
}
