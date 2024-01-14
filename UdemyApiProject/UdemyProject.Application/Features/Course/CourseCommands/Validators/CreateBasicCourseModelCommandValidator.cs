using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Contracts.DTOs.Course;

namespace UdemyProject.Application.Features.Course.CourseCommands.Validators
{
    public class CreateBasicCourseModelCommandValidator : AbstractValidator<CourseBasicDataDTO>
    {
        public CreateBasicCourseModelCommandValidator()
        {
            ApplyValidation();
        }

        public void ApplyValidation()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("{PropertyName} is Required");
            RuleFor(c => c.Name).NotNull().WithMessage("{PropertyName} is Required");
            RuleFor(c => c.Category).NotEmpty().WithMessage("{PropertyName} is Required");
            RuleFor(c => c.Category).NotNull().WithMessage("{PropertyName} is Required");
        }
    }
}