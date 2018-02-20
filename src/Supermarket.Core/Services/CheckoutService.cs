using System;
using System.Linq;
using System.Threading.Tasks;
using Supermarket.Core.Contracts;
using Supermarket.Core.Entities;

namespace Supermarket.Core.Services
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

        public async Task<(double PreTax, double PostTax)> Checkout(string postalCode)
        {
            if (string.IsNullOrEmpty(postalCode))
            {
                throw new ArgumentException(nameof(postalCode));
            }
            double preTaxCost = CalculateItemCost();
            double taxAmount = await _taxCalculator.Apply(preTaxCost, postalCode);
            return (preTaxCost, taxAmount);
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