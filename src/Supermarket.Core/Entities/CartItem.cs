namespace Supermarket.Core.Entities
{
    public abstract class CartItem
    {
        public string Upc { get; set; }
        
        public string Name { get; set; }
        
        public abstract double Cost();

        public abstract double Discount();
    }
}