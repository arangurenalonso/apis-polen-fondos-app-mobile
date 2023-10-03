namespace Presentation.Controllers
{
    using Application.Features.Prospecto.Command.RegistrarRedesSociales;
    using Application.Features.Vendedor.Command.DarDeBajaVendedor;
    using DocumentFormat.OpenXml.Wordprocessing;
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

            var vendedor = await _mediator.Send(command);
            return Ok($"Cese Correcto");
        }
    }
}
