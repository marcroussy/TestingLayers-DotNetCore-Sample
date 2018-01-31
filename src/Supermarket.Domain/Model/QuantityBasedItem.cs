namespace Supermarket.Domain.Model
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
            return 0d;
        }

        public override double Discount()
        {
            return 0d;
        }
    }
}