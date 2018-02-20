using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Supermarket.Infrastructure.Http.Utilities;
using Supermarket.Infrastructure.Http.Utilities.Authentication;
using Xunit;

namespace Supermarket.Deployment.Tests.Acceptance
{
    // Debugging acceptance tests isn't ideal when the Acceptance Tests and Application being debugged are in the same solution. 
    // There are a few possible solutions:
    //   - Run the API via the command line to debug the acceptance tests (but then you can't debug the API)
    //   - Separate the API and acceptance tests into two separate solutions and have them both open concurrently. 
    //     This is the only way you'll be able to debug the API and the Acceptance Tests at the same time. 

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

        public class Checkout : CheckoutTests
        {

            [Fact]
            public async Task GivenInvalidAuthorization_ThenUnauthorized()
            {
                var response = await HttpRequestFactory.PostAnonymous(_clientApiUrl, _checkoutResourceUrlSegment, new { postalCode= "something" });

                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }

            [Fact]
            public async Task GivenMissingPostalCode_ThenBadRequest()
            {
                var token = AuthenticationHelper.GetToken(_clientApiUrl);

                var response = await HttpRequestFactory.Post(_clientApiUrl, _checkoutResourceUrlSegment, "");

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }

            [Theory]
            [InlineData("J4Z 2B9")]
            [InlineData("H3Y 0J3")]
            public async Task GivenPostalCode_ThenOk(string postalCode)
            {
                var token = AuthenticationHelper.GetToken(_clientApiUrl);

                var response = await HttpRequestFactory.Post(_clientApiUrl, _checkoutResourceUrlSegment, new {  postalCode });

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }

            [Theory]
            [InlineData("J4Z 2B9")]
            [InlineData("H3Y 0J3")]
            public async Task GivenPostalCode_ThenHasCorrectResponseItems(string postalCode)
            {
                var token = AuthenticationHelper.GetToken(_clientApiUrl);

                var response = await HttpRequestFactory.Post(_clientApiUrl, _checkoutResourceUrlSegment, new { postalCode });

                dynamic responseContent = response.ContentAsDynamic();
                Assert.NotNull(responseContent.preTax);
                Assert.NotNull(responseContent.postTax);
            }

            [Theory]
            [InlineData("H2Y 1C6", 1.15)]
            [InlineData("K1A 0A4", 1.10)]
            public async Task GivenPostalCode_ThenCorrectPostTaxAmount(string postalCode, double taxRate)
            {
                var token = AuthenticationHelper.GetToken(_clientApiUrl);

                var response = await HttpRequestFactory.Post(_clientApiUrl, _checkoutResourceUrlSegment, new { postalCode });

                dynamic responseContent = response.ContentAsDynamic();
                var preTax = (double)responseContent.preTax;
                var postTax = (double)responseContent.postTax;
                Assert.Equal(preTax * taxRate, postTax);
            }

        }

    }
}
