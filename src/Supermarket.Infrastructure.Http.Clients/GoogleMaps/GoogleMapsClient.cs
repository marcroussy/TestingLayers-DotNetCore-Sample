using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supermarket.Core.Contracts;
using Supermarket.Core.Entities;
using Supermarket.Infrastructure.Http.Utilities;

namespace Supermarket.Core.Clients
{
    public class GoogleMapsClient : IGeoLocationClient
    {
        private readonly string _clientApiUrl;
        public GoogleMapsClient()
        {
            _clientApiUrl = $"https://maps.googleapis.com/maps/api/geocode";
        }

        public async Task<GeoLocation> GetDetails(string postalCode)
        {
            var response = await HttpRequestFactory.GetAnonymous(_clientApiUrl, $"json?address={postalCode}");
            var responseContent = response.ContentAsType<GoogleMapsResults>();

            var adminLevel1 = responseContent.results.SelectMany(result => result.address_components.Where(component => component.types.Any(type => type == "administrative_area_level_1")));

            var location = new GeoLocation()
            {
                Province = adminLevel1.First().short_name
            };

            return location;
        }
    }
}
