namespace Supermarket.Domain.Model
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
            // Lookup cost by kg for item


            return Weight;
        }

        public override double Discount()
        {
            return 0d;
        }
    }
}