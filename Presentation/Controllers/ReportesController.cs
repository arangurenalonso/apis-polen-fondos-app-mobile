namespace Presentation.Controllers
{
    using Application.Features.Reportes.Command.ExportarReporte1;
    using Application.Features.Reportes.ObtenerReporte1;
    using Application.Models.DTOResponse;
    using Application.Models.StoreProcedure.Response;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Presentation.Controllers.Common;
    using Presentation.Models;
    using System.Net;

    public class ReportesController : BaseApiController
    {
        public ReportesController(IMediator mediator) : base(mediator) { }


        [HttpGet("Reporte1")]
        [ProducesResponseType(typeof(List<Reporte1DTOResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseResult<List<Reporte1DTOResponse>>>> ObtenerReporte1([FromQuery]ObtenerReporte1Query query)
        {
            var result= await _mediator.Send(query);

            return new ResponseResult<List<Reporte1DTOResponse>>()
            {
                Data = result,
            };
        }
        [HttpGet("Reporte1/export")]
        [ProducesResponseType(typeof(FileStreamResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ExportarReporte1([FromQuery] ExportarReporte1Query query)
        {
            var stream = await _mediator.Send(query);
            return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") { FileDownloadName = "reporte.xlsx" };
        }
    }

}
