using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Supermarket.Infrastructure.Http.Utilities;
using Supermarket.Infrastructure.Http.Utilities.Authentication;
using Xunit;

namespace Supermarket.Deployment.Tests.Security
{
    public class CheckoutTests
    {
        private readonly string _clientApiUrl;
        private readonly string _checkoutResourceUrlSegment;

        public CheckoutTests()
        {
            // In a real project, this would likely come from a config or environment variable 
            // since we'll want to run the tests against every environment that we deploy to.
            _clientApiUrl = $"http://{Environment.GetEnvironmentVariable("TL_API_URL")}";
            _checkoutResourceUrlSegment = "checkout";
        }

        public class CheckoutSecurity : CheckoutTests
        {

            [Fact]
            public async Task GivenMissingAuthorization_ThenUnauthorized()
            {
                var response = await HttpRequestFactory.PostAnonymous(_clientApiUrl, _checkoutResourceUrlSegment, new { postalCode = "K1A 0A4" });

                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }

            [Fact]
            public async Task GivenInvalidAuthorization_ThenUnauthorized()
            {
                var response = await HttpRequestFactory.Post(_clientApiUrl, _checkoutResourceUrlSegment, new { postalCode = "K1A 0A4" }, "totally-invalid-token");

                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }

            [Theory]
            [InlineData("94J3J92")]
            [InlineData("11A 0A4")]
            public async Task GivenInvalidPostalCode_ThenBadRequest(string postalCode)
            {
                var response = await HttpRequestFactory.Post(_clientApiUrl, _checkoutResourceUrlSegment, new { postalCode });

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }

            [Theory]
            [InlineData("94J3J92AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA")]
            public async Task GivenLargeRequest_ThenBadRequest(string postalCode)
            {
                var response = await HttpRequestFactory.Post(_clientApiUrl, _checkoutResourceUrlSegment, new { postalCode });

                Assert.Equal(HttpStatusCode.RequestEntityTooLarge, response.StatusCode);
            }

        }

    }
}
