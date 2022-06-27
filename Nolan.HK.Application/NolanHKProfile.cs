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
            //CreateMap<CustomerclueRecycle, CustomerclueRecycleDto>()
            //.ForMember(
            //  d => d.HighSeasName, opt =>
            //  {
            //      opt.MapFrom(s => s.HighSeas.HighSeasName);
            //  });
            CreateMap<TimeSheetDetailCreateDto, TimeSheetDetail>().ReverseMap();
            CreateMap<TimeSheetDetail, TimeSheetDetailDto>().ReverseMap();
            CreateMap<TimeSheetCreateDto, TimeSheet>().ReverseMap();
           
            CreateMap<TimeSheetDetail, TimeSheetDetailDto>()
                .ForMember(
              d => d.UserName, opt =>
              {
                  opt.MapFrom(s => s.User.Name);
              }); 
            CreateMap<TimeSheet, TimeSheetDto>().ForMember(
              d => d.UserName, opt =>
              {
                  opt.MapFrom(s => s.User.Name);
              });
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
