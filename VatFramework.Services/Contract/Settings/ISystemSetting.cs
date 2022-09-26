using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VatFramework.Models.DataObjects.Settings;

namespace VatFramework.Services.Contract.Settings
{
    public interface ISystemSetting
    {
        Task<List<SystemSettingViewModel>> GetSystemSettings(bool? status);

        Task<SystemSettingViewModel> GetSystemSettingById(long id, string ControllerName, string ActionName);
        Task<string> CreateSystemSetting(SystemSettingViewModel obj, string ControllerName, string ActionName);
        Task<string> UpdateSystemSetting(SystemSettingViewModel obj, long id, string ControllerName, string ActionName);
        Task<bool> DeleteSystemSetting(long id, string ModifiedBy, string ControllerName, string ActionName);

        Task<List<SystemSettingViewModel>> GetSystemSettingByLookUpCode(string LookUpCode);
    }
}
