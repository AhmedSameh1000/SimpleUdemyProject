using MediatR;
using Microsoft.Extensions.Localization;
using UdemyProject.Application.Features.Authentication.AuthenticationQueries.Models;
using UdemyProject.Application.ResponseHandler;
using UdemyProject.Application.Shared;
using UdemyProject.Contracts.DTOs.AuthDTOs;
using UdemyProject.Contracts.ServicesContracts;

namespace UdemyProject.Application.Features.Authentication.AuthenticationQueries.Handlers
{
    internal class LogInQueryHandler : ResponseHandlerModel,
        IRequestHandler<LoginModelQuery, ResponseModel<AuthModel>>
    {
        private readonly IAuthServices _AuthServices;

        public LogInQueryHandler(IStringLocalizer<Sharedresources> stringLocalizer, IAuthServices authServices) : base(stringLocalizer)
        {
            _AuthServices = authServices;
        }

        public async Task<ResponseModel<AuthModel>> Handle(LoginModelQuery request, CancellationToken cancellationToken)
        {
            var Result = await _AuthServices.LoginAsync(request.LogInDto);

            if (Result.Message is not null)
            {
                return BadRequest<AuthModel>(Result.Message);
            }

            return Success(Result);
        }
    }
}