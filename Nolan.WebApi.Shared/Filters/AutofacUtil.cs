using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.WebApi.Shared.Filters
{
    public class AutofacUtil
    {
        public static ILifetimeScope Container { get; set; }

        public static T GetService<T>() where T : class
        {
            return Container.Resolve<T>();
        }
    }
}
