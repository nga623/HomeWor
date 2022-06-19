using Autofac;
using Microsoft.Extensions.Configuration;
using Nolan.Infra.Mapper.AutoMapper;
using Nolan.WebApi.Shared;
using System;
using System.Reflection;
using Autofac.Extras.DynamicProxy;
using FluentValidation;
using System.Linq;
using Nolan.Domain.Shared;
using Nolan.Infra.Repository;

namespace Nolan.Application.Shared
{
    public abstract class NolanApplicationModule : Autofac.Module
    {
        private readonly Assembly _appAssemblieToScan;
        private readonly Assembly _appContractsAssemblieToScan;
        private readonly Assembly _repoAssemblieToScan;
        private readonly Assembly _domainAssemblieToScan;
        protected NolanApplicationModule(Type modelType , IConfiguration configuration, IServiceInfo serviceInfo, bool isDddDevelopment = false)
        {
            _appAssemblieToScan = modelType?.Assembly ?? throw new ArgumentNullException(nameof(modelType));
            _appContractsAssemblieToScan = Assembly.Load(_appAssemblieToScan.FullName.Replace(".Application", ".Application.Contracts"));
            if (isDddDevelopment)
                _domainAssemblieToScan = Assembly.Load(_appAssemblieToScan.FullName.Replace(".Application", ".Domain"));
            else
                _repoAssemblieToScan = Assembly.Load(_appAssemblieToScan.FullName.Replace(".Application", ".Repository"));

            //_appModuleName = serviceInfo.ShortName;
            //_redisSection = configuration.GetRedisSection();
        }
        protected override void Load(ContainerBuilder builder)
        {  //注册依赖模块ActivatingEventArgs
            this.LoadDepends(builder);
            builder.RegisterAssemblyTypes(_appAssemblieToScan)
                       .Where(t => t.IsAssignableTo<IAppService>() && !t.IsAbstract)
                       .AsImplementedInterfaces()
                       .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                       .InstancePerLifetimeScope();
                      // .EnableInterfaceInterceptors();
            // .InterceptedBy(interceptors.ToArray());

            //注册DtoValidators
            builder.RegisterAssemblyTypes(_appContractsAssemblieToScan)
                       .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                       .AsImplementedInterfaces()
                       .InstancePerLifetimeScope();
        }
        private void LoadDepends(ContainerBuilder builder)
        {
            
            //builder.RegisterModuleIfNotRegistered(new AdncInfraEventBusModule(_appAssemblieToScan));
             builder.RegisterModule(new AutoMapperModule(_appAssemblieToScan));
            //builder.RegisterModuleIfNotRegistered(new AdncInfraCachingModule(_redisSection));

            if (_domainAssemblieToScan != null)
            {
                var modelType = _domainAssemblieToScan.GetTypes().FirstOrDefault(x => x.IsAssignableTo<NolanDomainModule>() && !x.IsAbstract);
                builder.RegisterModule(System.Activator.CreateInstance(modelType) as Autofac.Module);
            }

            if (_repoAssemblieToScan != null)
            {
                var modelType = _repoAssemblieToScan.GetTypes().FirstOrDefault(x => x.IsAssignableTo<NolanRepositoryModule>() && !x.IsAbstract);
                builder.RegisterModule(System.Activator.CreateInstance(modelType) as Autofac.Module);
            }

        }
    }
}
