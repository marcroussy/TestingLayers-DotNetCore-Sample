using System;
using System.Collections.Generic;
using System.Text;

namespace Supermarket.Core.Clients
{
    internal class GoogleMapsResult
    {
        public string formatted_address { get; set; }
        public List<GoogleAddressComponent> address_components { get; set; }
    }
}
