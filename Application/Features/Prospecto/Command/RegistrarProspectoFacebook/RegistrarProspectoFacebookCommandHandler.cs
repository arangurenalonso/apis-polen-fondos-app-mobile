
using Application.Contracts.Repositories.Base;
using Application.Contracts.Repositories;
using Application.Features.Prospecto.Command.RegistrarProspecto;
using AutoMapper;
using Domain.Entities;
using Domain.Enum;
using MediatR;
using System.Text.Json;
using System.Globalization;

namespace Application.Features.Prospecto.Command.RegistrarProspectoFacebook
{
    public class RegistrarProspectoFacebookCommandHandler : IRequestHandler<RegistrarProspectoFacebookCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IProspectoRepository _prospectoRepository;
        public RegistrarProspectoFacebookCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IProspectoRepository prospectoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _prospectoRepository = prospectoRepository;
        }


        public async Task<int> Handle(RegistrarProspectoFacebookCommand request, CancellationToken cancellationToken)
        {
            var fechaActual = DateTime.Now;
            var mensaje = "Registro correcto";
            try
            {
                var prospectoMaestroNuevo = _mapper.Map<MaestroProspecto>(request);
                prospectoMaestroNuevo.MaeFeccrea = fechaActual;
                prospectoMaestroNuevo.MaeFecactu = fechaActual;
                _unitOfWork.Repository<MaestroProspecto>().AddEntity(prospectoMaestroNuevo);

                await _unitOfWork.Complete();

                var prospectoNuevo = _mapper.Map<Prospectos>(request);
                prospectoNuevo.MaeId = prospectoMaestroNuevo.MaeId;
                prospectoNuevo.EstId = (int)EstadoEnum.NoContactado;
                prospectoNuevo.ProFecest = fechaActual;
                prospectoNuevo.ProFecasi = fechaActual;
                prospectoNuevo.FecCap = fechaActual;
                prospectoNuevo.ProFecpro = request.Fecha;
                //prospectoNuevo.ProFecpro = ParsearFecha(request.Fecha);
                prospectoNuevo.TipoPersona = 1;
                prospectoNuevo.Origin = "BULK_LOAD";
                prospectoNuevo.IsValidate = 0;
                _unitOfWork.Repository<Prospectos>().AddEntity(prospectoNuevo);

                var result = await _unitOfWork.Complete();

                if (result <= 0)
                {
                    throw new ApplicationException($"No se pudo insertar el registro");
                }
                return prospectoNuevo.ProId;

            }
            catch (System.Exception e)
            {
                mensaje = e.Message;

                throw;
            }

            finally
            {
                var log = new LogFondos()
                {
                    Fecha = DateTime.Now,
                    Mensaje = mensaje,
                    Tipo = "Polen",
                    Valor = JsonSerializer.Serialize(request)
                };
                await _unitOfWork.Repository<LogFondos>().AddAsync(log);
            }
        }
        private DateTime ParsearFecha(string fecha)
        {
            DateTime fechaConvertida;

            if (DateTime.TryParseExact(fecha, "M/d/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaConvertida))
            {
                return fechaConvertida;
            }
            else
            {
                throw new FormatException("Formato de fecha incorrecto.");
            }

        }
    }

}
