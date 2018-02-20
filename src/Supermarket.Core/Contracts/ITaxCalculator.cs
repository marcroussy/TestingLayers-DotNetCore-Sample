using System.Threading.Tasks;

namespace Supermarket.Core.Contracts
{
    public interface ITaxCalculator
    {
         Task<double> Apply(double amount, string postalCode);
    }
}