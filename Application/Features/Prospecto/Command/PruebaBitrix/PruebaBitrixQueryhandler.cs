namespace Application.Features.Prospecto.Command.PruebaBitrix
{
    using Application.Contracts.Repositories.Base;
    using Domain.Entities;
    using MediatR;
    using System.Text.Json;

    public class PruebaBitrixQueryhandler : IRequestHandler<PruebaBitrixQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public PruebaBitrixQueryhandler(IUnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<bool> Handle(PruebaBitrixQuery request, CancellationToken cancellationToken)
        {

            var mensaje = $"Prueba de insersion id enviado: {request.Id}";
            var log = new LogFondos()
            {
                Fecha = DateTime.Now,
                Mensaje = mensaje,
                Tipo = "Polen",
                Valor = JsonSerializer.Serialize(request)
            };
            await _unitOfWork.Repository<LogFondos>().AddAsync(log);
            return true;

        }
    }
}
