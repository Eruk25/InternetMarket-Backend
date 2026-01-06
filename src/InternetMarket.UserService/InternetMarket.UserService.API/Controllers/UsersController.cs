using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.API.DTOs.Requests.UpdateProfile;
using InternetMarket.UserService.API.Exstensions;
using InternetMarket.UserService.Application.ResetPasswordToken;
using InternetMarket.UserService.Application.Users.Update.UpdateUserPassword;
using InternetMarket.UserService.Application.Users.Update.UpdateUserProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
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
            var userId = User.GetUserId();
            await _mediator.Send(new UpdateUserProfileCommand(userId, request.Name, request.Email));
            return Ok();
        }

        [HttpPost]
        [Route("forgot-password")]
        public async Task<ActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordRequest request)
        {
            await _mediator.Send(new ForgotPasswordCommand(request.Email));
            return Ok();
        }

        [HttpPatch]
        [Route("reset-password")]
        public async Task<ActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest request)
        {
            await _mediator.Send(new UpdateUserPasswordCommand(
                request.Email,
                request.ResetCode,
                request.NewPassword));
            return Ok();
        }
    }
}