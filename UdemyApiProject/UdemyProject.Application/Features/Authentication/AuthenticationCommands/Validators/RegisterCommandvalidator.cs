using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Contracts.DTOs.AuthDTOs;

namespace UdemyProject.Application.Features.Authentication.AuthenticationCommands.Validators
{
    public class RegisterCommandvalidator : AbstractValidator<RegisterDto>
    {
        public RegisterCommandvalidator()
        {
            ApplyValidator();
        }

        public void ApplyValidator()
        {
            RuleFor(c => c.Email).
                     NotEmpty().WithMessage("{PropertyName} Must Not be Empty")
                    .NotNull().WithMessage("{PropertyName} Must Not be Null");

            RuleFor(c => c.FullName).
                     NotEmpty().WithMessage("{PropertyName} Must Not be Empty")
                    .NotNull().WithMessage("{PropertyName} Must Not be Null")
                    .MaximumLength(20).WithMessage("{PropertyName} Must Not less than (20) Character");

            RuleFor(c => c.Password).
             NotEmpty().WithMessage("{PropertyName} Must Not be Empty")
            .NotNull().WithMessage("{PropertyName} Must Not be Null");
        }
    }
}