using System;
using System.Linq;
using System.Threading.Tasks;
using Supermarket.Core.Contracts;

namespace Supermarket.Core.Services
{
    public class TaxCalculator : ITaxCalculator
    {
        private const double QuebecTaxRate = 1.15;
        private const double RestOfCanadaTaxRate = 1.10;

        private IGeoLocationClient _locationClient;

        public TaxCalculator(IGeoLocationClient locationClient)
        {
            _locationClient = locationClient;
        }

        public async Task<double> Apply(double amount, string postalCode)
        {
            // Logic is as simple as possible because the goal is to explore the testing options, not the complexity of the problem.
            var response = await _locationClient.GetDetails(postalCode);
            if (response.Province == "QC")
            {
                return amount * QuebecTaxRate;
            }
            else
            {
                return amount * RestOfCanadaTaxRate;
            }
        }
    }
}