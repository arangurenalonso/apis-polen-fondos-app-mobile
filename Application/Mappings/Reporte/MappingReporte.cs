namespace Application.Mappings.Reporte
{
    using Application.Features.Reportes.ObtenerReporte1;
    using Application.Models.DTOResponse;
    using Application.Models.StoreProcedure.Request;
    using Application.Models.StoreProcedure.Response;
    using AutoMapper;

    public class MappingReporte : Profile
    {
        public MappingReporte()
        {
            CreateMap<ReporteControl1SPResponse, Reporte1DTOResponse>()
                .ForMember(dest => dest.Seller, origen => origen.MapFrom(src => src.Vendedor))
                .ForMember(dest => dest.Record_date, origen => origen.MapFrom(src => src.Fecha_Registro))
                .ForMember(dest => dest.Name, origen => origen.MapFrom(src => src.Prospecto))
                .ForMember(dest => dest.Status, origen => origen.MapFrom(src => src.Estado_Actual))
                .ForMember(dest => dest.Management_days, origen => origen.MapFrom(src => src.Dias))
                .ForMember(dest => dest.Collection_date, origen => origen.MapFrom(src => src.Fecha_Capt))
                .ForMember(dest => dest.Assignment_days, origen => origen.MapFrom(src => src.DiasCaptacion))
                .ForMember(dest => dest.Mail, origen => origen.MapFrom(src => src.Correo))
                .ForMember(dest => dest.Zone, origen => origen.MapFrom(src => src.Zona))
                .ForMember(dest => dest.District, origen => origen.MapFrom(src => src.Distrito))
                .ForMember(dest => dest.Product, origen => origen.MapFrom(src => src.Producto))
                .ForMember(dest => dest.Certificate_value, origen => origen.MapFrom(src => src.Certificado))
                .ForMember(dest => dest.Sales_origin, origen => origen.MapFrom(src => src.Origen_Venta))
                .ForMember(dest => dest.Half, origen => origen.MapFrom(src => src.Medio))
                .ForMember(dest => dest.Discarded, origen => origen.MapFrom(src => src.Descartado))
                .ForMember(dest => dest.Priority_prospect, origen => origen.MapFrom(src => src.Prioridad))
                .ForMember(dest => dest.Comments, origen => origen.MapFrom(src => src.Comentario))
                .ForMember(dest => dest.Celular, origen => origen.MapFrom(src => src.Celular))
                .ForMember(dest => dest.Supervisor, origen => origen.MapFrom(src => src.Supervisor))
                .ForMember(dest => dest.Motivodes, origen => origen.MapFrom(src => src.MotivoDescarte))
                .ForMember(dest => dest.Inscripcion, origen => origen.MapFrom(src => src.FechaInscripcion));

        }
    }
}
