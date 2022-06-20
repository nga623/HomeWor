using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.Infra.EfCore.PostGresSql
{
    public class UnitOfWorkStatus
    {
        public bool IsStartingUow { get; internal set; }
    }
}
