using Microsoft.EntityFrameworkCore;
using VatFramework.Models.Domains.Account;

using VatFramework.Models.Domains.Permission;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;

using VatFramework.Models.Domains;
using VatFramework.Models.Domains.EmailTemplate;
using VatFramework.Models.Domains.Settings;
using VatFramework.Models.Domains.ActivityLog;
using VatFramework.Models.Domains.ErrorLog;
using VatFramework.Models.Domains.Icons;

namespace VatFramework.Models
{
    public class APPContext : IdentityDbContext<ApplicationUser,
        ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole,
        IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public APPContext(DbContextOptions<APPContext> options) : base(options)
        {
            //Database.Migrate();

        }

        public APPContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connect = config.GetSection("ConnectionStrings").Get<List<string>>().FirstOrDefault();
            // optionsBuilder.UseSqlServer(connect);
            optionsBuilder.UseSqlServer(connect);

        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-3.1&tabs=visual-studio
        /// </summary>

    


        public virtual DbSet<Icon> Icon { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<ActivityLog> ActivityLog { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public DbSet<ApplicationUserClaim> ApplicationUserClaims { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
       
        public DbSet<ApplicationUserLogin> ApplicationUserLogins { get; set; }
        public DbSet<ApplicationUserPasswordHistory> ApplicationUserPasswordHistorys { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserLoginHistory> UserLoginHistory { get; set; }
        public DbSet<EmailLog> EmailLogs { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        //public DbSet<EmailAttachment> EmailAttachments { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
                
        //public DbSet<State> State { get; set; }

        //public DbSet<Country> Country { get; set; }

        //public DbSet<PrintingStore> PrintingStores { get; set; }

        //public DbSet<EmbossingStore> EmbossingStores { get; set; }

        //public DbSet<PackagingStore> PackagingStores { get; set; }

        //public DbSet<SMSLog> SMSLog { get; set; }

     

        //public DbSet<Application> Application { get; set; }

        //public DbSet<PortalVersion> PortalVersion { get; set; }
        //public DbSet<CaseNoteNumber> CaseNoteNumber { get; set; }

        ////new tables
        //public DbSet<Plant> Plants { get; set; }
       
      

        //public DbSet<Store> Stores { get; set; }

        //public DbSet<StoreBalance> StoreBalances { get; set; }

        //public DbSet<StoreAudit> StoreAudits { get; set; }

        

        //public DbSet<CategoryType> CategoryTypes { get; set; }

        //public DbSet<PlantRequest> PlantRequests { get; set; }


        //public DbSet<RequestCommentHistory> RequestCommentHistories { get; set; }
        //// more tables 
        public DbSet<SystemSetting> SystemSetting { get; set; }


       
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

          
           //builder.Seed();

            builder.Ignore<ApplicationUserLogin>();
          //  builder.Ignore<ApplicationUserRole>();
            builder.Ignore<UserLoginHistory>();

           

            builder.Entity<ApplicationUser>(b => {
                b.HasKey(x => x.Id);
                b.Property(x => x.Id).ValueGeneratedOnAdd();
                b.HasMany(x => x.Roles).WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
            });

            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.Users)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.Roles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            //  builder.Entity<ApplicationUserRole>().HasNoKey();
            //  base.OnModelCreating(builder);
        }


    
    }
}
