using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Application.ResponseHandler;
using UdemyProject.Contracts.DTOs.AuthDTOs;

namespace UdemyProject.Application.Features.Authentication.AuthenticationCommands.Models
{
    public record RefreshTokenModelCommand(string userId) : IRequest<ResponseModel<AuthModel>>;
}