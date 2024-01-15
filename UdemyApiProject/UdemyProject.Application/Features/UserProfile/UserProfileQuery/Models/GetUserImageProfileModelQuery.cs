using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Application.ResponseHandler;

namespace UdemyProject.Application.Features.UserProfile.UserProfileQuery.Models
{
    public record GetUserImageProfileModelQuery(string UserId) : IRequest<ResponseModel<string>>;
}