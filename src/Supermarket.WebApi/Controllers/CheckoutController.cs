using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Supermarket.Core.Contracts;
using Supermarket.WebApi.Models;

namespace Supermarket.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CheckoutController : Controller
    {
        private ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        [Authorize]
        [HttpPost]
        [RequestSizeLimit(24)]
        public async Task<IActionResult> Post([FromBody]CheckoutRequest request)
        {
            if (request == null || 
                request.PostalCode == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var checkoutResult = await _checkoutService.Checkout(request.PostalCode);
            var response = new CheckoutResponse() { PreTax = checkoutResult.PreTax, PostTax = checkoutResult.PostTax };
            return new OkObjectResult(response);
        }

    }
}