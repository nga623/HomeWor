using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nolan.Application.Shared;
using Nolan.HK.Migrations;
using Nolan.Infra.EfCore.PostGresSql;
using Nolan.WebApi.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Nolan.HK.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private IServiceCollection _services;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _services = services;
            services.AddDbContext<HomeWorkContext>(options =>
           options.UseNpgsql(Configuration.GetConnectionString("HomeWorkContext")));
            services.AddControllersWithViews();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
         
            var hostBuilderContext = (HostBuilderContext)_services
            .FirstOrDefault(d => d.ServiceType == typeof(HostBuilderContext))
            ?.ImplementationInstance;

            var configuration = hostBuilderContext.Configuration as IConfigurationRoot;


            var serviceInfo = (IServiceInfo)_services
                .FirstOrDefault(d => d.ServiceType == typeof(IServiceInfo))
                ?.ImplementationInstance;
            

             
             
           
            var applicationAssembly = Assembly.Load(serviceInfo.AssemblyFullName.Replace("MVC", "Application"));
             
            var applicationModelType = applicationAssembly.GetTypes()
                            .FirstOrDefault(m =>
                               m.FullName != null
                               && typeof(NolanApplicationModule).IsAssignableFrom(m)
                               && !m.IsAbstract);

            //builder.RegisterModuleIfNotRegistered<AdncInfraMongoModule>();
             builder.RegisterModule<NolanInfraEfCoreModule>();
            //builder.RegisterModuleIfNotRegistered(new AdncInfraConsulModule(consulUrl));
            //builder.RegisterModuleIfNotRegistered(Activator.CreateInstance(applicationModelType, configuration, serviceInfo) as IModule);
            builder.RegisterModule(Activator.CreateInstance(applicationModelType, configuration, serviceInfo) as IModule);
            Action<ContainerBuilder> completedExecute = null;
            completedExecute?.Invoke(builder);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutofacUtil.Container = app.ApplicationServices.GetAutofacRoot();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
    public class AutofacUtil
    {
        /// <summary>
        /// Autofac依赖注入静态服务
        /// </summary>
        public static ILifetimeScope Container { get; set; }

        /// <summary>
        /// 获取服务(Single)
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <returns></returns>
        public static T GetService<T>() where T : class
        {
            return Container.Resolve<T>();
        }
    }
}
