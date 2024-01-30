namespace Presentation.Controllers
{
    using Application.Features.Vendedor.Command.CrearVendedor;
    using Application.Features.Vendedor.Command.DarDeBajaVendedor;
    using Application.Features.Vendedor.Query.ObtenerGerentes;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Presentation.Controllers.Common;
    using System.Net;

    public class VendedorController : BaseApiController
    {
        public VendedorController(IMediator mediator) : base(mediator) { }
        [HttpPost("BajaVendedor")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> DarDeBajaVendedor([FromBody] DarDeBajaVendedorCommand command)
        {

            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> Crear([FromBody] CrearVendedorCommand command)
        {

            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("Gerente")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> ObtenerGerente()
        {
            var query = new ObtenerGerentesQuery("005");
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        [HttpGet("Gerente/{idGerente}/Gestores")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> ObtenerGestores([FromRoute]string idGerente)
        {
            var query = new ObtenerGerentesQuery("001", idGerente);
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        [HttpGet("Gestor/{idGestor}/Supervisores")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> ObtenerSupervisores(string idGestor)
        {
            var query = new ObtenerGerentesQuery("002", null, idGestor);
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        [HttpGet("Supervisor/{idVendedor}/Vendedores")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> ObtenerVendedores(string idVendedor)
        {
            var query = new ObtenerGerentesQuery("003", null, null, idVendedor);
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}

