using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.HK.Application.Contracts.Dtos 
{
   public class TimeSheetDetailDto
    {
		public Guid Userid { get; set; }
		[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
		public DateTime Date { get; set; }
		public string Weekday { get; set; }
		public int TimesheetCount { get; set; }
		public string Note { get; set; }
		public Guid ProjectID { get; set; }
		public Guid TimesheetID { get; set; }
	}
}
