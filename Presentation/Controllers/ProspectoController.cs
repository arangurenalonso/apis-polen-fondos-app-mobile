namespace Presentation.Controllers
{
    using Application.Features.Prospecto.Command.RegistrarMaestroProspecto;
    using Application.Features.Prospecto.Command.RegistrarProspecto;
    using Application.Features.Prospecto.Command.RegistrarRedesSociales;
    using Application.Features.Prospecto.Query.ObtenerDatosFoja;
    using Application.Features.Prospecto.Query.ObtenerDatosProspectoPorVendedor;
    using Application.Mappings.Prospecto.DTO;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Presentation.Controllers.Common;
    using Presentation.Filtros;
    using Presentation.Models;
    using System.Net;
    public class ProspectoController : BaseApiController
    {
        public ProspectoController(IMediator mediator) : base(mediator) { }

        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        [HttpGet("Vendedor/{CodVendedor}/Documento/{nroDocumento}", Name = "ObtenerDatosProspectoPorVendedor")]
        [ProducesResponseType(typeof(SPGetDatosProspectoVendResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SPGetDatosProspectoVendResponse>> ObtenerDatosProspectoPorVendedor(string CodVendedor,
        string nroDocumento)
        {
            var query = new ObtenerDatosProspectoPorVendedorQuery(CodVendedor, nroDocumento);
            var grupos = await _mediator.Send(query);
            return Ok(grupos);
        }

        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        [HttpGet("Foja/{ProspectoId}", Name = "ObtenerProspectoDatosFoja")]
        [ProducesResponseType(typeof(SpGetListaDatosFojaResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SpGetListaDatosFojaResult>> ObtenerProspectoDatosFoja(int ProspectoId)
        {
            var query = new ObtenerDatosFojaProspectoQuery(ProspectoId);
            var fojaDataList = await _mediator.Send(query);
            return Ok(fojaDataList);
        }
        [HttpGet("RegistrarMaestroProspecto", Name = "RegistrarMaestroProspecto")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> RegistrarMaestroProspecto([FromQuery] RegistrarMaestroProspectoCommand command)
        {
            var maestroProspectoId = await _mediator.Send(command);
            return Ok(maestroProspectoId);
        }
        [HttpGet("RegistrarProspecto", Name = "RegistrarProspecto")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> RegistrarProspecto([FromQuery] RegistrarProspectoCommand command)
        {
            var ProspectoId = await _mediator.Send(command);
            return Ok(ProspectoId);
        }

        [HttpPost("RedesSociales", Name = "RegistrarProspectoRedesSociales")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> RegistrarProspectoRedesSociales([FromBody] RegistrarProspectoRedesSocialesCommand command)
        {
            var prospecto = await _mediator.Send(command);
            return Ok(prospecto);
        }
    }
}