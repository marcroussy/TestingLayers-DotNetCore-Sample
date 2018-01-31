using Supermarket.Domain.Model;

namespace Supermarket.Domain.Contracts
{
    public interface ICart
    {
        CartItem[] Get();

    }
}