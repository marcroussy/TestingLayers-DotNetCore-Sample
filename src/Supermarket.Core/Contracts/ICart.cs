using Supermarket.Core.Entities;

namespace Supermarket.Core.Contracts
{
    public interface ICart
    {
        CartItem[] Get();
    }
}