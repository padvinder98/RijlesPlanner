using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RijlesPlanner.ApplicationCore.Containers;
using RijlesPlanner.ApplicationCore.Interfaces;
using RijlesPlanner.Data.Connection;
using RijlesPlanner.Data.Repositories;
using RijlesPlanner.IData.Interfaces;
using RijlesPlanner.IData.Interfaces.ConnectionFactory;

namespace RijlesPlanner.UI.MVC
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
            // Enable cookie authentication
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie();

            services.AddControllersWithViews();

            // Register dependencies.
            services.AddScoped<IConnection, Connection>();

            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleContainer, RoleContainer>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserContainer, UserContainer>();

            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<ILessonContainer, LessonContainer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Add authentication to request pipeline
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
