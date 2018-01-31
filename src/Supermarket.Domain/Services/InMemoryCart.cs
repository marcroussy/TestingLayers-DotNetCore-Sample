using System.Collections.Generic;
using System.Linq;
using Supermarket.Domain.Contracts;
using Supermarket.Domain.Model;

namespace Supermarket.Domain.Services
{
    public class InMemoryCartService : ICart
    {
        public CartItem[] Get()
        {
            var cart = new List<CartItem>();
            cart.Add(new QuantityBasedItem("141410004", "Banana", 5));
            cart.Add(new QuantityBasedItem("485730384", "Chicken Breasts", 3));
            cart.Add(new QuantityBasedItem("947620229", "Tuna John West", 1));
            cart.Add(new WeightBasedItem("264633838", "White Ham", 0.305));
            cart.Add(new WeightBasedItem("141410004", "Salami", 0.85));

            return cart.ToArray();
        }
    }
}