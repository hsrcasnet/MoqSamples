using System;

namespace MoqSamples
{
    public interface IShoppingCart
    {
        event EventHandler<ProductEventArgs> ProductAdded;

        void Add(IProduct product);
    }
}