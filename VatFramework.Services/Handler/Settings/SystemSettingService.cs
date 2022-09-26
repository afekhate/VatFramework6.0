using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VatFramework.Models;
using VatFramework.Models.DataObjects.Settings;
using VatFramework.Services.Contract.EntityService;
using VatFramework.Services.Contract.Settings;
using VatFramework.Utilities.ExceptionUtility;

namespace VatFramework.Services.Handler.Settings
{
    public class SystemSettingService : ISystemSetting
    {
        private readonly APPContext _context;
        private readonly ILogger<SystemSettingService> _logger;
        private readonly IActivityLog _activityRepo;

        public SystemSettingService(APPContext context,
            ILogger<SystemSettingService> logger, IActivityLog activityRepo)
        {
            _context = context;
            _logger = logger;
            _activityRepo = activityRepo;
        }

        #region System Setting Services
        /// <summary>
        /// Create System Setting
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>

        public async Task<string> CreateSystemSetting(SystemSettingViewModel obj, string ControllerName, string ActionName)
        {
            string status = "";
            try
            {

                var checkexist = _context.SystemSetting
                    .Where(x => x.ItemName.ToUpper() == obj.ItemName.ToUpper()).ToList().Count();

                if (checkexist == 0)
                {
                    Models.Domains.Settings.SystemSetting newrecord = new Models.Domains.Settings.SystemSetting
                    {
                        LookUpCode = obj.LookUpCode,
                        ItemName = obj.ItemName,
                        ItemValue = obj.ItemValue,
                        CreatedDate = DateTime.Now,
                        CreatedBy = obj.CreatedBy,
                        IsActive = obj.IsActive,
                        IsDeleted = false,

                    };

                    _context.SystemSetting.Add(newrecord);
                    if (await _context.SaveChangesAsync() > 0)
                    {

                        _activityRepo.CreateActivityLog(string.Format("Added a new system settings request with Name : {0}", newrecord.ItemName), ControllerName, ActionName, obj.CreatedBy, newrecord);

                        status = ResponseErrorMessageUtility.RecordSaved;
                        return status;
                    }
                    else
                    {
                        status = ResponseErrorMessageUtility.RecordNotSaved;
                        return status;
                    }
                }
                status = string.Format(ResponseErrorMessageUtility.RecordExistBefore, obj.ItemName);
                return status;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }
        }

        public async Task<bool> DeleteSystemSetting(long id, string ModifiedBy, string ControllerName, string ActionName)
        {
            try
            {
                var model = _context.SystemSetting
                    .FirstOrDefault(x => x.ID == id);

                model.IsActive = false;
                model.ModifiedBy = ModifiedBy;
                model.LastModified = DateTime.Now;
                model.IsDeleted = false;

                if (await _context.SaveChangesAsync() > 0)
                {
                    _activityRepo.CreateActivityLog(string.Format("Deleted an  existing system setting request with Name : {0}", model.ItemName), ControllerName, ActionName, model.CreatedBy, model);

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


        public async Task<List<SystemSettingViewModel>> GetSystemSettingByLookUpCode(string LookUpCode)
        {
            SystemSettingViewModel model = new SystemSettingViewModel();

            try
            {

                return _context.SystemSetting
                .Where(x => x.LookUpCode.ToLower() == LookUpCode.ToLower() && x.IsDeleted == false)
                .Select(modelResponse => new SystemSettingViewModel
                {
                    LookUpCode = modelResponse.LookUpCode,
                    ID = modelResponse.ID,
                    ItemName = modelResponse.ItemName,
                    CreatedDate = DateTime.Now,
                    ItemValue = modelResponse.ItemValue,
                    IsActive = (bool)modelResponse.IsActive,
                    CreatedBy = modelResponse.CreatedBy,
                    ModifiedBy = modelResponse.ModifiedBy,
                    LastModified = modelResponse.LastModified,

                    DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                    ResponseMessage = ResponseErrorMessageUtility.RecordFetched

                })
                .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                model.DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_Fail;
                model.ResponseMessage = ResponseErrorMessageUtility.RecordNotFetched + " " + ex.Message;
                return null;
            }
        }


        public async Task<SystemSettingViewModel> GetSystemSettingById(long id, string ControllerName, string ActionName)
        {
            SystemSettingViewModel model = new SystemSettingViewModel();

            try
            {

                return _context.SystemSetting
                .Where(x => x.ID == id && x.IsDeleted == false)
                .Select(modelResponse => new SystemSettingViewModel
                {
                    LookUpCode = modelResponse.LookUpCode,
                    ID = modelResponse.ID,
                    ItemName = modelResponse.ItemName,
                    CreatedDate = DateTime.Now,
                    ItemValue = modelResponse.ItemValue,
                    IsActive = (bool)modelResponse.IsActive,
                    CreatedBy = modelResponse.CreatedBy,
                    ModifiedBy = modelResponse.ModifiedBy,
                    LastModified = modelResponse.LastModified,

                    DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                    ResponseMessage = ResponseErrorMessageUtility.RecordFetched

                })
                .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                model.DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_Fail;
                model.ResponseMessage = ResponseErrorMessageUtility.RecordNotFetched + " " + ex.Message;
                return model;
            }
        }

        public async Task<List<SystemSettingViewModel>> GetSystemSettings(bool? status)
        {
            var modelResponse = new List<SystemSettingViewModel>();

            var model = new SystemSettingViewModel();

            try
            {

                if (status == null)
                {
                    return _context.SystemSetting.Where(a => a.IsDeleted == false)
                     .Select(modelResponse => new SystemSettingViewModel
                     {
                         LookUpCode = modelResponse.LookUpCode,
                         ID = modelResponse.ID,
                         ItemName = modelResponse.ItemName,
                         CreatedDate = DateTime.Now,
                         ItemValue = modelResponse.ItemValue,
                         IsActive = (bool)modelResponse.IsActive,
                         CreatedBy = modelResponse.CreatedBy,
                         ModifiedBy = modelResponse.ModifiedBy,
                         LastModified = modelResponse.LastModified,

                         DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                         ResponseMessage = ResponseErrorMessageUtility.RecordFetched

                     })
                    .ToList();
                }
                else
                {
                    return _context.SystemSetting
                    .Where(x => x.IsActive == status)

                    .Select(modelResponse => new SystemSettingViewModel
                    {
                        LookUpCode = modelResponse.LookUpCode,
                        ID = modelResponse.ID,
                        ItemName = modelResponse.ItemName,
                        CreatedDate = DateTime.Now,
                        ItemValue = modelResponse.ItemValue,
                        IsActive = (bool)modelResponse.IsActive,
                        CreatedBy = modelResponse.CreatedBy,
                        ModifiedBy = modelResponse.ModifiedBy,
                        LastModified = modelResponse.LastModified,

                        DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                        ResponseMessage = ResponseErrorMessageUtility.RecordFetched

                    })
                    .ToList();
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

        public async Task<string> UpdateSystemSetting(SystemSettingViewModel obj, long id, string ControllerName, string ActionName)
        {
            string status = "";
            try
            {

                var checkexist = _context.SystemSetting
                   .Where(x => x.ID != id &&
                   (x.ItemName == obj.ItemName)).Count();

                if (checkexist == 0)
                {
                    var model = _context.SystemSetting
                    .FirstOrDefault(x => x.ID == id);
                    model.LookUpCode = obj.LookUpCode;
                    model.ItemName = obj.ItemName;
                    model.ItemValue = obj.ItemValue;
                    model.IsActive = obj.IsActive;
                    model.ModifiedBy = obj.CreatedBy;
                    model.LastModified = DateTime.Now;

                    if (await _context.SaveChangesAsync() > 0)
                    {
                        _activityRepo.CreateActivityLog(string.Format("Updated an existing system settings request with Name : {0}", model.ItemName), ControllerName, ActionName, obj.CreatedBy, model);

                        status = ResponseErrorMessageUtility.SuccessfulAndUpdated;
                        return status;
                    }
                    else
                    {
                        status = ResponseErrorMessageUtility.RecordNotSaved;
                        return status;
                    }
                }
                status = string.Format(ResponseErrorMessageUtility.RecordExistBefore, obj.ItemName);
                return status;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }
        }

        #endregion

    }
}
