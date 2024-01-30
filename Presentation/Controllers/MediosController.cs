namespace Presentation.Controllers
{
    using Application.Features.Medios.Query.ObtenerTodosEstados;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Presentation.Controllers.Common;
    using System.Net;

    public class MediosController : BaseApiController
    {
        public MediosController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> ObtenerTodos()
        {
            var query = new ObtenerTodosMediosQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
