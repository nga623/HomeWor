using Nolan.HK.Domain.Enum;
using Nolan.Infra.Repository.Entities.EfEnities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.HK.Domain.Entities
{
	public class User : EfFullAuditEntity
	{
		
		public string Name { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public UserTypeEnum UserTypeEnum { get; set; }
		public string UserType { get; set; }
		public DateTime CreateTime { get; set; }
		public DateTime EditTime { get; set; }

	}
	
}
