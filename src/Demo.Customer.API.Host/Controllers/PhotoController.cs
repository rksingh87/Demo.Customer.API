using Microsoft.AspNetCore.Mvc;

namespace Demo.Customer.API.Host.Controllers
{

    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/photos")]
    [ApiController]
    public class PhotoController : ControllerBase
    {

        [HttpGet]
        [MapToApiVersion("1")]
        [Produces("application/json")]
        //[ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(Result<List<DecisionMatrixResponseDto>>))]
        public ActionResult GetResult()
        {
            return Ok(null);
        }

    }
}