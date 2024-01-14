﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyProject.Application.Features.CourseLecture.CourseLectureCommand.Models;
using UdemyProject.Contracts.DTOs.LectureDTOs;

namespace UdemyProject.Api.Controllers
{
    public class CourseLectureController : AppBaseController
    {
        private readonly IMediator _Mediator;

        public CourseLectureController(IMediator mediator)
        {
            _Mediator = mediator;
        }

        [HttpPost("CreateLecture")]
        public async Task<IActionResult> CreateLectue(int SectionId)
        {
            var Response = await _Mediator.Send(new CreateLectureModelCommand(SectionId));
            return NewResult(Response);
        }

        [HttpPut("UpdateLecture")]
        public async Task<IActionResult> UpdateLecture([FromForm] LectureForUpdateDTO lectureForUpdateDTO)
        {
            var Response = await _Mediator.Send(new UpdateLectureModelCommand(lectureForUpdateDTO));
            return NewResult(Response);
        }
    }
}