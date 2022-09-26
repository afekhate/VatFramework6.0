using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VatFramework.Models;
using VatFramework.Services.Contract.Authenication;
using VatFramework.Services.Contract.EmailMessaging;
using VatFramework.Services.Contract.EntityService;
using VatFramework.Services.Contract.ErrorLogger;
using VatFramework.Services.Contract.Permission;
using VatFramework.Services.Contract.Settings;
using VatFramework.Services.Contract.UserService;
using VatFramework.Services.FileHandler;
using VatFramework.Services.Handler.AuditLog;
using VatFramework.Services.Handler.Authenication;
using VatFramework.Services.Handler.EmailMessaging;
using VatFramework.Services.Handler.ErrorLogger;
using VatFramework.Services.Handler.FileHandler;
using VatFramework.Services.Handler.Icon;
using VatFramework.Services.Handler.Permission;
using VatFramework.Services.Handler.RolePermission;
using VatFramework.Services.Handler.RoleServices;
using VatFramework.Services.Handler.Settings;
using VatFramework.Services.Handler.UserService;
using VatFramework.Utilities.AuthenticationUtility.AuthUser;

//using VatFramework.Web.Areas.Admin.Controllers;

namespace VatFramework.Services
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // ASP.NET Authorization Polices
            //Register the Permission policy handlers
            //services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
            //services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
           // services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, AdditionalUserClaimsPrincipalFactory>();
           
            services.AddTransient<DbContext, APPContext>();
            services.AddTransient(typeof(DbContextOptions<APPContext>));
            //services.AddTransient<ShortCodeController>();
            //// Services
            services.AddTransient<IRole, RoleServices>();
            services.AddTransient<IActivityLog, ActivityLogServices>();
            services.AddTransient<IPermission, PermissionServices>();
            services.AddTransient<IRolePermission, RolePermissionService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISystemSetting, SystemSettingService>();
           
            services.AddTransient<IAuthenication, AuthenticationService>();
            services.AddTransient<IErrorLogger, ErrorLoggerService>();
            services.AddTransient<IEmailMessaging, EmailMessagingServices>();
            services.AddTransient<IFileHandler, FileHandlerServices>();

            services.AddTransient<IIconService, IconServices>();





            // Domain.Core - Identity
            services.AddScoped<IAuthUser, AuthUser>();
            services.AddScoped<IAuthenication, AuthenticationService>();


        }
    }
}
