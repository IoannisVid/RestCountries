using AutoMapper;

namespace RestCountries.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Country, CountryDto>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.CountryName))
                .ForMember(dest => dest.Capital, opt => opt.MapFrom(src => src.CountryCapital))
                .ForMember(dest => dest.Borders, opt => opt.MapFrom(src => src.Borders.Select(b => b.BorderCode).ToList()));
        }
    }
}
