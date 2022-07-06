using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPRN211
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}"
                    );
                endpoints.MapControllerRoute(
                    name: "p1",
                    pattern: "{controller}/{action}/{param1}/{param2}"
                    );
                endpoints.MapControllerRoute(
                    name: "p2",
                    pattern: "{controller}/{action}/{param1}/{param2}/{param3}/{param4}"
                    );
                endpoints.MapControllerRoute(
                    name: "p3",
                    pattern: "{controller}/{action}/{param1}/{param2}/{param3}"
                    );
                endpoints.MapControllerRoute(
                    name: "p4",
                    pattern: "{controller}/{action}/{param1}"
                    );
            });
        }
    }
}
