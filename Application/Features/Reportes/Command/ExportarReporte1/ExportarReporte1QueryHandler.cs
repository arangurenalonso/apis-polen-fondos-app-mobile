namespace Application.Features.Reportes.Command.ExportarReporte1
{
    using AutoMapper;
    using MediatR;
    using Application.Contracts.Repositories.Base;
    using Application.Contracts.Repositories;
    using Application.Models.StoreProcedure.Request;
    using Application.Models.DTOResponse;
    using System.Text;
    using Application.Models.StoreProcedure.Response;
    using ClosedXML.Excel;

    public class ExportarReporte1QueryHandler : IRequestHandler<ExportarReporte1Query, Stream>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IReportesRepository _reportesRepository;

        public ExportarReporte1QueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IReportesRepository reportesRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _reportesRepository = reportesRepository;
        }


        public async Task<Stream> Handle(ExportarReporte1Query request, CancellationToken cancellationToken)
        {
            var spparameters = MapearSPParameters(request);
            var result = await _reportesRepository.ObtenerReporteControl1(spparameters);
            return ObtenerExcel(result);
        }
        private Stream ObtenerCSV(List<ReporteControl1SPResponse> result)
        {
            var csvBuilder = new StringBuilder();
            // Encabezados
            csvBuilder.AppendLine("Vendedor,Supervisor,Fecha de Captación,Fecha de registro de prospecto o de derivación,Fecha de inscripción,Origen de la venta,Medio,Nombre de cliente,E mail,Telefono,Zona,Distrito,Producto,Valor del certificado,Dias desde la asignación,Dias desde la última gestión,Descartado?,Motivo del Descarte,Prospecto con prioridad?,Estado actual prospecto,Comentarios");

            // Datos
            foreach (var item in result)
            {
                csvBuilder.AppendLine($"{item.Vendedor},{item.Supervisor},{item.Fecha_Capt},{item.Fecha_Registro},{item.FechaInscripcion},{item.Origen_Venta},{item.Medio},{item.Prospecto},{item.Correo},{item.Celular},{item.Zona},{item.Distrito},{item.Producto},{item.Certificado},{item.Dias},{item.DiasCaptacion},{item.Descartado},{item.MotivoDescarte},{item.Prioridad},{item.Estado_Actual},{item.Comentario}");
            }

            var buffer = Encoding.UTF8.GetBytes(csvBuilder.ToString());
            var stream = new MemoryStream(buffer);
            return stream;

        }
        private Stream ObtenerExcel(List<ReporteControl1SPResponse> result)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Reporte1");

                // Encabezados
                var headers = new string[]
                    {
                        "Vendedor", "Supervisor", "Fecha de Captación", "Fecha de registro de prospecto o de derivación",
                        "Fecha de inscripción", "Origen de la venta", "Medio", "Nombre de cliente", "E mail", "Telefono", "Zona", "Distrito",
                        "Producto", "Valor del certificado", "Dias desde la asignación", "Dias desde la última gestión", "Descartado?",
                        "Motivo del Descarte", "Prospecto con prioridad?", "Estado actual prospecto", "Comentarios"
                    };

                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cell(1, i + 1).Value = headers[i];
                }

                // Datos
                int row = 2;
                foreach (var item in result)
                {
                    worksheet.Cell(row, 1).Value = item.Vendedor;
                    worksheet.Cell(row, 2).Value = item.Supervisor;
                    worksheet.Cell(row, 3).Value = item.Fecha_Capt;
                    worksheet.Cell(row, 4).Value = item.Fecha_Registro;
                    worksheet.Cell(row, 5).Value = item.FechaInscripcion;
                    worksheet.Cell(row, 6).Value = item.Origen_Venta;
                    worksheet.Cell(row, 7).Value = item.Medio;
                    worksheet.Cell(row, 8).Value = item.Prospecto;
                    worksheet.Cell(row, 9).Value = item.Correo;
                    worksheet.Cell(row, 10).Value = item.Celular;
                    worksheet.Cell(row, 11).Value = item.Zona;
                    worksheet.Cell(row, 12).Value = item.Distrito;
                    worksheet.Cell(row, 13).Value = item.Producto;
                    worksheet.Cell(row, 14).Value = item.Certificado;
                    worksheet.Cell(row, 15).Value = item.Dias;
                    worksheet.Cell(row, 16).Value = item.DiasCaptacion;
                    worksheet.Cell(row, 17).Value = item.Descartado;
                    worksheet.Cell(row, 18).Value = item.MotivoDescarte;
                    worksheet.Cell(row, 19).Value = item.Prioridad;
                    worksheet.Cell(row, 20).Value = item.Estado_Actual;
                    worksheet.Cell(row, 21).Value = item.Comentario;
                    row++;
                }

                // Guardar a Stream
                var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Position = 0; 
                return stream;
            }
        }
        private ReporteControl1SPRequest MapearSPParameters(ExportarReporte1Query request)
        {
            DateTime fechaFin = DateTime.Now;
            DateTime fechaInicio = fechaFin.AddDays(-30);
            if (request.start_at != null && request.end_at != null)
            {
                fechaInicio = request.start_at.Value;
                fechaFin = request.end_at.Value;

            }
            var spParameters = new ReporteControl1SPRequest()
            {
                start_at = fechaInicio,
                end_at = fechaFin,
                ven_gercod = request.ven_gercod == null ? "All" : request.ven_gercod,
                ven_gescod = request.ven_gescod == null ? "All" : request.ven_gescod,
                ven_supcod = request.ven_supcod == null ? "All" : request.ven_supcod,
                ven_cod = request.ven_cod == null ? "All" : request.ven_cod,
                prioridad = request.prioridad == null ? "All" : request.prioridad,
                states = request.states == null ? "All" : request.states,
                medio = request.medio == null ? "All" : request.medio
            };
            return spParameters;
        }
    }
}
