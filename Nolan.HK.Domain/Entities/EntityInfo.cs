using Nolan.Infra.Repository.Entities.EfEnities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.HK.Domain.Entities
{
    public class EntityInfo : AbstractEntityInfo
    {
        public override (Assembly Assembly, IEnumerable<Type> Types) GetEntitiesInfo()
        {
            var assembly = this.GetType().Assembly;
            var entityTypes = base.GetEntityTypes(assembly);

            return (assembly, entityTypes);
        }
    }
}
