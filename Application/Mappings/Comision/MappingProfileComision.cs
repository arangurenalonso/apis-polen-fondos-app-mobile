namespace Application.Mappings.Comision
{
    using Application.Mappings.Comision.DTO;
    using AutoMapper;
    using Domain.Entities;

    public class MappingProfileComision : Profile
    {
        public MappingProfileComision()
        {
            CreateMap<InsertarComisionesParaLiquidacionMasivoDTORequest, ComisionesParaLiquidacion>();
        }
    }
}
