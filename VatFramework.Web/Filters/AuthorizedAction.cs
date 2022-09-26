using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VatFramework.Models.Domains.Permission;

namespace VatFramework.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AuthorizedAction : ActionFilterAttribute
    {
        

    public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {

                base.OnActionExecuting(filterContext);


                if (filterContext.HttpContext.User.Identity.IsAuthenticated == false)
                {
                    var values = new RouteValueDictionary(new
                    {
                        action = "Index",
                        controller = "NonAuthorized",
                        area = ""
                    });

                    filterContext.Result = new RedirectToRouteResult(values);
                }


                var currentMenus = filterContext.HttpContext.Session.GetString("Menus");
                var menus = JsonConvert.DeserializeObject<List<Permission>>(currentMenus);
                var controllerName = filterContext.RouteData.Values["controller"];
                var actionName = filterContext.RouteData.Values["action"];
                var areaName = filterContext.RouteData.DataTokens["area"] as string;
                string url = string.Empty;
                string checkPassword = controllerName.ToString();

                if (checkPassword == "ChangePassword" || checkPassword == "UpdateMyProfile")
                {


                    return;
                }
                
                    if (!string.IsNullOrEmpty(areaName))
                    {
                        url = areaName + "/" + controllerName + "/" + actionName;
                    }
                    else
                    {
                        url = controllerName + "/" + actionName;
                    }

                    if (!menus.Where(s => s.Url.ToLower() == url.ToLower()).Any())
                    {
                        filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary { { "controller", "NonAuthorized" }, { "action", "Index" }, { "area", "" } });
                        return;
                    }
                
            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectToRouteResult(
                       new RouteValueDictionary { { "controller", "NonAuthorized" }, { "action", "Index" }, { "area", "" } });
                return;
            }
        }
    }
}
