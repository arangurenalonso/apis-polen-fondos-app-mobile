namespace Application.Features.Estados.Query.ObtenerTodosEstados
{
    using AutoMapper;
    using MediatR;
    using Application.Contracts.Repositories.Base;
    using Application.Contracts.Repositories;
    using Application.Models.DTOResponse;

    public class ObtenerTodosEstadosQueryHandler : IRequestHandler<ObtenerTodosEstadosQuery, List<SearchViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStateRepository _stateRepository;

        public ObtenerTodosEstadosQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IStateRepository stateRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _stateRepository = stateRepository;
        }


        public async Task<List<SearchViewModel>> Handle(ObtenerTodosEstadosQuery request, CancellationToken cancellationToken)
        {
            var result = await _stateRepository.ObtenerTodos();
            return _mapper.Map<List<SearchViewModel>>(result);
        }
    }
}
