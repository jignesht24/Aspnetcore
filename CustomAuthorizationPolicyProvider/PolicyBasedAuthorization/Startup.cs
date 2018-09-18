using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolicyBasedAuthorization.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using PolicyBasedAuthorization.Policy;
using Microsoft.AspNetCore.Authorization;

namespace PolicyBasedAuthorization
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI();
            services.AddTransient<IAuthorizationPolicyProvider, MinimumTimeSpendPolicy>();
            services.AddSingleton<IAuthorizationHandler, MinimumTimeSpendHandler>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAuthorization();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            CreateUserAndClaim(serviceProvider).Wait();
        }
        private async Task CreateUserAndClaim(IServiceProvider serviceProvider)
        {
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //Added Roles
            var roleResult = await RoleManager.FindByNameAsync("Administrator");
            if (roleResult == null)
            {
                roleResult = new IdentityRole("Administrator");
                await RoleManager.CreateAsync(roleResult);
            }

            var roleClaimList = (await RoleManager.GetClaimsAsync(roleResult)).Select(p => p.Type);
            if (!roleClaimList.Contains("ManagerPermissions"))
            {
                await RoleManager.AddClaimAsync(roleResult, new Claim("ManagerPermissions", "true"));
            }

            IdentityUser user = await UserManager.FindByEmailAsync("jignesh@gmail.com");

            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = "jignesh@gmail.com",
                    Email = "jignesh@gmail.com",
                };
                await UserManager.CreateAsync(user, "Test@123");
            }

            await UserManager.AddToRoleAsync(user, "Administrator");

            var claimList = (await UserManager.GetClaimsAsync(user)).Select(p => p.Type);
            if (!claimList.Contains("DateOfJoining"))
            {
                await UserManager.AddClaimAsync(user, new Claim("DateOfJoining", "09/25/1984"));
            }
            if (!claimList.Contains("IsAdmin"))
            {
                await UserManager.AddClaimAsync(user, new Claim("IsAdmin", "true"));
            }

            IdentityUser user2 = await UserManager.FindByEmailAsync("rakesh@gmail.com");

            if (user2 == null)
            {
                user2 = new IdentityUser()
                {
                    UserName = "rakesh@gmail.com",
                    Email = "rakesh@gmail.com",
                };
                await UserManager.CreateAsync(user2, "Test@123");
            }
            var claimList1 = (await UserManager.GetClaimsAsync(user2)).Select(p => p.Type);
            if (!claimList1.Contains("IsAdmin"))
            {
                await UserManager.AddClaimAsync(user2, new Claim("IsAdmin", "false"));
            }
            if (!claimList1.Contains("DateOfJoining"))
            {
                await UserManager.AddClaimAsync(user2, new Claim("DateOfJoining", "09/01/2018"));
            }
            if (!claimList1.Contains("IsHR"))
            {
                await UserManager.AddClaimAsync(user2, new Claim("IsHR", "true"));
            }
        }
    }
}
