using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.APIs.Controllers.Errors;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Buggy
{
    public class BuggyController : ApiControllerBase
    {

        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequest()
        {
            return NotFound(new ApiResponse(404));   // 404
        }

        [HttpGet("serverror")]
        public IActionResult GetServerError()
        {
            throw new Exception();   // 500
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));    // 400 
        }

        [HttpGet("badrequest/{id}")]
        public IActionResult GetValidationError(int id)
        {
            

            return Ok();
        }

        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorizedError()
        {
            return Unauthorized(new ApiResponse(401));
        }
    }
}
