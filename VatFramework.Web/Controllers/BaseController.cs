
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VatFramework.Web.Filters;


namespace VatFramework.Web.Controllers
{
    public class BaseController : Controller
    {
      
        
        public string GetControllerName(Controller controller )
        {
           
             return ControllerContext.RouteData.Values["controller"].ToString();


           
        }

        /// <summary>
        /// https://benfoster.io/blog/aspnet-identity-stripped-bare-mvc-part-1
        /// </summary>
        public AppUser CurrentUser
        {
            get
            {
                return new AppUser(this.User as ClaimsPrincipal);
            }
        }


        public  string GetControllerAndActionName(Controller controller)
        {
            string actionName = ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();

            return "Controller: " + controllerName + (!string.IsNullOrEmpty(actionName) ? " ; Action Name: " + actionName : string.Empty);
        }
        protected void CreateViewBagParams()
        {
            ViewBag.IsUpdate = false;
            ViewBag.ProjectTitle = "FRSC Inventory Management  System ";
            ViewBag.ModalTitle = "Add";
            ViewBag.ButtonAction = "Save";
            ViewBag.PostAction = "Create";
            ViewBag.ImageProperty = "fileinput-new";
            ViewBag.ImageProperty2 = "fileinput-new";
            ViewBag.ButtonActionCss = "btn btn-primary glow mr-1 mb-1";
            ViewBag.ButtonActionAddIcon = "fa fa-plus-circle";
           
            ViewBag.ButtonActionCloseIcon = "fa fa-plus-circle";
        }

        protected void ImportViewBagParams()
        {
            ViewBag.IsUpdate = false;
            ViewBag.ProjectTitle = "FRSC Inventory Management  System";
            ViewBag.ModalTitle = "Import Users";
            ViewBag.ButtonAction = "Save";
            ViewBag.PostAction = "Import";
            ViewBag.ImageProperty = "fileinput-new";
            ViewBag.ImageProperty2 = "fileinput-new";
            ViewBag.ButtonActionCss = "btn btn-primary glow mr-1 mb-1";
            ViewBag.ButtonActionAddIcon = "fa fa-plus-circle";

            ViewBag.ButtonActionCloseIcon = "fa fa-plus-circle";
        }

        protected void ListViewBagParams()
        {
            ViewBag.IsUpdate = false;
            ViewBag.ProjectTitle = "FRSC Inventory Management  System";
            ViewBag.ModalTitle = "Add";
            ViewBag.ButtonAction = "Save";
            ViewBag.PostAction = "Create";
            ViewBag.ImageProperty = "fileinput-new";
            ViewBag.ImageProperty2 = "fileinput-new";
            ViewBag.ButtonActionCss = "btn btn-primary glow mr-1 mb-1";
            ViewBag.ButtonActionAddIcon = "fa fa-plus-circle";
            ViewBag.ButtonActionCloseIcon = "fa fa-plus-circle";
        }
        protected void EditViewBagParams()
        {
            ViewBag.ProjectTitle = "FRSC Inventory Management  System";
            ViewBag.IsUpdate = true;
            ViewBag.ModalTitle = "Edit";
            ViewBag.ButtonAction = "Update";
            ViewBag.PostAction = "Edit";
            ViewBag.ButtonActionCss = "btn btn-primary glow mr-1 mb-1";

        }


        protected void DeleteViewBagParams()
        {
            ViewBag.ProjectTitle = "FRSC Inventory Management  System ";
            ViewBag.IsUpdate = true;
            ViewBag.ModalTitle = "Delete";
            ViewBag.ButtonAction = "Delete";
            ViewBag.PostAction = "Delete";
            ViewBag.ButtonActionCss = "btn btn-danger shadow mr-1 mb-1";
        }


        protected void ResetPasswordViewBagParams()
        {
            ViewBag.ProjectTitle = "FRSC Inventory Management  System ";
            ViewBag.IsUpdate = true;
            ViewBag.ModalTitle = "Reset Password";
            ViewBag.ButtonAction = "ResetPassword";
            ViewBag.PostAction = "ResetPassword";
            ViewBag.ButtonActionCss = "btn btn-danger shadow mr-1 mb-1";
        }


        protected void EditViewBagParams(string imageUrl)
        {
            ViewBag.ProjectTitle = "FRSC Inventory Management  System ";
            ViewBag.IsUpdate = true;
            ViewBag.ModalTitle = "Edit";
            ViewBag.ButtonAction = "Update";
            ViewBag.PostAction = "Edit";
            ViewBag.ImageProperty = string.IsNullOrEmpty(imageUrl) ? "fileinput-new" : "fileinput-exists";
            ViewBag.ImageProperty2 = "fileinput-exists";
            ViewBag.ButtonActionCss = "btn btn-dark shadow mr-1 mb-1";
        }

        protected void ConfirmViewBagParams()
        {
            ViewBag.ProjectTitle = "FRSC Inventory Management  System";
            ViewBag.IsUpdate = true;
            ViewBag.ModalTitle = "Confirm";
            ViewBag.ButtonAction = "Confirm";
            ViewBag.ConfirmAction = "Confirm";
            ViewBag.ButtonActionCss = "btn btn-primary glow mr-1 mb-1";

        }

        protected string GetCurrentUserId()
        {
            return User.Identity.GetUserId();
            //.GetUserId<string>();
        }
        public string getAction()
        {
                    return ControllerContext.ActionDescriptor.ActionName;
        }

        public string getController()
        {
            return ControllerContext.ActionDescriptor.ControllerName;
        }
    }
}