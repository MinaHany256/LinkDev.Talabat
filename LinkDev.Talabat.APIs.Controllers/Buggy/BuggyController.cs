using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Buggy
{
    public class BuggyController : ApiControllerBase
    {

        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequest()
        {
            return NotFound();   // 404
        }

        [HttpGet("serverror")]
        public IActionResult GetServerError()
        {
            throw new Exception();   // 500
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new {StatusCode = 400 , Message = "Bad request"});    // 400 
        }

        [HttpGet("badrequest/{id}")]
        public IActionResult GetValidationError(int id)
        {
            return Ok();
        } 

        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorizedError()
        {
            return Unauthorized(new { StatusCode = 401, Message = "unauthorized" });
        }
    }
}
