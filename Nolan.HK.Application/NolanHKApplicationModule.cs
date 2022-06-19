using Autofac;
using Microsoft.Extensions.Configuration;
using Nolan.Application.Shared;
using Nolan.WebApi.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.HK.Application
{
     
    public class NolanHKApplicationModule : NolanApplicationModule
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public NolanHKApplicationModule(IConfiguration configuration, IServiceInfo serviceInfo)
                    : base(typeof(NolanHKApplicationModule), configuration, serviceInfo, true)
        {
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="builder"><see cref="ContainerBuilder"/></param>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }
    }
}
