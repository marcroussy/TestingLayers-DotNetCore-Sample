namespace Supermarket.Domain.Contracts
{
    public interface ITaxCalculator
    {
         double Apply(double amount, string postalCode);
    }
}