namespace Application.Features.Prospecto.Command.RegistrarMaestroProspecto
{
    using AutoMapper;
    using MediatR;
    using Application.Contracts.Repositories.Base;
    using Application.Contracts.Repositories;
    using Domain.Entities;
    using System.Drawing;
    using System.Text.Json;

    public class RegistrarMaestroProspectoCommandHandler : IRequestHandler<RegistrarMaestroProspectoCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMaestroProspectoRepository _maestroProspectoRepository;
        public RegistrarMaestroProspectoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,
            IMaestroProspectoRepository maestroProspectoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _maestroProspectoRepository = maestroProspectoRepository;

        }


        public async Task<int> Handle(RegistrarMaestroProspectoCommand request, CancellationToken cancellationToken)
        {
            var mensaje = "Registro correcto";
            try
            {
                var prospectoMaestroNuevo = _mapper.Map<MaestroProspecto>(request);
                prospectoMaestroNuevo.MaeFeccrea = DateTime.Now;
                prospectoMaestroNuevo.MaeFecactu = DateTime.Now;
                _unitOfWork.Repository<MaestroProspecto>().AddEntity(prospectoMaestroNuevo);

                var result = await _unitOfWork.Complete();

                if (result <= 0)
                {
                    throw new ApplicationException($"No se pudo insertar el record de MaestroProspecto: {prospectoMaestroNuevo}");
                }
                return prospectoMaestroNuevo.MaeId;

            }
            catch (System.Exception e)
            {
                mensaje=e.Message;
                throw;
            }
            finally
            {
                var log = new LogFondos()
                {
                    Fecha = DateTime.Now,
                    Mensaje = mensaje,
                    Tipo="Polen",
                    Valor=JsonSerializer.Serialize(request)
                };
                await _unitOfWork.Repository<LogFondos>().AddAsync(log);
            }
        }
    }
}
