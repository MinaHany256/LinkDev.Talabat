﻿using LinkDev.Talabat.APIs.Controllers.Errors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Common
{
    [ApiController]
    [Route("Errors/{Code}")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class ErrorsController : ControllerBase
    {

        [HttpGet]
        public IActionResult Error(int Code)
        {
            if (Code == (int)HttpStatusCode.NotFound)
            {
                var response = new ApiResponse((int)HttpStatusCode.NotFound, $"The Request endpoint: {Request.Path} is not found.");
                return NotFound(response);
            }

            return StatusCode(Code);
        }

    }
}