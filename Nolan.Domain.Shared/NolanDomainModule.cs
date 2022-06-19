using Autofac;
using Nolan.Infra.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.Domain.Shared
{
    
    /// <summary>
    /// Autofac注册
    /// </summary>
    public abstract class NolanDomainModule : Autofac.Module
    {
        private readonly Assembly _assemblieToScan;

        protected NolanDomainModule(Type modelType)
        {
            _assemblieToScan = modelType.Assembly;
        }

        protected override void Load(ContainerBuilder builder)
        {
            //注册EntityInfo
            builder.RegisterAssemblyTypes(_assemblieToScan)
                   .Where(t => t.IsAssignableTo<IEntityInfo>() && !t.IsAbstract)
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            //注册服务
            builder.RegisterAssemblyTypes(_assemblieToScan)
                   .Where(t => t.IsAssignableTo<IDomainService>())
                   .AsSelf()
                   .InstancePerLifetimeScope();
        }
    }
}
