
using Application.Contracts.Repositories.Base;
using Application.Contracts.Repositories;
using Application.Features.Prospecto.Command.RegistrarProspecto;
using AutoMapper;
using Domain.Entities;
using Domain.Enum;
using MediatR;
using System.Text.Json;
using System.Globalization;
using Application.Models.ConsumoApi.Bitrix24.Models;
using Application.Contracts.ApiExterna;
using Application.Models.ConsumoApi.Bitrix24.Entities;

namespace Application.Features.Prospecto.Command.RegistrarRedesSociales
{
    public class RegistrarProspectoRedesSocialesCommandHandler : IRequestHandler<RegistrarProspectoRedesSocialesCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBitrix24ApiService _bitrix24ApiService;
        private DateTime fechaActual=DateTime.Now;
        public RegistrarProspectoRedesSocialesCommandHandler(IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IBitrix24ApiService bitrix24ApiService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _bitrix24ApiService = bitrix24ApiService;
        }


        public async Task<int> Handle(RegistrarProspectoRedesSocialesCommand request, CancellationToken cancellationToken)
        {

            var idContactBitrix24 = await RegistrarContactoBitrix24(MapearContactBitrix(request));
            var idMaestroProspecto = await RegistrarMaestroProspecto(_mapper.Map<MaestroProspecto>(request), idContactBitrix24);
            var idProspecto = await RegistrarProspecto(_mapper.Map<Prospectos>(request), idMaestroProspecto);

            return idProspecto;

        }
        private ContactBitrix24 MapearContactBitrix(RegistrarProspectoRedesSocialesCommand request)
        {
            var obj = new ContactBitrix24
            {
                NAME=request.Nombre,
                LAST_NAME=request.Apellido
            };
            if (request.Telefono != null)
            {
                var phone = new TypedField
                {
                    VALUE = request.Telefono,
                    VALUE_TYPE = "PHONE"
                };
                obj.PHONE.Add(phone);
            }
            if (request.Email != null)
            {
                var email = new TypedField
                {
                    VALUE = request.Email,
                    VALUE_TYPE = "EMAIL"
                };
                obj.EMAIL.Add(email);
            }

            return obj;
        }
        private async Task<int> RegistrarMaestroProspecto(MaestroProspecto maestroProspecto, string idContactBitrix24)
        {
            maestroProspecto.MaeFeccrea = fechaActual;
            maestroProspecto.MaeFecactu = fechaActual;
            maestroProspecto.BitrixID = idContactBitrix24;
            var entityAdded=await _unitOfWork.Repository<MaestroProspecto>().AddAsync(maestroProspecto);
            return entityAdded.MaeId;
        }
        private async Task<int> RegistrarProspecto(Prospectos prospecto, int idMaestroProspecto)
        {
            prospecto.MaeId = idMaestroProspecto;
            prospecto.EstId = (int)EstadoEnum.NoContactado;
            prospecto.ProFecest = fechaActual;
            prospecto.ProFecasi = fechaActual;
            prospecto.FecCap = fechaActual;
            prospecto.TipoPersona = 1;
            prospecto.Origin = "BULK_LOAD";
            prospecto.IsValidate = 0;
            var entityAdded = await _unitOfWork.Repository<Prospectos>().AddAsync(prospecto);
            return entityAdded.ProId;
        }
        private async Task<string> RegistrarContactoBitrix24(ContactBitrix24 contactBitrix24)
        {
            var request = new ApiRequestBitrixCreate<ContactBitrix24>()
            {
                fields = contactBitrix24,
                Params = new ParamsApiRequestCreate()
                {
                    REGISTER_SONET_EVENT = "Y"
                }

            };

            var resultadoPost = await _bitrix24ApiService.CRMContactAdd(request);
            if (!resultadoPost.IsSuccess)
            {
                throw new ApplicationException(resultadoPost.Message);
            }
            return resultadoPost.Result.Result.ToString();
        }
        private async Task<string> RegistrarDealBitrix24(DealBitrix24 dealBitrix24)
        {
            var request = new ApiRequestBitrixCreate<DealBitrix24>()
            {
                fields = dealBitrix24,
                Params = new ParamsApiRequestCreate()
                {
                    REGISTER_SONET_EVENT = "Y"
                }

            };

            var resultadoPost = await _bitrix24ApiService.CRMDealAdd(request);
            if (!resultadoPost.IsSuccess)
            {
                throw new ApplicationException(resultadoPost.Message);
            }
            return resultadoPost.Result.Result.ToString();
        }

    }

}
