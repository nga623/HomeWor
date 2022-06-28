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
            CreateMap<TimeSheetDetail, TimeSheetDetailDto>().ReverseMap();
            CreateMap<TimeSheetCreateDto, TimeSheet>().ReverseMap();

            CreateMap<TimeSheetDetail, TimeSheetDetailDto>()
                .ForMember(
              d => d.UserName, opt =>
              {
                  opt.MapFrom(s => s.User.Name);
              });
            CreateMap<TimeSheet, TimeSheetDto>()
               .ForMember(
              d => d.UserName, opt =>
              {
                  opt.MapFrom(s => s.User.Name);
              })
               .ForMember(
              d => d.ProjectName, opt =>
              {
                  opt.MapFrom(s => s.Project.ProjectName);
              })
                .ForMember(
              d => d.UserType, opt =>
              {
                  opt.MapFrom(s =>(int) s.ApproveStatusEnum);
              })
                ;
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
