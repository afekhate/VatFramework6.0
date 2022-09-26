using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VatFramework.Models.DataObjects.ActivityLog;

namespace VatFramework.Services.Contract.EntityService
{
    public interface IActivityLog
    {
        #region Activity Log
        Task CreateActivityLogAsync(string description, string controllerName, string actionName, string userid, object record);
        void CreateActivityLog(string description, string controllerName, string actionName, string userid, object record);


        Task<IEnumerable<ActivityLogSearchViewModel>> SearchctivityLog(DateTime? StartDate, DateTime? EndDate, string SearchParameters);
        #endregion
    }
}
