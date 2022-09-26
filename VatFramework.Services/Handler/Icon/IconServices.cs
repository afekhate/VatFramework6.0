using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VatFramework.Models;
using VatFramework.Models.DataObjects.icon;
using VatFramework.Models.Domains.Icons;
using VatFramework.Services.Contract.EntityService;
using VatFramework.Services.Handler.DataAccess;
using VatFramework.Utilities.AuthenticationUtility.AuthUser;
using VatFramework.Utilities.ExceptionUtility;

namespace VatFramework.Services.Handler.Icon
{
    public class IconServices : IIconService
    {
        private readonly APPContext _context;
        private readonly IConfiguration _config;
        private readonly ILogger<IconServices> _logger;
        private readonly IActivityLog _activityRepo;



        public IconServices(APPContext context,
          IAuthUser authUser, ILogger<IconServices> logger, IActivityLog activityRepo)
        {
            _context = context;
           
            _logger = logger;
            _activityRepo = activityRepo;
        }


        public async Task<string> CreateIcon(IconViewModel obj, string ControllerName, string ActionName)
        {

            string status = "";
            try
            {

                var checkexist = await _context.Icon
                    .AnyAsync(x => x.IconName.ToUpper() == obj.IconName.ToUpper() ||
                    x.IconCode == obj.IconCode);

                if (checkexist == false)
                {
                    var newrecord = new Models.Domains.Icons.Icon
                    {
                        IconName = obj.IconName,
                        IconCode = obj.IconCode,
                        CreatedDate = DateTime.Now,
                        CreatedBy = obj.CreatedBy,
                        IsActive = obj.IsActive,
                        IsDeleted = false,
                        
                    };

                    _context.Icon.Add(newrecord);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        _activityRepo.CreateActivityLog(string.Format("Added a new menu icon request with Name : {0}", newrecord.IconName), ControllerName, ActionName, obj.CreatedBy, newrecord);

                        status = ResponseErrorMessageUtility.RecordSaved;
                        return status;
                    }
                    else
                    {
                        status = ResponseErrorMessageUtility.RecordNotSaved;
                        return status;
                    }
                }
                status = string.Format(ResponseErrorMessageUtility.RecordExistBefore, obj.IconName);
                return status;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }
        }



        public async Task<IconViewModel> GetIcons(long id)
        {
            IconViewModel model = new IconViewModel();

            try
            {

                return await _context.Icon
                .Where(x => x.ID == id)
                .Select(model => new IconViewModel
                {
                    IconName = model.IconName,
                    ID = model.ID,
                    IconCode = model.IconCode,
                    DateCreated = DateTime.Now,
                    IsActive = (bool)model.IsActive,

                    DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                    ResponseMessage = ResponseErrorMessageUtility.RecordFetched
                }).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                model.DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_Fail;
                model.ResponseMessage = ResponseErrorMessageUtility.RecordNotFetched + " " + ex.Message;

                _logger.LogError(ex.Message);
                return model;
            }

        }




        public async Task<string> UpdateIcon(IconViewModel obj, long id, string ControllerName, string ActionName)
        {
            string status = "";
            try
            {

                var checkexist = await _context.Icon
                   .AnyAsync(x => x.ID != id &&
                   (x.IconName == obj.IconName));

                if (checkexist == false)
                {
                    var model = await _context.Icon
                    .FirstOrDefaultAsync(x => x.ID == id);
                    model.IconCode = obj.IconCode;
                    model.IconName = obj.IconName;
                    model.IsActive = obj.IsActive;
                    model.ModifiedBy = obj.CreatedBy;
                    model.LastModified = DateTime.Now;

                    if (await _context.SaveChangesAsync() > 0)
                    {
                        _activityRepo.CreateActivityLog(string.Format("Updated and existing menu icon request with Name : {0}", model.IconName), ControllerName, ActionName,  obj.CreatedBy, model);

                        status = ResponseErrorMessageUtility.SuccessfulAndUpdated;
                        return status;
                    }
                    else
                    {
                        status = ResponseErrorMessageUtility.RecordNotSaved;
                        return status;
                    }
                }
                status = string.Format(ResponseErrorMessageUtility.RecordExistBefore, obj.IconName);
                return status;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }
        }
       

        public async Task<bool> DeleteIcon(long id, string ModifiedBy, string ControllerName, string ActionName)
        {
            try
            {
                var model = await _context.Icon
                    .FirstOrDefaultAsync(x => x.ID == id);

                model.IsActive = false;
                model.ModifiedBy = ModifiedBy;
                model.LastModified = DateTime.Now;
                model.IsDeleted = false;
                if (await _context.SaveChangesAsync() > 0)
                {
                    _activityRepo.CreateActivityLog(string.Format("Deleted an  existing menu icon request with Name : {0}", model.IconName), ControllerName, ActionName,  model.CreatedBy, model);

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

        public async Task<List<IconViewModel>> GetDeletedIncons()
        {



            List<IconViewModel> modelResponse = new List<IconViewModel>();

            IconViewModel model = new IconViewModel();

            try
            {

                    return await _context.Icon.Where(x=>x.IsDeleted ==true)
                    .Select(modelResponse => new IconViewModel
                    {
                        IconName = modelResponse.IconName,
                        ID = modelResponse.ID,
                        IconCode = modelResponse.IconCode,
                        DateCreated = DateTime.Now,
                        IsActive = (bool)modelResponse.IsActive,
                        CreatedBy = modelResponse.CreatedBy,
                        CreatedDate = modelResponse.CreatedDate,
                        ModifiedBy = modelResponse.ModifiedBy,
                        LastModified = modelResponse.LastModified,

                        DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                        ResponseMessage = ResponseErrorMessageUtility.RecordFetched

                    })
                    .ToListAsync();
                
               
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

        public async Task<List<IconViewModel>> GetIcons(bool? status)
        {
            List<IconViewModel> modelResponse = new List<IconViewModel>();

            IconViewModel model = new IconViewModel();

            try
            {

                if (status == null)
                {
                    return await _context.Icon
                    .Select(modelResponse => new IconViewModel
                    {
                        IconName = modelResponse.IconName,
                        ID = modelResponse.ID,
                        IconCode = modelResponse.IconCode,
                        DateCreated = DateTime.Now,
                        IsActive = (bool)modelResponse.IsActive,
                        CreatedBy = modelResponse.CreatedBy,
                        CreatedDate = modelResponse.CreatedDate,
                        ModifiedBy = modelResponse.ModifiedBy,
                        LastModified = modelResponse.LastModified,

                        DataIntegrity = ResponseErrorMessageUtility.DataIntegrity_OK,
                        ResponseMessage = ResponseErrorMessageUtility.RecordFetched

                    })
                    .ToListAsync();
                }
                else
                {
                    return await _context.Icon
                    .Where(x => x.IsActive == status)
                    .Select(modelResponse => new IconViewModel
                    {
                        IconName = modelResponse.IconName,
                        ID = modelResponse.ID,
                        IconCode = modelResponse.IconCode,
                        DateCreated = modelResponse.CreatedDate,
                        IsActive = (bool)modelResponse.IsActive,
                        CreatedBy = modelResponse.CreatedBy,
                        CreatedDate = modelResponse.CreatedDate,
                        ModifiedBy = modelResponse.ModifiedBy,
                        LastModified = modelResponse.LastModified,

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


        public async Task<IconViewModel> GetIconById(long id, string ControllerName, string ActionName)
        {
            IconViewModel model = new IconViewModel();

            try
            {

                return await _context.Icon
                .Where(x => x.ID == id)
                .Select(model => new IconViewModel
                {
                    IconName = model.IconName,
                    ID = model.ID,
                    IconCode = model.IconCode,
                    DateCreated = DateTime.Now,
                    IsActive = (bool)model.IsActive,

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



    }
}
