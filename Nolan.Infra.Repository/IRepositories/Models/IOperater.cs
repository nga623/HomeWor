using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.Infra.Repository.IRepositories.Models
{
    public interface IOperater
    {
        string Id { get; set; }

        string Account { get; set; }

        string Name { get; set; }

        string CustomerId { get; set; }

    }
}
