namespace Application.Features.Prospecto.Command.RegistrarProspecto
{
    using AutoMapper;
    using MediatR;
    using Application.Contracts.Repositories.Base;
    using Application.Contracts.Repositories;
    using Domain.Entities;
    using Domain.Enum;
    using System.Text.Json;

    public class RegistrarProspectoCommandHandler : IRequestHandler<RegistrarProspectoCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IProspectoRepository _prospectoRepository;
        public RegistrarProspectoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IProspectoRepository prospectoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _prospectoRepository = prospectoRepository;
        }


        public async Task<int> Handle(RegistrarProspectoCommand request, CancellationToken cancellationToken)
        {
            var mensaje = "Registro correcto";
            try
            {
                var fechaActual = DateTime.Now;
                var prospectoNuevo = _mapper.Map<Prospectos>(request);
                prospectoNuevo.EstId = (int)EstadoEnum.NoContactado;
                prospectoNuevo.ProFecest = fechaActual;
                prospectoNuevo.ProFecpro = fechaActual;
                prospectoNuevo.ProFecasi = fechaActual;
                prospectoNuevo.FecCap = fechaActual;
                prospectoNuevo.TipoPersona = 1;
                prospectoNuevo.Origin = "BULK_LOAD";
                prospectoNuevo.IsValidate = 0;
                _unitOfWork.Repository<Prospectos>().AddEntity(prospectoNuevo);

                var result = await _unitOfWork.Complete();

                if (result <= 0)
                {
                    throw new ApplicationException($"No se pudo insertar el record de Prospecto: {prospectoNuevo}");
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
    }
}
