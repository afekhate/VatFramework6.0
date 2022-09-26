using Abp.Extensions;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using VatFramework.Models;
using VatFramework.Models.Domains.Account;
using VatFramework.Services;
using VatFramework.Utilities.ExceptionUtility;


var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;



void WriteLogText(string ex, IWebHostEnvironment env)
{


    //HttpContext context = HttpContext.Current;
    String strErrFileName = String.Format("ErrorLog-{0}.txt", DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("D2") + DateTime.Now.Day.ToString("D2"));
    string path = env.WebRootPath;

    string file = Path.Combine(path, ResponseErrorMessageUtility.PrivateDocumentFolder + "\\ErrorLog.txt");

    StreamWriter writer;
    bool hasInner = false;
    using (writer = new StreamWriter(file, true))
    {
        writer.WriteLine(string.Format("=========================={0}===========================", DateTime.Now));
        writer.WriteLine(ex);

    }


}

// Add services to the container.
builder.Services.AddControllersWithViews();


var connect = config.GetSection("ConnectionStrings").Get<List<string>>().FirstOrDefault();

// this will handle ability to change something in your view and see it reflecting without rebuilding 
builder.Services.AddRazorPages()
.AddRazorRuntimeCompilation();

builder.Services.AddDbContext<APPContext>(options =>
//This is used in .net core for Skip and Take in .Net Core
//

options.UseSqlServer(connect));


builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<APPContext>()
.AddUserManager<UserManager<ApplicationUser>>()
.AddRoleManager<RoleManager<ApplicationRole>>()
.AddSignInManager<SignInManager<ApplicationUser>>()
.AddUserStore<UserStore<ApplicationUser, ApplicationRole, APPContext, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityUserToken<string>, IdentityRoleClaim<string>>>()
.AddRoleStore<RoleStore<ApplicationRole, APPContext, string, ApplicationUserRole, IdentityRoleClaim<string>>>()
.AddDefaultTokenProviders();





// Add Hangfire services.
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(connect, new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        UsePageLocksOnDequeue = true,
        DisableGlobalLocks = true
    }));

// Add the processing server as IHostedService
builder.Services.AddHangfireServer();


//Configure the Secret Key
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Expiration = TimeSpan.FromMinutes(10);
    });

builder.Services.Configure<PasswordHasherOptions>(options => options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2);

builder.Services.AddMvcCore().AddRazorPages(options =>
{
    options.Conventions.AddPageRoute("/Home/Index", "");

}).AddNToastNotifyToastr(new ToastrOptions
{
    ProgressBar = true,
    PositionClass = ToastPositions.TopRight,
    CloseButton = true,
    TimeOut = 8000,
    ExtendedTimeOut = 2000,
});
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    //options.JsonSerializerOptions.
    });

builder.Services.AddMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
});


builder.Services.AddCors(
                options => options.AddPolicy(
                    name: "localhost",
                    builder => builder
                        .WithOrigins(
                            // App:CorsOrigins in appsettings.json can contain more than one address separated by comma.
                            config["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                )
            );

void CreateFireBaseCredential(IWebHostEnvironment env)
{
    WriteLogText("i got here", env);

    string path = env.WebRootPath;
    var googleCredential = Path.Combine(path, ResponseErrorMessageUtility.PrivateDocumentFolder, ResponseErrorMessageUtility.Private_Key);

    FirebaseApp.Create(new AppOptions()
    {
        Credential = GoogleCredential.FromFile(googleCredential)
    });
}



// .NET Native DI Abstraction
RegisterServices(builder.Services);

void RegisterServices(IServiceCollection services)
{
    // Adding dependencies from another layers (isolated from Presentation)

    NativeInjectorBootStrapper.RegisterServices(services);
}







var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.UseNToastNotify();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
