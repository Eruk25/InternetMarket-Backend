using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.API.DTOs.Requests.Register;
using InternetMarket.UserService.Application.Users.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InternetMarket.UserService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync([FromBody] RegisterUserRequest request)
        {
            await _mediator.Send(new RegisterUserCommand(request.Name, request.Email, request.Password));
            return Ok();
        }

    }
}