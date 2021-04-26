using CoreApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CoreApplication
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContextPool<AddDBContext>(options => options.UseSqlServer(_config.GetConnectionString("EmployeeDB")));
        //    services.AddIdentity<IdentityUser, IdentityRole>()
        //.AddEntityFrameworkStores<AddDBContext>();
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 2;
                options.Password.RequireNonAlphanumeric = false;
            })
.AddEntityFrameworkStores<AddDBContext>();
            services.AddMvc(option => {
                option.EnableEndpointRouting = false; 
                var policy = new AuthorizationPolicyBuilder()
                       .RequireAuthenticatedUser()
                       .Build();
                option.Filters.Add(new Microsoft.AspNetCore.Mvc.Authorization.AuthorizeFilter(policy)); });
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/Administrator/AccessDenied");
            });

            services.AddScoped<IEmployeeRepository, SqlMockEmployeeRepository>();
            // services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteRolePolicy",
                    policy => policy.RequireClaim("Delete Role").RequireClaim("Edit Role"));
            });

            services.AddAuthorization(options =>
            {
                 options.AddPolicy("EditRolePolicy", policy => policy.RequireAssertion(context =>
        context.User.IsInRole("Admin") &&
        context.User.HasClaim(claim => claim.Type == "Edit Role" && claim.Value == "true") ||
        context.User.IsInRole("Super Admin")
    ));
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminRolePolicy",
                    policy => policy.RequireRole("Admin"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> ilogger)
        {
            if (env.IsDevelopment())
            {
                 app.UseExceptionHandler("/Error");
                //app.UseStatusCodePagesWithRedirects("/Error/{0}");
                //  app.UseDeveloperExceptionPage();
                //app.UseStatusCodePagesWithRedirects("/Error/{0}");
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
            }
            // app.UseDefaultFiles(defaultFilesOptions);
            app.UseStaticFiles();
            app.UseAuthentication();
            // app.UseFileServer();
            app.UseMvc(routes=> {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            //app.UseRouting();


            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

            //});

            //app.Run(async (context) =>
            //{

            //    await context.Response.WriteAsync(_config["MyKey"].ToString() + env.EnvironmentName);
            //});
        }
    }
}
