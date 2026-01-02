using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.API.DTOs.Requests.Login;
using InternetMarket.UserService.API.DTOs.Requests.Register;
using InternetMarket.UserService.Application.EmailVerificationToken.VerifyEmail;
using InternetMarket.UserService.Application.ResetPasswordToken;
using InternetMarket.UserService.Application.Users.Login;
using InternetMarket.UserService.Application.Users.Register;
using InternetMarket.UserService.Application.Users.Update.UpdateUserPassword;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
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

        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginAsync([FromBody] LoginUserRequest request)
        {
            var token = await _mediator.Send(new LoginUserCommand(request.Email, request.Password));
            return Ok(token);
        }

        [HttpGet("verify-email")]
        public async Task<ActionResult> VerifyEmailAsync([FromQuery] Guid token)
        {
            var success = await _mediator.Send(new VerifyEmailCommand(token));
            return success ? Ok() : BadRequest();
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