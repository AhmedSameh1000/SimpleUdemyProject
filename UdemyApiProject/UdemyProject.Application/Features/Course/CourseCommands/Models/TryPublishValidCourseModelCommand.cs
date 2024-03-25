using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Application.ResponseHandler;

namespace UdemyProject.Application.Features.Course.CourseCommands.Models
{
    public record TryPublishValidCourseModelCommand(string userId, int courseId) : IRequest<ResponseModel<bool>>;
}