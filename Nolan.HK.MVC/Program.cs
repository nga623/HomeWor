using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nolan.WebApi.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Nolan.HK.MVC
{
    public class ThreadPoolSettings
    {
        public int MinThreads { get; set; } = 300;
        public int MinCompletionPortThreads { get; set; } = 300;
        public int MaxThreads { get; set; } = 32767;
        public int MaxCompletionPortThreads { get; set; } = 1000;
    }
    public static class HostExtensions
    {
        public static IHost ChangeThreadPoolSettings(this IHost host)
        {
            var poolOptions = host.Services.GetService(typeof(IOptionsMonitor<ThreadPoolSettings>)) as IOptionsMonitor<ThreadPoolSettings>;
            return ChangeThreadPoolSettings(host, poolOptions);
        }

        public static IHost ChangeThreadPoolSettings(this IHost host, IOptionsMonitor<ThreadPoolSettings> poolOptions)
        {
            var logger = host.Services.GetService(typeof(ILogger<IHost>)) as ILogger<IHost>;

            poolOptions.OnChange(poolSetting =>
            {
                ThreadPool.GetMinThreads(out int workerThreads, out int completionPortThreads);
                ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int maxCompletionPortThreads);
                logger.LogInformation("before MinThreads={0},MinCompletionPortThreads={1}", workerThreads, completionPortThreads);
                logger.LogInformation("before MaxThreads={0},MaxCompletionPortThreads={1}", maxWorkerThreads, maxCompletionPortThreads);

                ThreadPool.SetMinThreads(poolSetting.MinThreads, poolSetting.MinCompletionPortThreads);
                ThreadPool.SetMaxThreads(poolSetting.MaxThreads, poolSetting.MaxCompletionPortThreads);

                ThreadPool.GetMinThreads(out int changedWorkerThreads, out int changedCompletionPortThreads);
                ThreadPool.GetMaxThreads(out int changedMaxWorkerThreads, out int changedsMaxCompletionPortThreads);
                logger.LogInformation("changed MinThreads={0},MinCompletionPortThreads={1}", changedWorkerThreads, changedCompletionPortThreads);
                logger.LogInformation("changed MaxThreads={0},MaxCompletionPortThreads={1}", changedMaxWorkerThreads, changedsMaxCompletionPortThreads);
            });

            var poolSetting = poolOptions.CurrentValue;
            ThreadPool.SetMinThreads(poolSetting.MinThreads, poolSetting.MinCompletionPortThreads);
            ThreadPool.SetMaxThreads(poolSetting.MaxThreads, poolSetting.MaxCompletionPortThreads);

            ThreadPool.GetMinThreads(out int workerThreads, out int completionPortThreads);
            ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int maxCompletionPortThreads);
            logger.LogInformation("Setting MinThreads={0},MinCompletionPortThreads={1}", workerThreads, completionPortThreads);
            logger.LogInformation("Setting MaxThreads={0},MaxCompletionPortThreads={1}", maxWorkerThreads, maxCompletionPortThreads);
            return host;
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var hostBuilder = CreateHostBuilder(args);
            var host = hostBuilder.Build();
            host.ChangeThreadPoolSettings();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseServiceProviderFactory(new AutofacServiceProviderFactory())
             .ConfigureServices(services =>
             {
                 services.Add(ServiceDescriptor.Singleton(typeof(IServiceInfo), ServiceInfo.Create(Assembly.GetExecutingAssembly())));
             })
                 ;
    }
}
