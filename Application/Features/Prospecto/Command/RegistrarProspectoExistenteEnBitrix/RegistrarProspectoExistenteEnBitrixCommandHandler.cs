namespace Application.Features.Prospecto.Command.RegistrarProspectoExistenteEnBitrix
{
    using AutoMapper;
    using MediatR;
    using Application.Contracts.Repositories.Base;
    using Domain.Entities;
    using System.Text.Json;
    using Application.Contracts.ApiExterna;

    public class RegistrarProspectoExistenteEnBitrixCommandHandler : IRequestHandler<RegistrarProspectoExistenteEnBitrixCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBitrix24ApiService _bitrix24ApiService;
        public RegistrarProspectoExistenteEnBitrixCommandHandler(
            IUnitOfWork unitOfWork, 
            IMapper mapper,
            IBitrix24ApiService bitrix24ApiService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _bitrix24ApiService = bitrix24ApiService;
        }


        public async Task<int> Handle(RegistrarProspectoExistenteEnBitrixCommand request, CancellationToken cancellationToken)
        {
            
            try
            {

                var deal=await _bitrix24ApiService.ValidarExistenciaDealEnBitrix(request.ProspectoId);
                return request.ProspectoId;
            }
            catch (System.Exception e)
            {

                var mensaje = $"Error ocurrido en RegistrarProspectoExistenteEnBitrix --- {e.Message}";
                var log = new LogFondos()
                {
                    Fecha = DateTime.Now,
                    Mensaje = mensaje,
                    Tipo = "Polen",
                    Valor = JsonSerializer.Serialize(request)
                };
                await _unitOfWork.Repository<LogFondos>().AddAsync(log);
                await Task.Delay(TimeSpan.FromSeconds(10));
                throw;
            }
        }
    }
}
