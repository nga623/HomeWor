using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.HK.Domain.Entities
{
	public class TimeSheet
	{
		public Guid Id { get; set; }
		public DateTime CreateTime { get; set; }
		public int TotalCount { get; set; }
		public DateTime ApproveTime { get; set; }
		public ApproveStatusEnum ApproveStatusEnum { get; set; }
		public string ApproveStatus { get; set; }
	}
	public enum ApproveStatusEnum
	{
		UnApprove = 0,
		Approve = 1
	}
}
