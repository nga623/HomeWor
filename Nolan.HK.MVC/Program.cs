using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nolan.WebApi.Shared;
using System.Reflection;
using System.Threading;
 

namespace Nolan.HK.MVC
{
    public class ThreadPoolSettings
    {
        public int MinThreads { get; set; } = 300;
        public int MinCompletionPortThreads { get; set; } = 300;
        public int MaxThreads { get; set; } = 32767;
        public int MaxCompletionPortThreads { get; set; } = 1000;
    }
    
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var hostBuilder = CreateHostBuilder(args);
            var host = hostBuilder.Build();
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
