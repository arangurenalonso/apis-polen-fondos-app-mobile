namespace Application.Features.Reportes.ObtenerReporte1
{
    using AutoMapper;
    using MediatR;
    using Application.Contracts.Repositories.Base;
    using Application.Contracts.Repositories;
    using Application.Models.StoreProcedure.Request;
    using Application.Models.DTOResponse;

    public class ObtenerReporte1QueryHandler : IRequestHandler<ObtenerReporte1Query, List<Reporte1DTOResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IReportesRepository _reportesRepository;

        public ObtenerReporte1QueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IReportesRepository reportesRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _reportesRepository = reportesRepository;
        }


        public async Task<List<Reporte1DTOResponse>> Handle(ObtenerReporte1Query request, CancellationToken cancellationToken)
        {
            var spparameters = MapearSPParameters(request);
            var result = await _reportesRepository.ObtenerReporteControl1(spparameters);
            return _mapper.Map<List<Reporte1DTOResponse>>(result); 
        }

        private ReporteControl1SPRequest MapearSPParameters(ObtenerReporte1Query request)
        {
            DateTime fechaFin = DateTime.Now;
            DateTime fechaInicio = fechaFin.AddDays(-30);
            if (request.start_at!=null && request.end_at != null)
            {
                fechaInicio = request.start_at.Value;
                fechaFin = request.end_at.Value;

            }
            var spParameters = new ReporteControl1SPRequest()
            {
                start_at = fechaInicio,
                end_at = fechaFin,
                ven_gercod = request.ven_gercod == null ? "All": request.ven_gercod,
                ven_gescod = request.ven_gescod == null ? "All" : request.ven_gescod,
                ven_supcod = request.ven_supcod == null ? "All" : request.ven_supcod,
                ven_cod = request.ven_cod == null ? "All" : request.ven_cod,
                prioridad = request.prioridad == null ? "All" : request.prioridad,
                states = request.states == null ? "All" : request.states,
                medio = request.medio == null ? "All" : request.medio
            };
            return spParameters ;
        }
    }
}
