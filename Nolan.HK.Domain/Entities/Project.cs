using Nolan.Infra.Repository.Entities.EfEnities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.HK.Domain.Entities
{
	public class Project : EfFullAuditEntity
	{
		 
		public string ProjectName { get; set; }
	}
}
