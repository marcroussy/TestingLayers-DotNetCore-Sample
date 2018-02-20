using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Supermarket.WebApi.Models;

namespace Supermarket.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class HealthController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            var response = new HealthResponse()
            {
                Name = "Supermarket.WebApi",
                Status = "All Good"
            };
            return new OkObjectResult(response);

        }

    }
}
