using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NToastNotify;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using VatFramework.Models;
using VatFramework.Services.Contract.ErrorLogger;
using VatFramework.Utilities.ExceptionUtility;
using System.Collections.Generic;
using VatFramework.Services.Handler.DataAccess;
using System.Data;

using System.Linq;
using VatFramework.Models.DataObjects.ReportDTO;

namespace VatFramework.Web.Controllers
{
    public class DashboardController : BaseController
    {
        
   
        private IToastNotification _toastNotification;
        private readonly log4net.Core.ILogger _logger;
        private readonly APPContext _context;
        private readonly IErrorLogger _errorLogger;
        


        public DashboardController(ILogger<DashboardController> logger,
           APPContext context, IErrorLogger errorLogger, IToastNotification toastNotification)
        {

            _context = context;
            _errorLogger = errorLogger;
            _toastNotification = toastNotification;
           
        }

        public async Task<IActionResult> Index()
        {
            try
            {
               var roleName= HttpContext.Session.GetString("RoleName");
                var dataResult = new DashboardModel();
                if (roleName == null)
                {
                    _toastNotification.AddInfoToastMessage(ResponseErrorMessageUtility.IllegalOperation, null);
                    return RedirectToAction("LogOff", "Account", new { area = "" });
                }
                else
                {
                    AccessDataLayer accessDataLayer = new AccessDataLayer(_context);
                    DBManager dBManager = new DBManager(_context);
                    var parameters = new List<IDbDataParameter>();
                    try
                    {

                        ViewBag.Role = CurrentUser.RoleId;

                        //if (CurrentUser.RoleId == ResponseErrorMessageUtility.CentralAdmin)
                        //{
                        //    parameters.Add(dBManager.CreateParameter("@RoleId", CurrentUser.RoleId, DbType.String));
                        //    parameters.Add(dBManager.CreateParameter("@PlantId", CurrentUser.PlantId, DbType.String));
                        //    parameters.Add(dBManager.CreateParameter("@MiniPlantId", CurrentUser.MiniPlantId, DbType.String));

                        //    DataTable response = accessDataLayer.ProcessStoreProcedure(parameters.ToArray(), ResponseErrorMessageUtility.spCentralAdminDashboard);
                        //    List<ReportData> data = new List<ReportData>();



                        //    foreach (DataRow dt in response.Rows)
                        //    {
                        //        ReportData activityInfo = new ReportData
                        //        {
                        //            Description = dt["CategoryDescription"].ToString(),
                        //            Label = dt["MaterialName"].ToString(),
                        //            Total = dt["Total"].ToString(),

                        //        };

                        //        data.Add(activityInfo);

                        //    }

                        //    dataResult = new DashboardModel
                        //    {
                        //        storeRequests = await _StoreManager.GetStores(null),
                        //        MiniPlantStatus = data

                        //    };
                        //}

                        //else if (CurrentUser.RoleId == ResponseErrorMessageUtility.Plant_Blanking)
                        //{
                           
                        //    var blanks = _miniPlant.GetBlankingReportDetails(null, null);
                        //    var prints = _miniPlant.GetPrintingReportDetails(null, null);

                        //    var treated = new List<VatFramework.Models.DataObjects.MiniPlant.MiniPlantRequestViewModel>();
                        //    var pending = new List<VatFramework.Models.DataObjects.MiniPlant.MiniPlantRequestViewModel>();
                        //    pending = blanks;

                        //    foreach (var request  in prints)
                        //    {
                        //        var oneRequest = blanks.Where(x => x.MiniPlantRequestId == request.MiniPlantRequestId).FirstOrDefault();
                        //        treated.Add(oneRequest);
                        //        pending.Remove(oneRequest);
                               
                        //    }


                        //    dataResult = new DashboardModel
                        //    {

                        //        Treated = treated,
                        //        Pending = pending,
                        //        UnitAnalyses = new List<UnitAnalysis> 
                        //        { 
                        //            new UnitAnalysis { Description = "All Request", Quantity = treated.Count + pending.Count },
                        //            new UnitAnalysis { Description = "Blanked", Quantity = treated.Count },
                        //            new UnitAnalysis { Description = "Pending", Quantity = pending.Count }
                        //        }

                        //    };
                        //}


                        //else if (CurrentUser.RoleId == ResponseErrorMessageUtility.Screen_Printing)
                        //{

                           
                        //    var prints = _miniPlant.GetPrintingReportDetails(null, null);
                        //    var emboss = _miniPlant.GetEmbossingReportDetails(null, null);

                        //    var treated = new List<VatFramework.Models.DataObjects.MiniPlant.MiniPlantRequestViewModel>();
                        //    var pending = new List<VatFramework.Models.DataObjects.MiniPlant.MiniPlantRequestViewModel>();
                        //    pending = prints;

                        //    foreach (var request in emboss)
                        //    {
                        //        var oneRequest = prints.Where(x => x.MiniPlantRequestId == request.MiniPlantRequestId).FirstOrDefault();
                        //        treated.Add(oneRequest);
                        //        pending.Remove(oneRequest);

                        //    }


                        //    dataResult = new DashboardModel
                        //    {

                        //        Treated = treated,
                        //        Pending = pending,
                        //        UnitAnalyses = new List<UnitAnalysis>
                        //        {
                        //            new UnitAnalysis { Description = "All Request", Quantity = treated.Count + pending.Count },
                        //            new UnitAnalysis { Description = "Printed", Quantity = treated.Count },
                        //            new UnitAnalysis { Description = "Pending", Quantity = pending.Count }
                        //        }

                        //    };
                        //}

                        //else if (CurrentUser.RoleId == ResponseErrorMessageUtility.EMBOSSING_Role)
                        //{


                            
                        //    var emboss = _miniPlant.GetEmbossingReportDetails(null, null);
                        //    var packs = _miniPlant.GetPackagingReportDetails(null, null);

                        //    var treated = new List<VatFramework.Models.DataObjects.MiniPlant.MiniPlantRequestViewModel>();
                        //    var pending = new List<VatFramework.Models.DataObjects.MiniPlant.MiniPlantRequestViewModel>();
                        //    pending = emboss;

                        //    foreach (var request in packs)
                        //    {
                        //        var oneRequest = emboss.Where(x => x.MiniPlantRequestId == request.MiniPlantRequestId).FirstOrDefault();
                        //        treated.Add(oneRequest);
                        //        pending.Remove(oneRequest);

                        //    }


                        //    dataResult = new DashboardModel
                        //    {

                        //        Treated = treated,
                        //        Pending = pending,
                        //        UnitAnalyses = new List<UnitAnalysis>
                        //        {
                        //            new UnitAnalysis { Description = "All Request", Quantity = treated.Count + pending.Count },
                        //            new UnitAnalysis { Description = "Embossed", Quantity = treated.Count },
                        //            new UnitAnalysis { Description = "Pending", Quantity = pending.Count }
                        //        }

                        //    };
                        //}

                     
                        //else if (CurrentUser.RoleId == ResponseErrorMessageUtility.MiniAdmin)
                        //{
                        //    parameters.Add(dBManager.CreateParameter("@MiniPlantId", CurrentUser.MiniPlantId, DbType.String));
                        //    DataTable response = accessDataLayer.ProcessStoreProcedure(parameters.ToArray(), ResponseErrorMessageUtility.SpMiniPlantDashboard);
                        //    List<ReportData> data = new List<ReportData>();



                        //    foreach (DataRow dt in response.Rows)
                        //    {
                        //        ReportData activityInfo = new ReportData
                        //        {
                        //            Description = dt["Description"].ToString(),
                        //            Label = dt["Label"].ToString(),
                        //            Total = dt["Total"].ToString(),
                        //            MappingItem = dt["MappingItem"].ToString(),
                        //            PlantName = dt["PlantName"].ToString(),
                        //        };

                        //        data.Add(activityInfo);

                        //    }

                        //    dataResult = new DashboardModel
                        //    {
                        //        miniPlantRequests = _miniPlant.GetPlantMiniRequests(Convert.ToInt64(CurrentUser.MiniPlantId)),
                        //        MiniPlantStatus = data

                        //    };
                        //}
                        //else if (CurrentUser.RoleId == ResponseErrorMessageUtility.PlantAdmin)
                        //{
                        //    parameters.Add(dBManager.CreateParameter("@PlantId", CurrentUser.PlantId, DbType.Int64));
                        //    DataTable response = accessDataLayer.ProcessStoreProcedure(parameters.ToArray(), ResponseErrorMessageUtility.spPlantdminDashboard);
                        //    List<ReportData> data = new List<ReportData>();



                        //    foreach (DataRow dt in response.Rows)
                        //    {
                        //        ReportData activityInfo = new ReportData
                        //        {
                        //            Description = dt["CategoryDescription"].ToString(),
                        //            Label = dt["MaterialName"].ToString(),
                        //            Total = dt["Total"].ToString(),
                        //        };

                        //        data.Add(activityInfo);

                        //    }

                        //    dataResult = new DashboardModel
                        //    {
                        //        plantRequests = await _plantRequestManager.GetPlantRequests(Convert.ToInt64(CurrentUser.PlantId)),
                        //        MiniPlantStatus = data

                        //    };
                        //}
                        //else
                        //{

                        //}

                        ViewBag.roleName = roleName;
                        return View(dataResult);
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                   
                  
                }
            }
            catch (Exception ex)
            {

                return systemError(ex);

              
            }
          
        }

       

        public RedirectToActionResult systemError(Exception ex)
        {
            _errorLogger.LogError(ex, GetControllerAndActionName(this));
            _toastNotification.AddErrorToastMessage(ResponseErrorMessageUtility.OperationFailed_Ex, null);

            ModelState.Clear();

            return RedirectToAction(nameof(Index));
        }

    }
}