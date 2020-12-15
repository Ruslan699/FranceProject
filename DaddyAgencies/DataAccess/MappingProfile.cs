using System;
using AutoMapper;
using DataAccess.Context;
using Models = DataModel.Models;

namespace DataAccess
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegionEntity, Models.Region>()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}
