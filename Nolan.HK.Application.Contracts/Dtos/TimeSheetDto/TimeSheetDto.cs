using Nolan.HK.Domain.Enum;
using System;
using System.Collections.Generic;


namespace Nolan.HK.Application.Contracts.Dtos
{
    public class TimeSheetDto
    {
        public string ProjectID { get; set; }
        public Guid Id { get; set; }
        public int UserType { get; set; }
        public string UserName { get; set; }
        public string ProjectName { get; set; }
        public ApproveStatusEnum ApproveStatusEnum { get; set; }
        public List<TimeSheetDetailDto> ListTimeSheetDetails { get; set; }
    }
}
