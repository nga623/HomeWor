using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Nolan.Application.Shared;
using Nolan.Domain.Shared.ConfigModels;
using Nolan.Infra.EfCore.PostGresSql;
using Nolan.WebApi.Shared;
using Nolan.WebApi.Shared.Filters;
using Nolan.WebApi.Shared.Log;
using System;
using System.Linq;
using System.Reflection;
using System.Text;


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
           
            services.Configure<JwtSetting>(option =>
            {
                option.SecurityKey = Configuration["JwtSetting:SecurityKey"];
                option.Issuer = Configuration["JwtSetting:Issuer"];
                option.Audience = Configuration["JwtSetting:Audience"];
               
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = false,
                      ValidateActor=false,
                      ValidateAudience=false,
                      ValidIssuer = Configuration["JwtSetting:Issuer"],
                      ValidAudience = Configuration["JwtSetting:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSetting:SecurityKey"])),
                      // 默认允许 300s  的时间偏移量，设置为0
                      ClockSkew = TimeSpan.Zero
                  };
              });

          
            services.AddDbContext<HomeWorkContext>(
                options =>
           options.UseNpgsql(Configuration.GetConnectionString("HomeWorkContext"), optionsBuilder =>
           {
               optionsBuilder.MigrationsAssembly("Nolan.HK.Migrations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
           })
           );
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddControllers(options => options.Filters.Add(typeof(CustomerGlobalExceptionFilterAsync)));
            services.AddSingleton<INLogHelper, NLogHelper>();
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

            builder.RegisterModule<NolanInfraEfCoreModule>();
            builder.RegisterModule(Activator.CreateInstance(applicationModelType, configuration, serviceInfo) as IModule);
            Action<ContainerBuilder> completedExecute = null;
            completedExecute?.Invoke(builder);
        }
         
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
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=User}/{action=Index}/{id?}");
            });
        }
    }
     
}
