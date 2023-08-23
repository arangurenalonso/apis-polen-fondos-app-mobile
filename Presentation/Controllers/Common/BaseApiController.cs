﻿namespace Presentation.Controllers.Common
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected readonly IMediator _mediator;
        public BaseApiController(IMediator mediator)
        {
            _mediator = mediator;

        }

    }
}