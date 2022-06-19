using Autofac;
using Nolan.Infra.EfCore.PostGresSql.Repositories;
using Nolan.Infra.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.Infra.EfCore.PostGresSql
{
     
    public class NolanInfraEfCoreModule : Module
    {
        /// <summary>
        /// Autofac注册
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            ////注册UOW状态类
            //builder.RegisterType<UnitOfWorkStatus>()
            //       .AsSelf()
            //       .InstancePerLifetimeScope();

            ////注册UOW
            //builder.RegisterType<UnitOfWork<AdncDbContext>>()
            //       .As<IUnitOfWork>()
            //       .InstancePerLifetimeScope();

            //注册ef公共EfRepository
            //builder.RegisterGeneric(typeof(EfRepository<>))
            //       .UsingConstructor(typeof(NolanDbContext))
            //       .AsImplementedInterfaces()
            //       .InstancePerLifetimeScope();

            //注册ef公共EfBasicRepository
            builder.RegisterGeneric(typeof(EfBasicRepository<>))
                   .UsingConstructor(typeof(NolanDbContext))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();




            //注册Repository服务
            builder.RegisterAssemblyTypes(this.ThisAssembly)
                  .Where(t => t.IsClosedTypeOf(typeof(IRepository<>)))
                  .AsImplementedInterfaces()
                  .InstancePerLifetimeScope();
        }

        /// <summary>
        /// Autofac注册,该方法供UnitTest工程使用
        /// </summary>
        /// <param name="builder"></param>
        public static void Register(ContainerBuilder builder)
        {
            new NolanInfraEfCoreModule().Load(builder);
        }
    }
}
