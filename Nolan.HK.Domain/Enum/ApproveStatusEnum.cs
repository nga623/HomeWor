using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.HK.Domain.Enum
{
	public enum ApproveStatusEnum
	{
		[Description("UnApprove")]
		UnApprove = 0,
		[Description("Approve")]
		Approve = 1
	}
}
