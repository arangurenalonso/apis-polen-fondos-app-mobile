namespace Application.Mappings.Search
{
    using Application.Models.DTOResponse;
    using AutoMapper;
    using DocumentFormat.OpenXml.Presentation;
    using Domain.Entities;
    public class MappingSearch : Profile
    {
        public MappingSearch()
        {
            CreateMap<Vendedores, SearchViewModel>()
                    .ForMember(dest => dest.Id, origen => origen.MapFrom(src => src.VenCod))
                    .ForMember(dest => dest.Text, origen => origen.MapFrom(src => src.VenNom));
            CreateMap<StateEntity, SearchViewModel>()
                        .ForMember(dest => dest.Id, origen => origen.MapFrom(src => src.Id))
                        .ForMember(dest => dest.Text, origen => origen.MapFrom(src => src.Title));
            CreateMap<Medios, SearchViewModel>()
                        .ForMember(dest => dest.Id, origen => origen.MapFrom(src => src.MedId))
                        .ForMember(dest => dest.Text, origen => origen.MapFrom(src => src.MedDescription));
        }
    }
}
