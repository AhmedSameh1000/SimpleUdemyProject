using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Application.ResponseHandler;
using UdemyProject.Contracts.DTOs.LectureDTOs;

namespace UdemyProject.Application.Features.CourseLecture.CourseLectureCommand.Models
{
    public record UpdateLectureModelCommand(LectureForUpdateDTO Lecture) : IRequest<ResponseModel<bool>>;
}