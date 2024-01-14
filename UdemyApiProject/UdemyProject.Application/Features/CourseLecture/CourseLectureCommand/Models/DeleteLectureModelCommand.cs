using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Application.ResponseHandler;

namespace UdemyProject.Application.Features.CourseLecture.CourseLectureCommand.Models
{
    public record DeleteLectureModelCommand(int LectureId) : IRequest<ResponseModel<bool>>;
}