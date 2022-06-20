using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.Infra.Repository.IRepositories.Models
{
    public class Operater : IOperater
    {
        public string Id { get; set; }

        public string Account { get; set; }

        public string Name { get; set; }
        public string CustomerId { get; set; }
    }
}
