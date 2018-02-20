using System;
using System.Net;
using System.Threading.Tasks;
using Supermarket.Infrastructure.Http.Utilities;
using Xunit;

namespace Supermarket.Deployment.Tests.External
{
    public class GoogleMapsContractTests
    {
        private readonly string _clientApiUrl;
        public GoogleMapsContractTests()
        {
            // In a real project, this would likely come from a config or environment variable 
            // since we'll want to run the tests against every environment that we deploy to.
            _clientApiUrl = $"https://maps.googleapis.com/maps/api/geocode";
        }

        [Fact]
        public async Task GivenValidRequest_ThenHasExpectedResponseCode()
        {
            var response = await HttpRequestFactory.GetAnonymous(_clientApiUrl, $"json?address=J3Y%209H5");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("J3Y%209H5")]
        public async Task GivenValidRequest_ThenHasExpectedResponseItems(string postalCode)
        {
            var response = await HttpRequestFactory.GetAnonymous(_clientApiUrl, $"json?address={postalCode}");

            dynamic responseContent = response.ContentAsDynamic();
            Assert.NotNull(responseContent.results);
            Assert.NotNull(responseContent.results[0].place_id);
            Assert.NotNull(responseContent.results[0].formatted_address);
        }

    }
}
