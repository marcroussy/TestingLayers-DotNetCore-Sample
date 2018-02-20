using System.Threading.Tasks;
using Supermarket.Core.Entities;

namespace Supermarket.Core.Contracts
{
    public interface ICheckoutService
    {
         Task<(double PreTax, double PostTax)> Checkout(string postalCode);

         ReceiptItem[] Receipt();
    }
}