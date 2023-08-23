namespace Application.Behaviors
{
    using Application.Contracts.Repositories.Base;
    using Domain.Entities;
    using MediatR;
    using System.Text.Json;

    public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
       where TRequest : IRequest<TResponse>
    {

        private readonly IUnitOfWork _unitOfWork;

        public UnhandledExceptionBehavior(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (System.Exception ex)
            {
                var log = new LogFondos()
                {
                    Fecha = DateTime.Now,
                    Mensaje = ex.Message,
                    Tipo = "Polen",
                    Valor = JsonSerializer.Serialize(request)
                };
                await _unitOfWork.Repository<LogFondos>().AddAsync(log);

                var requestName = typeof(TRequest).Name;
                throw;

            }
        }
    }
}
