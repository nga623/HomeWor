using Autofac;
using Nolan.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.HK.Domain
{
     
    public sealed class NolanHKDomainModule : NolanDomainModule
    {
        public NolanHKDomainModule() : base(typeof(NolanHKDomainModule))
        {
        }
        /// <summary>
        /// Autofac注册
        /// </summary>
        /// <param name="builder"><see cref="ContainerBuilder"/></param>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }
    }
}
