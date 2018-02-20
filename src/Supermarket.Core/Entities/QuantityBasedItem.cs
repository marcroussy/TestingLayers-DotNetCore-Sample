namespace Supermarket.Core.Entities
{
    public class QuantityBasedItem : CartItem
    {
        public int Quantity { get; set; }
        
        public QuantityBasedItem(string upc, string name, int quantity)
        {
            Upc = upc;
            Name = name;
            Quantity = quantity;
        }

        public override double Cost()
        {
            // For simplicity, all items are the same cost. 
            return 3d;
        }

        public override double Discount()
        {
            // Same here, applying a global 10% discount
            var discountPercent = 0.07d;
            return Cost() * discountPercent;
        }
    }
}
