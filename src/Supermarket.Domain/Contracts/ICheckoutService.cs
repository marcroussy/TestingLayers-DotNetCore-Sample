using Supermarket.Domain.Model;

namespace Supermarket.Domain.Contracts
{
    public interface ICheckoutService
    {
         (double PreTax, double PostTax) Checkout();

         ReceiptItem[] Receipt();
    }
}