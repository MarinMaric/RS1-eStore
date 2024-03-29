﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore_web.EF;
using eStore_web.Services;
using eStore_web.SignalR;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eStore_web
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
            services.AddCors(options => options.AddPolicy("MyPolicy", builder =>
            {

                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<IMyUser, MyUserService>();
            services.AddScoped<INotifikacije, NotifikacijeService>();

            services.AddSignalR();
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IAntiforgery antiforgery)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            //----------------------------
            app.UseSession();
            app.UseCors("MyPolicy");
            //----------------------------
            //app.UseAuthentication();
            app.UseRouting();
            app.UseEndpoints(endpoints=> { 
                endpoints.MapHub<SignalRHub>("/notifikacije");
                endpoints.MapControllerRoute("default", "{controller=Kupac}/{action=Index}/{id?}");
            });



            //app.UseSignalR(routes =>
            //{
            //    routes.MapHub<SignalRHub>("/notifikacije");
            //});
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Kupac}/{action=Index}/{id?}");

            //});
        }
    }
}
