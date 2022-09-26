using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using VatFramework.Models;
using VatFramework.Models.DataObjects.ActivityLog;
using VatFramework.Models.Domains.ActivityLog;
using VatFramework.Services.Contract.EntityService;
using VatFramework.Utilities.AuthenticationUtility.AuthUser;
using VatFramework.Utilities.ExceptionUtility;

namespace VatFramework.Services.Handler.AuditLog
{
    public class ActivityLogServices : IActivityLog
    {
        private readonly APPContext _context;
        private readonly IConfiguration _config;
        private readonly ILogger<ActivityLogServices> _logger;
        private readonly IAuthUser _authUser;




        public ActivityLogServices(APPContext context,
          IAuthUser authUser, ILogger<ActivityLogServices> logger)
        {
            _context = context;
            _authUser = authUser;
            _logger = logger;
        }


        public async Task CreateActivityLogAsync(string description, string moduleName, string moduleAction, string userid, object record)
        {

            try
            {

                var alog = new ActivityLog
                {

                    ModuleName = moduleName,
                    ModuleAction = moduleAction,
                    UserId = userid,
                    Description = description,
                    Record = record != null ? JsonConvert.SerializeObject(record) : "N/A",
                    CreatedDate = DateTime.Now,
                    IPAddress = Utilities.Common.GetLocalIPAddress()
                };

                await _context.ActivityLog.AddAsync(alog);
                await _context.SaveChangesAsync();



            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

            }
        }



        public void CreateActivityLog(string description, string moduleName, string moduleAction, string userid, object record)
        {

            try
            {

                var alog = new ActivityLog
                {
                    ModuleName = moduleName,
                    ModuleAction = moduleAction,
                    UserId = userid,
                    Description = description,
                    Record = record != null ? JsonConvert.SerializeObject(record) : "N/A",
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted =false,
                    CreatedBy = userid,
                    LastModified = DateTime.Now,
                    IPAddress = Utilities.Common.GetLocalIPAddress()


                };

                _context.ActivityLog.Add(alog);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

            }
        }



        public async Task<IEnumerable<ActivityLogSearchViewModel>> SearchctivityLog(DateTime? StartDate, DateTime? EndDate, string SearchParameters)
        {
            ActivityLogSearchViewModel model = new ActivityLogSearchViewModel();
            List<ActivityLogSearchViewModel> resultList = new List<ActivityLogSearchViewModel>();
            if (StartDate == null || EndDate == null)
            {
                model.DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_Fail;
                model.ResponseMessage = ResponseErrorMessageUtility.InvalidDate;

                resultList.Add(model);

                return resultList;
            }


            try
            {
                return await _context.ActivityLog.Include(a => a.ApplicationUser)
                                      .Where(x => x.CreatedDate >= Convert.ToDateTime(StartDate) && x.CreatedDate < Convert.ToDateTime(EndDate)
                                      && (x.Description.Contains(SearchParameters) || x.Description.Contains(SearchParameters) || x.Description.Contains(SearchParameters)
                                         || x.Description.Contains(SearchParameters)))

                                         .Select(modelResponse => new ActivityLogSearchViewModel
                                         {
                                             Description = modelResponse.Description,
                                             CreatedDate = modelResponse.CreatedDate,
                                             ModuleAction = modelResponse.ModuleAction,
                                             //CreatedBy = modelResponse.ApplicationUser.FullName,
                                             ModuleName = modelResponse.ModuleName,
                                             LastModified = modelResponse.LastModified,
                                             Record = modelResponse.Record,
                                             IsActive = modelResponse.IsActive,
                                             DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                                             ResponseMessage = ResponseErrorMessageUtility.RecordFetched,
                                            IPAddress = Utilities.Common.GetLocalIPAddress()

                                         }).ToListAsync();
            }


            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                model.DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_Fail;
                model.ResponseMessage = ResponseErrorMessageUtility.RecordNotFetched + " " + ex.Message;
                resultList.Add(model);
                return resultList;
            }
        }
    }
}
