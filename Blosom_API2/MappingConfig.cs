using AutoMapper;
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

           
        }
    }
}
