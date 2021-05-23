using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FieldAgent.Core.Interfaces.DAL;
using FieldAgent.DAL.Repositories;
using FieldAgent.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.MVC
{
    public class Startup :DbContext
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddControllers();
            services.AddTransient<IAgencyRepository, AgencyRepository>();
            services.AddTransient<IMissionRepository, MissionRepository>();
            services.AddTransient<IReportsRepository>(s => new ReportsRepository(_configuration.GetConnectionString("FieldAgent")));
            services.AddDbContext<FieldAgentContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("FieldAgent")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default",
                pattern: "{controller}/{action}/{id?}");
                endpoints.MapControllers();
            });
        }
    }
}
