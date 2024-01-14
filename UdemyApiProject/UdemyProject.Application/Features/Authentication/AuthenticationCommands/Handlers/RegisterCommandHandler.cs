using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Application.Features.Authentication.AuthenticationCommands.Models;
using UdemyProject.Application.ResponseHandler;
using UdemyProject.Application.Shared;
using UdemyProject.Contracts.DTOs.AuthDTOs;
using UdemyProject.Contracts.ServicesContracts;

namespace UdemyProject.Application.Features.Authentication.AuthenticationCommands.Handlers
{
    public class RegisterCommandHandler : ResponseHandlerModel,
        IRequestHandler<RegisterModelCommand, ResponseModel<AuthModel>>,
        IRequestHandler<RefreshTokenModelCommand, ResponseModel<AuthModel>>
    {
        private readonly IAuthServices _authServices;
        private readonly IValidator<RegisterDto> _RegisterDtoValidtor;

        public RegisterCommandHandler(IStringLocalizer<Sharedresources> stringLocalizer,
            IAuthServices authServices,
            IValidator<RegisterDto> RegisterDtoValidtor) : base(stringLocalizer)
        {
            _authServices = authServices;
            _RegisterDtoValidtor = RegisterDtoValidtor;
        }

        public async Task<ResponseModel<AuthModel>> Handle(RegisterModelCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _RegisterDtoValidtor.Validate(request.RegisterDto);
            if (!validationResult.IsValid)
            {
                return BadRequest<AuthModel>(string.Join(",", validationResult.Errors.Select(c => c.ErrorMessage).ToList()));
            }
            var Response = await _authServices.RegisterAsync(request.RegisterDto);

            if (Response.Message is not null)
            {
                return BadRequest<AuthModel>(Response.Message);
            }
            return Success(Response);
        }

        public async Task<ResponseModel<AuthModel>> Handle(RefreshTokenModelCommand request, CancellationToken cancellationToken)
        {
            var Result = await _authServices.RefreshTokenAsync(request.userId);
            if (!Result.IsAuthenticated)
            {
                return BadRequest<AuthModel>();
            }
            return Success(Result);
        }
    }
}