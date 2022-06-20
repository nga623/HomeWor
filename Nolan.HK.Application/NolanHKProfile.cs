using AutoMapper;
using Nolan.HK.Application.Contracts.Dtos;
using Nolan.HK.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.HK.Application
{

    
    public class NolanHKProfile : Profile
    {
        public NolanHKProfile()
        {
            CreateMap<TimeSheetDetailCreateDto, TimeSheetDetail>().ReverseMap();
        }
    }
}
