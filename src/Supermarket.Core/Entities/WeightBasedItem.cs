namespace Supermarket.Core.Entities
{
    public class WeightBasedItem : CartItem
    {
        public double Weight { get; set; }

        public WeightBasedItem(string upc, string name, double weight)
        {
            Upc = upc;
            Name = name;
            Weight = weight;
        }

        public override double Cost()
        {
            // For the purposes of the example, all items have the same cost per kg for now. 
            var costPerKg = 2.5d;
            return Weight * costPerKg;
        }

        public override double Discount()
        {
            // Again for simplicity reasons, all items have the same discount. 
            var discountPercent = 0.05d;
            return Cost() * discountPercent;
        }
    }
}