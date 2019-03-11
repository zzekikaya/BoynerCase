using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Boyner.Core.Repository;
using Boyner.Data;
using Boyner.Domain.Entities;
using Core.MessageBroker.Abstract;
using Core.MessageBroker.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boyner.Web.UI
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


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvc();
            //services.AddMvc(opt => opt.EnableEndpointRouting = false);
            //services.AddDbContext<ConfigContext>(
            //    options => options.UseSqlServer(
            //        Configuration.GetConnectionString("BoynerConnection")));

            RegisterServices(services);
        }

        public void RegisterServices(IServiceCollection services)
        {
            services.AddSession();
            services.AddAutoMapper();
            services.AddMvc();
            //services.AddSingleton(Config);

            //services.AddTransient<Configuration>();//seed işlemleri
            services.AddTransient<DbContext, ConfigContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IMessageBroker, MessageBroker>(); 
            services.AddDbContext<ConfigContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("BoynerConnection")));
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
