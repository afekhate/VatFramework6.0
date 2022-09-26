using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VatFramework.Models.DataObjects.EmailMessaging;


namespace VatFramework.Services.Contract.EmailMessaging
{
   public  interface  IEmailMessaging
    {
        
        #region Email Template
        Task<List<EmailTemplateViewModel>> GetAllEmailTemplates(bool? status);
        Task<EmailTemplateViewModel> GetEmailTemplateById(long id, string ControllerName, string ActionName);
        Task<EmailTemplateViewModel> GetEmailTemplateByCode(string Code);
        Task<string> CreateEmailTemplate(EmailTemplateViewModel obj, string ControllerName, string ActionName);
        Task<string> UpdateEmailTemplate(EmailTemplateViewModel obj, string ControllerName, string ActionName);
        Task<bool> DeleteEmailTemplate(long id, string ControllerName, string ActionName, string ModifyBy);
        #endregion

        #region EmailLog
        //Task<string> SendEmail(string EmailTemplateCode, string To, string CC, string emailBCC, string emailAttach, string emailTokens, string serverPath, DateTime? SendDate, bool SendNow = true);
        Task<bool> PrepareEmailLog(string EmailTemplateCode,string emailTo, string emailCC
      , string emailBCC,List<Handler.EmailMessaging.EmailTokenViewModel> emailTokens,
            string CreadedBy,DateTime SendDateTime, bool EmailScheduler = false);

        Task<bool> PrepareEmailLog(string EmailTemplateCode, string emailTo, string emailCC
        , string emailBCC, List<Handler.EmailMessaging.EmailTokenViewModel> emailTokens,
       string CreadedBy, bool EmailScheduler = false, bool TranslateMessage = false, string contentToTranslate = "");


      bool SendEmail(EmailLogViewModel newEmailLog, bool EmailScheduler, string createdBy);
        bool SendEmail(EmailLogViewModel newEmailLog, string createdBy);



        Task<string> PrepareMultipleEmailLog(string EmailTemplateCode, List<string> emailTos, string emailCC
      , string emailBCC, List<Handler.EmailMessaging.EmailTokenViewModel> emailTokens,
     string CreadedBy, bool EmailScheduler = false, bool TranslateMessage = false, string contentToTranslate = "");

        #endregion



    }
}
