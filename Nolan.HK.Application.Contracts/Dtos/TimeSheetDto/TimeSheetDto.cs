using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.HK.Application.Contracts.Dtos
{
   public class TimeSheetDto
    {
		public string ProjectID { get; set; }
		public Guid Id { get; set; }
		public int UserType { get; set; }
		public string UserName { get; set; }
		public List<TimeSheetDetailDto> ListTimeSheetDetails { get; set; }
	}
}
