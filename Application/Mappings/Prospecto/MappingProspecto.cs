namespace Application.Mappings.Prospecto
{
    using Application.Features.Prospecto.Command.RegistrarMaestroProspecto;
    using Application.Features.Prospecto.Command.RegistrarProspecto;
    using Application.Features.Prospecto.Command.RegistrarRedesSociales;
    using Application.Helper;
    using Application.Models.ConsumoApi.Bitrix24.Entities;
    using AutoMapper;
    using Domain.Entities;
    using Domain.Enum;
    using Domain.Enum.Dictionario;
    public class MappingProspecto : Profile
    {
        public MappingProspecto()
        {
            CreateMap<RegistrarMaestroProspectoCommand, MaestroProspecto>()
                    .ForMember(dest => dest.DocId, origen => origen.MapFrom(src => src.TipoDoc != null ? (EnumDictionaryProvider.TipoDocumentoDict[src.TipoDoc.Value]) : null))
                    .ForMember(dest => dest.MaeNumdoc, origen => origen.MapFrom(src => src.NumDoc))
                    .ForMember(dest => dest.MaePat, origen => origen.MapFrom(src => src.ApePaterno))
                    .ForMember(dest => dest.MaeMat, origen => origen.MapFrom(src => src.ApeMaterno))
                    .ForMember(dest => dest.MaeNom, origen => origen.MapFrom(src => src.Nombres))
                    .ForMember(dest => dest.MaeRaz, origen => origen.MapFrom(src => src.RazonSocial))
                    .ForMember(dest => dest.MaeRel, origen => origen.MapFrom(src => $"{src.Nombres.Trim()} {src.ApePaterno.Trim()} {src.ApeMaterno.Trim()}"))
                    .ForMember(dest => dest.MaeDir, origen => origen.MapFrom(src => src.Direccion))
                    .ForMember(dest => dest.Genero, origen => origen.MapFrom(src => src.Genero != null? (EnumDictionaryProvider.GeneroEnumDict[src.Genero.Value]) :null))
                    .ForMember(dest => dest.Fnacimiento, origen => origen.MapFrom(src => src.Fnacimiento))
                    .ForMember(dest => dest.DepId, origen => origen.MapFrom(src => src.DepId))
                    .ForMember(dest => dest.ProId, origen => origen.MapFrom(src => src.ProId))
                    .ForMember(dest => dest.DisId, origen => origen.MapFrom(src => src.DisId))
                    .ForMember(dest => dest.MaeCel1, origen => origen.MapFrom(src => src.Cel1))
                    .ForMember(dest => dest.MaeCel2, origen => origen.MapFrom(src => src.Cel2))
                    .ForMember(dest => dest.MaeTelfijo, origen => origen.MapFrom(src => src.Telfijo))
                    .ForMember(dest => dest.MaeEmail, origen => origen.MapFrom(src => src.Email));

            CreateMap<RegistrarProspectoCommand, Prospectos>()
                    .ForMember(dest => dest.MaeId, origen => origen.MapFrom(src => src.MaestroProspectoId))
                    .ForMember(dest => dest.IntId, origen => origen.MapFrom(src => src.Interes))
                    .ForMember(dest => dest.NlinId, origen => origen.MapFrom(src => src.linea))
                    .ForMember(dest => dest.Corivta, origen => origen.MapFrom(src => src.OrigenVenta))
                    .ForMember(dest => dest.MedId, origen => origen.MapFrom(src => src.Medio))
                    .ForMember(dest => dest.ProCom, origen => origen.MapFrom(src => src.Comentario))
                    .ForMember(dest => dest.VenCod, origen => origen.MapFrom(src => src.CodVendedor))
                    .ForMember(dest => dest.VenSupcod, origen => origen.MapFrom(src => src.CodSupervisor))
                    .ForMember(dest => dest.VenGescod, origen => origen.MapFrom(src => src.CodGestor))
                    .ForMember(dest => dest.VenGercod, origen => origen.MapFrom(src => src.CodGerente))
                    .ForMember(dest => dest.ZonId, origen => origen.MapFrom(src => src.Zona));

            CreateMap<RegistrarProspectoRedesSocialesCommand, MaestroProspecto>()
                    .ForMember(dest => dest.MaeNom, origen => origen.MapFrom(src => src.Nombre))
                    .ForMember(dest => dest.MaePat, origen => origen.MapFrom(src => src.Apellido))
                    .ForMember(dest => dest.MaeCel1, origen => origen.MapFrom(src => MethodHelper.GetLastNCharacters(src.Telefono,10)))
                    .ForMember(dest => dest.MaeEmail, origen => origen.MapFrom(src => src.Email));

            CreateMap<RegistrarProspectoRedesSocialesCommand, Prospectos>()
                    .ForMember(dest => dest.ProCom, origen => origen.MapFrom(src => src.Anuncio))
                    .ForMember(dest => dest.ProFecpro, origen => origen.MapFrom(src => src.Fecha))
                    .ForMember(dest => dest.MedId, origen => origen.MapFrom(src => src.Plataforma== "fb" ?(int)OrigenVentaEnum.Facebook:(int)OrigenVentaEnum.TikTok));

        }
    }
}
