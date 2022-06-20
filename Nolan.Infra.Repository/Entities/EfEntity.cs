using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.Infra.Repository.Entities
{
    public abstract class EfEntity : Entity, IEfEntity<Guid>
    {
    }
}
