using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UdemyProject.Application.Features.Authentication.AuthenticationCommands.Models;
using UdemyProject.Application.Features.Authentication.AuthenticationQueries.Models;
using UdemyProject.Contracts.DTOs.AuthDTOs;

namespace UdemyProject.Api.Controllers
{
    [ApiController]
    public class AuthController : AppBaseController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var Response = await _mediator.Send(new RegisterModelCommand(registerDto));
            return NewResult(Response);
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(LogInDTo logInDTo)
        {
            var Response = await _mediator.Send(new LoginModelQuery(logInDTo));
            return NewResult(Response);
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(string userId)
        {
            var Response = await _mediator.Send(new RefreshTokenModelCommand(userId));
            return NewResult(Response);
        }

        [Authorize]
        [HttpGet("SecureData")]
        public IActionResult Data()
        {
            return Ok(new List<string> { "Ahmed", "Smaeh" });
        }
    }
}