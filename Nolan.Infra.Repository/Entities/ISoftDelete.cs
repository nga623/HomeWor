using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.Infra.Repository.Entities
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }

        DateTime? DeleteTime { get; set; }

        string DeletedBy { get; set; }
    }
}
