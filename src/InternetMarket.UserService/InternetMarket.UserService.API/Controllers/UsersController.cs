using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.API.DTOs.Requests.UpdateProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InternetMarket.UserService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> UpdateUserProfileAsync([FromBody] UpdateUserProfileRequest request)
        {
            await _mediator.Send(new UpdateUserProfileCommand(request.Name, request.Email));
            return Ok();
        }
    }
}