namespace Presentation.Controllers
{
    using Application.Features.Estados.Query.ObtenerTodosEstados;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Presentation.Controllers.Common;
    using System.Net;

    public class EstadoController : BaseApiController
    {
        public EstadoController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> ObtenerTodos()
        {
            var query = new ObtenerTodosEstadosQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
