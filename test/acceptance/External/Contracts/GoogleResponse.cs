using System;
namespace Supermarket.ExternalDependencyTests.Contracts
{
    public class GoogleResponse
    {
        public string formatted_address { get; set; }
        public string place_id { get; set; }
    }
}
