using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UdemyProject.Application.ResponseHandler;

namespace UdemyProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppBaseController : ControllerBase
    {
        public ObjectResult NewResult<T>(ResponseModel<T> response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return Ok(response);

                case HttpStatusCode.Created:
                    return Created(string.Empty, response);

                case HttpStatusCode.Unauthorized:
                    return Unauthorized(response);

                case HttpStatusCode.BadRequest:
                    return BadRequest(response);

                case HttpStatusCode.NotFound:
                    return NotFound(response);

                case HttpStatusCode.Accepted:
                    return Accepted(string.Empty, response);

                case HttpStatusCode.UnprocessableEntity:
                    return UnprocessableEntity(response);

                default:
                    return BadRequest(response);
            }
        }
    }
}