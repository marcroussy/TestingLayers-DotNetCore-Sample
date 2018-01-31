using System.Linq;
using Supermarket.Domain.Contracts;
using Supermarket.Domain.Model;

namespace Supermarket.Domain.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ICart _cart;
        private readonly ITaxCalculator _taxCalculator;

        public CheckoutService(
            ICart cart,
            ITaxCalculator taxCalculator)
        {
            _cart = cart;
            _taxCalculator = taxCalculator;
        }

        public (double PreTax, double PostTax) Checkout()
        {
            double preTaxCost = CalculateItemCost();

            double taxAmount = _taxCalculator.Apply(preTaxCost, "H3Z J9B");

            return (preTaxCost, preTaxCost + taxAmount);
        }

        public ReceiptItem[] Receipt()
        {
            var cartItems = _cart.Get().Select(item => 
                new ReceiptItem() { Name = item.Name, Cost = item.Cost(), Discount = item.Discount() });
            return cartItems.ToArray();
        }

        private double CalculateItemCost()
        {
            double itemCost = 0;
            foreach (var item in _cart.Get())
            {
                itemCost += item.Cost();
            }
            return itemCost;
        }
    }
}