using HelpersLayer.Helpers.ResponseHandler;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HelpersLayer.Helpers.Bases
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class AppControllerBase : ControllerBase
    {
        public static IActionResult ApiResult<T>(IApiResponse<T> response)
        {
            return response.StatusCode switch
            {
                HttpStatusCode.OK => new OkObjectResult(response),
                HttpStatusCode.Created => new CreatedResult(string.Empty, response),
                HttpStatusCode.Unauthorized => new UnauthorizedObjectResult(response),
                HttpStatusCode.BadRequest => new BadRequestObjectResult(response),
                HttpStatusCode.NotFound => new NotFoundObjectResult(response),
                HttpStatusCode.Accepted => new AcceptedResult(string.Empty, response),
                HttpStatusCode.UnprocessableEntity => new UnprocessableEntityObjectResult(response),
                HttpStatusCode.NoContent => new NoContentResult(),
                HttpStatusCode.Forbidden => new ObjectResult(response) { StatusCode = (int)HttpStatusCode.Forbidden }
,
                _ => new BadRequestObjectResult(response)

            };
        }
    }
}
