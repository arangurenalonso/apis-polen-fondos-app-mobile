namespace Application.Features.Medios.Query.ObtenerTodosEstados
{
    using AutoMapper;
    using MediatR;
    using Application.Contracts.Repositories.Base;
    using Application.Contracts.Repositories;
    using Application.Models.DTOResponse;

    public class ObtenerTodosMediosQueryHandler : IRequestHandler<ObtenerTodosMediosQuery, List<SearchViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediosRepository _mediosRepository;

        public ObtenerTodosMediosQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediosRepository mediosRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediosRepository = mediosRepository;
        }


        public async Task<List<SearchViewModel>> Handle(ObtenerTodosMediosQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediosRepository.ObtenerTodos();
            return _mapper.Map<List<SearchViewModel>>(result);
        }
    }
}
