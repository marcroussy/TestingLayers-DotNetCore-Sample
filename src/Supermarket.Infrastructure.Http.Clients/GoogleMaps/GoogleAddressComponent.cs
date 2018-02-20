using System;
using System.Collections.Generic;
using System.Text;

namespace Supermarket.Core.Clients
{
    internal class GoogleAddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }
}
