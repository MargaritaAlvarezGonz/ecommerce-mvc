using AutoMapper;
using Blossom_Web.Models.Dto;

namespace Blossom_Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<BlossomDto,BlossomCreateDto>().ReverseMap();
            CreateMap<BlossomDto,BlossomUpdateDto>().ReverseMap();

            CreateMap<NumberBlossomDto,NumberBlossomCreateDto>().ReverseMap();
            CreateMap<NumberBlossomDto, NumberBlossomUpdateDto>().ReverseMap();
        }
    }
}
