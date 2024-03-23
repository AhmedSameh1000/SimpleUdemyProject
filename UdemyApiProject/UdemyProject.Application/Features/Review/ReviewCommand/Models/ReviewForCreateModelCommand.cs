using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Application.ResponseHandler;
using UdemyProject.Contracts.DTOs.ReviewDTOs;

namespace UdemyProject.Application.Features.Review.ReviewCommand.Models
{
    public record ReviewForCreateModelCommand(ReviewForCreateDto reviewForCreateDto) : IRequest<ResponseModel<bool>>;
}