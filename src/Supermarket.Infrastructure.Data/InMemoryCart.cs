using System.Collections.Generic;
using System.Linq;
using Supermarket.Core.Contracts;
using Supermarket.Core.Entities;

namespace Supermarket.Infrastructure.Data
{
    public class InMemoryCartService : ICart
    {
        public CartItem[] Get()
        {
            var cart = new List<CartItem>();
            cart.Add(new QuantityBasedItem("A", "Banana", 5));
            cart.Add(new QuantityBasedItem("B", "Chicken Breasts", 3));
            cart.Add(new QuantityBasedItem("C", "Tuna John West", 1));
            cart.Add(new WeightBasedItem("D", "White Ham", 0.305));
            cart.Add(new WeightBasedItem("E", "Salami", 0.85));

            return cart.ToArray();
        }
    }
}