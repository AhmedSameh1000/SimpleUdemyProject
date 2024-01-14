using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Application.ResponseHandler;

namespace UdemyProject.Application.Features.CourseLecture.CourseLectureCommand.Models
{
    public record CreateLectureModelCommand(int SectionId) : IRequest<ResponseModel<bool>>;
}