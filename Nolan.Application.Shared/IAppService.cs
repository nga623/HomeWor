using Nolan.Infra.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.Application.Shared
{
    public interface IAppService
    {
        IObjectMapper Mapper { get; set; }
    }
}
