using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Supermarket.Core.Entities;

namespace Supermarket.Core.Contracts
{
    public interface IGeoLocationClient
    {
        Task<GeoLocation> GetDetails(string postalCode);
    }
}
