using AutoMapper;
using Blosom_API2.Models;
using Blosom_API2.Models.Dto;
using Blossom_API.Models;
using Blossom_API.Models.Dto;

namespace Blosom_API2
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Blossom, BlossomDto>();
            CreateMap<BlossomDto, Blossom>();

            CreateMap<Blossom, BlossomCreateDto>().ReverseMap();
            CreateMap<Blossom, BlossomUpdateDto>().ReverseMap();

            CreateMap<NumberBlossom, NumberBlossomDto>().ReverseMap();
            CreateMap<NumberBlossom, NumberBlossomCreateDto>().ReverseMap();
            CreateMap<NumberBlossom, NumberBlossomUpdateDto>().ReverseMap();
        }
    }
}
