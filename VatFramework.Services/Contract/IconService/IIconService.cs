using System.Collections.Generic;
using System.Threading.Tasks;
using VatFramework.Models.DataObjects.icon;


namespace VatFramework.Services.Contract.EntityService
{
    public interface IIconService
    {
        Task<List<IconViewModel>> GetIcons(bool? status);

        Task<List<IconViewModel>> GetDeletedIncons();
        Task<IconViewModel> GetIconById(long id, string ControllerName, string ActionName);
        Task<string> CreateIcon(IconViewModel obj,string ControllerName, string ActionName);
        Task<string> UpdateIcon(IconViewModel obj, long id, string ControllerName, string ActionName);
        Task<bool> DeleteIcon(long id,string ModifiedBy, string ControllerName, string ActionName);
    }
}
