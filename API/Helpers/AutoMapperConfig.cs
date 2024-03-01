using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<AppUser, MemberDto>()
            .ForMember(mem => mem.Age,opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()))
            .ForMember(mem => mem.PhotoUrl ,opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<Photo, PhotoDto>();
            CreateMap<MemberDto, AppUser>();
            CreateMap<PhotoDto, Photo>();
        }
    }
}