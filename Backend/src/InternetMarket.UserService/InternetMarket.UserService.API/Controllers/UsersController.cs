using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using InternetMarket.UserService.API.DTOs.Requests.UpdateEmail;
using InternetMarket.UserService.API.DTOs.Requests.UpdateProfile;
using InternetMarket.UserService.API.Extensions;
using InternetMarket.UserService.Application.EmailVerificationToken.EmailChange;
using InternetMarket.UserService.Application.ResetPasswordToken;
using InternetMarket.UserService.Application.Users.Get;
using InternetMarket.UserService.Application.Users.GetMe;
using InternetMarket.UserService.Application.Users.Update.UpdateUserEmail;
using InternetMarket.UserService.Application.Users.Update.UpdateUserPassword;
using InternetMarket.UserService.Application.Users.Update.UpdateUserProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

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

        [HttpGet]
        [Authorize]
        [Route("me")]
        public async Task<IActionResult> GetUserProfileAsync()
        {
            var userId = User.GetUserId();
            var profile = await _mediator.Send(new GetMyProfileQuery(userId));
            return Ok(profile);
        }

        [HttpPut]
        [Authorize]
        [Route("profile")]
        public async Task<IActionResult> UpdateUserProfileAsync([FromBody] UpdateUserProfileRequest request)
        {
            var userId = User.GetUserId();
            await _mediator.Send(new UpdateUserProfileCommand(userId, request.Name));
            return Ok();
        }

        [HttpPost]
        [Route("{id}/email-change")]
        public async Task<IActionResult> InitialEmailChangeAsync([FromBody] ChangeEmailRequest request)
        {
            await _mediator.Send(new EmailChangeCommand(request.Email));
            return Ok();
        }

        [HttpPost]
        [Route("{id}/email-change-confirm")]
        public async Task<IActionResult> UpdateEmailAsync([FromQuery] Guid token, [FromBody] UpdateEmailRequest request)
        {
            var userId = User.GetUserId();
            await _mediator.Send(new UpdateUserEmailCommand(userId, request.Email, token));
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