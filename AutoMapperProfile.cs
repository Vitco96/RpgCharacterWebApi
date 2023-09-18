using AutoMapper;
using Tutorial_proj.Models;

namespace Tutorial_proj
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
        }
    }
}
