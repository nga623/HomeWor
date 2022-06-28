using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.WebApi.Shared.Log
{
    public interface INLogHelper
    {
        void LogError(Exception ex);
    }
}
