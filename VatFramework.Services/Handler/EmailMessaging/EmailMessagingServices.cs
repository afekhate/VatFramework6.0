using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using VatFramework.Models;
using VatFramework.Models.DataObjects.EmailMessaging;
using VatFramework.Services.Contract.EmailMessaging;
using VatFramework.Services.Contract.EntityService;

using VatFramework.Services.Contract.Settings;
using VatFramework.Utilities.AuthenticationUtility.AuthUser;
using VatFramework.Utilities.ExceptionUtility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using VatFramework.Models.Domains.EmailTemplate;

namespace VatFramework.Services.Handler.EmailMessaging
{
    public class EmailMessagingServices : IEmailMessaging
    {
        private readonly APPContext _context;
        private readonly IConfiguration _config;
        private readonly ILogger<EmailMessagingServices> _logger;
        private readonly IAuthUser _authUser;
        private readonly IActivityLog _activityLog;
        private readonly IHostingEnvironment _env;
        private readonly ISystemSetting _systemSettingManager;
        private readonly IConfiguration _configuration;

        public static bool _mailSent;

        
        public EmailMessagingServices(APPContext context,
          IAuthUser authUser, ILogger<EmailMessagingServices> logger, IActivityLog activityLog, IConfiguration config,
          IConfiguration iConfig, IHostingEnvironment env, ISystemSetting systemSettingManager)
        {
            _context = context;
            _authUser = authUser;
            _logger = logger;
            _activityLog = activityLog;
            _config = config;
            _configuration = iConfig;
       
            systemSettingManager = systemSettingManager;
        }


        #region Email Template
        public async Task<string> CreateEmailTemplate(EmailTemplateViewModel obj, string ContollerName, string ActionName)
        {
            string status = "";
            try
            {

                var checkCode = await _context.EmailTemplates
                 .AnyAsync(s => s.Code.Trim().ToLower() == obj.Code.Trim().ToLower());

                if (checkCode == true)
                {
                    status = ResponseErrorMessageUtility.ExistingCode;
                    return status;
                }

                var checkDescription = await _context.EmailTemplates
                   .AnyAsync(s => s.Description.Trim().ToLower() == obj.Description.Trim().ToLower());


                if (checkDescription == true)
                {
                    status = ResponseErrorMessageUtility.ExistingSubjectDescription;
                    return status;
                }

                var incoming = obj.Subject.Trim().ToLower();

                var checkexist = await _context.EmailTemplates
                    .AnyAsync(s => s.Subject.Trim().ToLower() == incoming && s.IsDeleted == false
                   && s.Code == obj.Code);

                if (checkexist == false)
                {
                    EmailTemplate newrecord = new EmailTemplate
                    {
                        Subject = obj.Subject.Trim(),
                        MailBody = obj.MailBody,
                        Description = obj.Description,
                        Code = obj.Code.ToUpper(),
                        CreatedDate = DateTime.Now,
                        CreatedBy = obj.CreatedBy,
                        IsActive = obj.IsActive,
                        IsDeleted = false,
                    };

                    _context.EmailTemplates.Add(newrecord);
                    if (await _context.SaveChangesAsync() > 0)
                    {

                        _activityLog.CreateActivityLog(string.Format("Created EmailTemplate with Subject : {0}", obj.Subject), ContollerName, ActionName, obj.CreatedBy, newrecord);


                        status = ResponseErrorMessageUtility.RecordSaved;
                        return status;
                    }
                    else
                    {
                        status = ResponseErrorMessageUtility.RecordNotSaved;
                        return status;
                    }
                }
                status = string.Format(ResponseErrorMessageUtility.RecordExistBefore, obj.Subject.ToUpper());
                return status;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }
        }
        public async Task<string> UpdateEmailTemplate(EmailTemplateViewModel obj, string ContollerName, string ActionName)
        {
            string status = "";
            try
            {




                var checkexist = await _context.EmailTemplates
                   .AnyAsync(s => s.ID == obj.ID);


                if (checkexist == true)
                {
                    var model = await _context.EmailTemplates
                    .FirstOrDefaultAsync(x => x.ID == obj.ID);
                    model.IsActive = true;
                    model.Subject = obj.Subject.Trim();
                    model.MailBody = obj.MailBody.Trim();
                    model.LastModified = DateTime.Now;
                    model.ModifiedBy = obj.CreatedBy;
                    model.Code = obj.Code;
                    model.Description = obj.Description;

                    if (await _context.SaveChangesAsync() > 0)
                    {
                        _activityLog.CreateActivityLog(string.Format("Update Email Template with Subject : {0}", obj.Subject), ContollerName, ActionName, obj.CreatedBy, obj);


                        status = ResponseErrorMessageUtility.SuccessfulAndUpdated;
                        return status;
                    }
                    else
                    {
                        status = ResponseErrorMessageUtility.RecordNotSaved;
                        return status;
                    }
                }
                status = string.Format(ResponseErrorMessageUtility.RecordExistBefore, obj.Subject.ToUpper());
                return status;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }
        }
        public async Task<bool> DeleteEmailTemplate(long id, string ContollerName, string ActionName, string ModifiedBy)
        {
            try
            {
                var model = await _context.EmailTemplates
                    .FirstOrDefaultAsync(x => x.ID == id);

                model.IsActive = false;
                model.ModifiedBy = ModifiedBy;
                model.LastModified = DateTime.Now;
                model.IsDeleted = true;

                if (await _context.SaveChangesAsync() > 0)
                {
                    _activityLog.CreateActivityLog(string.Format("Delete Email Template with Subject : {0}", model.Subject), ContollerName, ActionName, model.CreatedBy, model);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
        public async Task<List<EmailTemplateViewModel>> GetAllEmailTemplates(bool? status)
        {

            var modelResponse = new List<EmailTemplateViewModel>();

            var model = new EmailTemplateViewModel();

            try
            {

                if (status == null)
                {
                    return await _context.EmailTemplates.Where(a => a.IsDeleted == false)
                    .Select(modelResponse => new EmailTemplateViewModel
                    {
                        Subject = modelResponse.Subject,
                        ID = modelResponse.ID,
                        MailBody = modelResponse.MailBody,
                        EmailTemplateId = model.EmailTemplateId,
                        Code = modelResponse.Code,
                        LastModified = modelResponse.LastModified,
                        ModifiedBy = modelResponse.ModifiedBy,
                        CreatedDate = DateTime.Now,
                        IsActive = (bool)modelResponse.IsActive,
                        CreatedBy = modelResponse.CreatedBy,
                        Description = modelResponse.Description,
                        DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                        ResponseMessage = ResponseErrorMessageUtility.RecordFetched

                    })
                    .ToListAsync();
                }
                else
                {
                    return await _context.EmailTemplates

                    .Where(x => x.IsActive == status && x.IsDeleted != false)
                    .Select(modelResponse => new EmailTemplateViewModel
                    {
                        Subject = modelResponse.Subject,
                        ID = modelResponse.ID,
                        MailBody = modelResponse.MailBody,
                        EmailTemplateId = model.EmailTemplateId,
                        Code = modelResponse.Code,
                        LastModified = modelResponse.LastModified,
                        ModifiedBy = modelResponse.ModifiedBy,
                        CreatedDate = DateTime.Now,
                        IsActive = (bool)modelResponse.IsActive,
                        CreatedBy = modelResponse.CreatedBy,
                        DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                        ResponseMessage = ResponseErrorMessageUtility.RecordFetched

                    })
                    .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                model.DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_Fail;
                model.ResponseMessage = ResponseErrorMessageUtility.RecordNotFetched + " " + ex.Message;
                modelResponse.Add(model);
                return modelResponse;
            }
        }
        public async Task<EmailTemplateViewModel> GetEmailTemplateById(long id, string ControllerName, string ActionName)
        {
            EmailTemplateViewModel model = new EmailTemplateViewModel();

            try
            {

                return await _context.EmailTemplates
                .Where(x => x.ID == id)
                .Select(model => new EmailTemplateViewModel
                {
                    Subject = model.Subject,
                    ID = model.ID,
                    MailBody = model.MailBody,
                    EmailTemplateId = model.ID,
                    Code = model.Code,
                    LastModified = model.LastModified,
                    ModifiedBy = model.ModifiedBy,
                    CreatedDate = DateTime.Now,
                    IsActive = model.IsActive,
                    CreatedBy = model.CreatedBy,
                    Description = model.Description,
                    DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                    ResponseMessage = ResponseErrorMessageUtility.RecordFetched



                })
                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                model.DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_Fail;
                model.ResponseMessage = ResponseErrorMessageUtility.RecordNotFetched + " " + ex.Message;
                return model;
            }
        }
        public async Task<EmailTemplateViewModel> GetEmailTemplateByCode(string Code)
        {
            EmailTemplateViewModel model = new EmailTemplateViewModel();

            try
            {
                return await _context.EmailTemplates
                .Where(x => x.Code.ToLower() == Code.ToLower())
                .Select(model => new EmailTemplateViewModel
                {
                    Subject = model.Subject,
                    MailBody = model.MailBody,
                    Code = model.Code,
                    Description = model.Description,
                    DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                    ResponseMessage = ResponseErrorMessageUtility.RecordFetched

                })
                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                model.DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_Fail;
                model.ResponseMessage = ResponseErrorMessageUtility.RecordNotFetched + " " + ex.Message;
                return model;
            }
        }
        #endregion

        #region Emailing  

        public async Task<bool> PrepareEmailLog(string EmailTemplateCode, string emailTo, string cc
      , string bcc,
            List<EmailTokenViewModel> emailTokens, string createdBy, DateTime SendDate, bool EmailScheduler)
        {
            string status = "";

            EmailLogViewModel emailLogViewModel = new EmailLogViewModel();
            try
            {
                var emailTemplate = await GetEmailTemplateByCode(EmailTemplateCode);


                StringBuilder sbEmailBody = new StringBuilder();

                sbEmailBody.Append(emailTemplate.MailBody);

                foreach (var token in emailTokens)
                {
                    sbEmailBody = sbEmailBody.Replace(token.Token, token.TokenValue);
                }

                EmailLogViewModel newEmailLog = new EmailLogViewModel
                {
                    Receiver = emailTo,
                    CC = string.IsNullOrEmpty(cc) ? null : cc,
                    BCC = string.IsNullOrEmpty(bcc) ? null : bcc,
                    MessageBody = sbEmailBody.ToString(),
                    Subject = emailTemplate.Subject,
                };



                if (newEmailLog != null)
                {
                    return SendEmail(newEmailLog, EmailScheduler, createdBy);
                }
                else
                {
                    return false;
                }



            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return false;
            }
        }


        public async Task<bool> PrepareEmailLog(string EmailTemplateCode, string emailTo, string cc
     , string bcc,
           List<EmailTokenViewModel> emailTokens, string createdBy, bool EmailScheduler, bool TranslateMessage = false, string TranslateContent = "")
        {
            string status = "";

            EmailLogViewModel emailLogViewModel = new EmailLogViewModel();
            try
            {
                APPContext dbdata = new APPContext();

                var emailTemplate = dbdata.EmailTemplates.Where(x => x.Code.ToLower() == EmailTemplateCode.ToLower()).FirstOrDefault();


                StringBuilder sbEmailBody = new StringBuilder();

                sbEmailBody.Append(emailTemplate.MailBody);

                foreach (var token in emailTokens)
                {
                    sbEmailBody = sbEmailBody.Replace(token.Token, token.TokenValue);
                }

                string newMessage = "";

                newMessage = sbEmailBody.ToString();

                EmailLogViewModel newEmailLog = new EmailLogViewModel
                {
                    Receiver = emailTo,
                    CC = string.IsNullOrEmpty(cc) ? null : cc,
                    BCC = string.IsNullOrEmpty(bcc) ? null : bcc,
                    MessageBody = sbEmailBody.ToString(),
                    Subject = emailTemplate.Subject,
                };



                if (newEmailLog != null)
                {
                    return SendEmail(newEmailLog, EmailScheduler, createdBy);
                }
                else
                {
                    return false;
                }



            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return false;
            }
        }


        public async Task<string> PrepareMultipleEmailLog(string EmailTemplateCode, List<string> emailTo, string cc
   , string bcc,
         List<EmailTokenViewModel> emailTokens, string createdBy, bool EmailScheduler, bool TranslateMessage = false, string TranslateContent = "")
        {
            string status = "";

            EmailLogViewModel emailLogViewModel = new EmailLogViewModel();
            try
            {
                APPContext dbdata = new APPContext();

                var emailTemplate = await dbdata.EmailTemplates.Where(x => x.Code.ToLower() == EmailTemplateCode.ToLower()).FirstOrDefaultAsync();


                StringBuilder sbEmailBody = new StringBuilder();

                sbEmailBody.Append(emailTemplate.MailBody);

                foreach (var token in emailTokens)
                {
                    sbEmailBody = sbEmailBody.Replace(token.Token, token.TokenValue);
                }

                string newMessage = "";

                newMessage = sbEmailBody.ToString();

                foreach (var email in emailTo)
                {

                    EmailLogViewModel newEmailLog = new EmailLogViewModel
                    {
                        Receiver = email,
                        CC = string.IsNullOrEmpty(cc) ? null : cc,
                        BCC = string.IsNullOrEmpty(bcc) ? null : bcc,
                        MessageBody = sbEmailBody.ToString(),
                        Subject = emailTemplate.Subject,
                    };



                    if (newEmailLog != null)
                    {

                        var response =  SendEmail(newEmailLog, EmailScheduler, createdBy);
                    }
                    else
                    {
                        return ResponseErrorMessageUtility.FAIL ;
                    }
                }

                return ResponseErrorMessageUtility.succesful;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return ResponseErrorMessageUtility.FAIL;
            }
        }


        private void LogEmail(EmailLog email)
        {
            CreateEmailLog(email);

        }



        public bool SendEmail(EmailLogViewModel emailModel, bool IsEmailScheduler, string createdBy)
        {
            string _smtpusername = "";
            string _smtppassword = "";
            string _smtpHost = "";

            string _smtpEnableSsl = "";
            string _SenderId = "";
            string _smtpPort = "";

            APPContext aPPContext = new APPContext();


            var setupResponse = aPPContext.SystemSetting.Where(a => a.LookUpCode.ToUpper() == ResponseErrorMessageUtility.SMTP_LOOKUP.ToUpper());

            if (setupResponse.Count() > 0)
            {


                foreach (var response in setupResponse)
                {

                    if (response.ItemName.ToUpper() == ResponseErrorMessageUtility._smtpusername.ToUpper())
                        _smtpusername = response.ItemValue;

                    if (response.ItemName.ToUpper() == ResponseErrorMessageUtility._smtppassword.ToUpper())
                        _smtppassword = response.ItemValue;


                    if (response.ItemName.ToUpper() == ResponseErrorMessageUtility._smtpHost.ToUpper())
                        _smtpHost = response.ItemValue;

                    if (response.ItemName.ToUpper() == ResponseErrorMessageUtility._smtpPort.ToUpper())
                        _smtpPort = response.ItemValue;


                    if (response.ItemName.ToUpper() == ResponseErrorMessageUtility._smtpEnableSsl.ToUpper())
                        _smtpEnableSsl = response.ItemValue;

                    if (response.ItemName.ToUpper() == ResponseErrorMessageUtility._SenderId.ToUpper())
                        _SenderId = response.ItemValue;
                }


                EmailLog email = new EmailLog
                {
                    Receiver = emailModel.Receiver,
                    CC = emailModel.CC,
                    BCC = emailModel.BCC,
                    Subject = emailModel.Subject,
                    MessageBody = emailModel.MessageBody,
                    CreatedDate = DateTime.Now,
                    DateToSend = DateTime.Now,
                    Sender = _SenderId,
                    IsSent = false,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = createdBy

                };



                try
                {

                    // Wrap up the Logo Image Here


                    var message = new MailMessage
                    {
                        Sender = new MailAddress(email.Sender),
                        From = new MailAddress(email.Sender),
                        Subject = email.Subject,
                        Priority = MailPriority.High,
                        IsBodyHtml = true,
                        Body = HttpUtility.HtmlDecode(emailModel.MessageBody),
                    };

                    var emailToCol = email.Receiver.Split(',');
                    if (emailToCol.Count() == 0)
                    {
                        throw new ArgumentException("Email to not supplied");
                    }

                    foreach (string emailTo in emailToCol)
                    {
                        if (Regex.IsMatch(emailTo, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
                        {
                            message.To.Add(new MailAddress(emailTo));
                        }
                    }

                    if (!string.IsNullOrEmpty(emailModel.CC))
                    {
                        var ccCol = emailModel.CC.Split(',');
                        foreach (string cc in ccCol)
                        {
                            if (Regex.IsMatch(cc, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
                            {
                                message.CC.Add(new MailAddress(cc));
                            }

                        }
                    }

                    if (!string.IsNullOrEmpty(emailModel.BCC))
                    {
                        var bccCol = emailModel.BCC.Split(',');
                        foreach (string bcc in bccCol)
                        {
                            if (Regex.IsMatch(bcc, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
                            {
                                message.CC.Add(new MailAddress(bcc));
                            }

                        }
                    }



                    var smtpClient = new SmtpClient
                    {
                        Host = _smtpHost,
                        Port = Convert.ToInt32(_smtpPort),
                        EnableSsl = Convert.ToBoolean(_smtpEnableSsl),
                        DeliveryMethod = 0,
                        UseDefaultCredentials = false,
                        Credentials = new System.Net.NetworkCredential(_smtpusername, _smtppassword)
                    };


                    smtpClient.Send(message);
                }
                catch (Exception ex)
                {
                    //log mail if this is not email scheduler email
                    if (!IsEmailScheduler)
                    {
                        LogEmail(email);
                    }

                    return false;
                }

                email.IsSent = true;
                if (!IsEmailScheduler)
                {
                    LogEmail(email);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SendEmail(EmailLogViewModel emailModel, string createdBy)
        {
            string _smtpusername = "";
            string _smtppassword = "";
            string _smtpHost = "";

            string _smtpEnableSsl = "";
            string _SenderId = "";
            string _smtpPort = "";


            var setupResponse = _context.SystemSetting.Where(a => a.LookUpCode.ToUpper() == ResponseErrorMessageUtility.SMTP_LOOKUP.ToUpper());

            if (setupResponse.Count() > 0)
            {


                foreach (var response in setupResponse)
                {

                    if (response.ItemName.ToUpper() == ResponseErrorMessageUtility._smtpusername.ToUpper())
                        _smtpusername = response.ItemValue;

                    if (response.ItemName.ToUpper() == ResponseErrorMessageUtility._smtppassword.ToUpper())
                        _smtppassword = response.ItemValue;


                    if (response.ItemName.ToUpper() == ResponseErrorMessageUtility._smtpHost.ToUpper())
                        _smtpHost = response.ItemValue;

                    if (response.ItemName.ToUpper() == ResponseErrorMessageUtility._smtpPort.ToUpper())
                        _smtpPort = response.ItemValue;


                    if (response.ItemName.ToUpper() == ResponseErrorMessageUtility._smtpEnableSsl.ToUpper())
                        _smtpEnableSsl = response.ItemValue;

                    if (response.ItemName.ToUpper() == ResponseErrorMessageUtility._SenderId.ToUpper())
                        _SenderId = response.ItemValue;
                }


                EmailLog email = new EmailLog
                {
                    Receiver = emailModel.Receiver,
                    CC = emailModel.CC,
                    BCC = emailModel.BCC,
                    Subject = emailModel.Subject,
                    MessageBody = emailModel.MessageBody,
                    CreatedDate = DateTime.Now,
                    DateToSend = DateTime.Now,
                    Sender = _SenderId,
                    IsSent = false,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = createdBy

                };



                try
                {

                    // Wrap up the Logo Image Here


                    var message = new MailMessage
                    {
                        Sender = new MailAddress(email.Sender),
                        From = new MailAddress(email.Sender),
                        Subject = email.Subject,
                        Priority = MailPriority.High,
                        IsBodyHtml = true,
                        Body = HttpUtility.HtmlDecode(emailModel.MessageBody),
                    };

                    var emailToCol = email.Receiver.Split(',');
                    if (emailToCol.Count() == 0)
                    {
                        throw new ArgumentException("Email to not supplied");
                    }

                    foreach (string emailTo in emailToCol)
                    {
                        if (Regex.IsMatch(emailTo, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
                        {
                            message.To.Add(new MailAddress(emailTo));
                        }
                    }

                    if (!string.IsNullOrEmpty(emailModel.CC))
                    {
                        var ccCol = emailModel.CC.Split(',');
                        foreach (string cc in ccCol)
                        {
                            if (Regex.IsMatch(cc, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
                            {
                                message.CC.Add(new MailAddress(cc));
                            }

                        }
                    }

                    if (!string.IsNullOrEmpty(emailModel.BCC))
                    {
                        var bccCol = emailModel.BCC.Split(',');
                        foreach (string bcc in bccCol)
                        {
                            if (Regex.IsMatch(bcc, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
                            {
                                message.CC.Add(new MailAddress(bcc));
                            }

                        }
                    }



                    var smtpClient = new SmtpClient
                    {
                        Host = _smtpHost,
                        Port = Convert.ToInt32(_smtpPort),
                        EnableSsl = Convert.ToBoolean(_smtpEnableSsl),
                        DeliveryMethod = 0,
                        UseDefaultCredentials = false,
                        Credentials = new System.Net.NetworkCredential(_smtpusername, _smtppassword)
                    };


                    smtpClient.Send(message);
                }
                catch (Exception ex)
                {
                    //log mail if this is not email scheduler email

                    return false;
                }


                return true;
            }
            else
            {
                return false;
            }
        }





        #endregion

        #region EmailAttachments
        public void CreateEmailLog(EmailLog obj)
        {
            try
            {

                obj.DateSent = null;
                obj.IsSent = false;
                _context.EmailLogs.Add(obj);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

            }
        }

        #endregion

    }
}