using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.API.DTOs.Requests.UpdateProfile;
using InternetMarket.UserService.Application.Users.Update.UpdateUserProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPut]
        [Authorize]
        [Route("profile")]
        public async Task<IActionResult> UpdateUserProfileAsync([FromBody] UpdateUserProfileRequest request)
        {
            var userId = Guid.Parse(User.Identity.Name);
            await _mediator.Send(new UpdateUserProfileCommand(userId, request.Name, request.Email));
            return Ok();
        }
    }
}