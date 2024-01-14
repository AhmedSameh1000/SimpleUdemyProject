using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Application.Features.Course.CourseCommands.Models;
using UdemyProject.Contracts.DTOs.CourseDTOs;

namespace UdemyProject.Application.Features.Course.CourseCommands.Validators
{
    public class CreateCourseRequirmentCommandValidator : AbstractValidator<CoursePrerequisiteDTO>
    {
        public CreateCourseRequirmentCommandValidator()
        {
            ApplyValidation();
        }

        public void ApplyValidation()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("{ProperyName} is required");
        }
    }
}