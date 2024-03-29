﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Application.ResponseHandler;
using UdemyProject.Contracts.DTOs.UserProfileDTOs;

namespace UdemyProject.Application.Features.UserProfile.UserProfileCommand.Models
{
    public record UpdateUserProfileModelCommand(UserProfileDTO userProfile) : IRequest<ResponseModel<bool>>;
}