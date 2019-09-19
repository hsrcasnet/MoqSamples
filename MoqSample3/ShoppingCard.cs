using System;
using System.Collections.Generic;

namespace MoqSamples
{
    public interface IProduct
    {
        bool IsValid { get; }
    }

    public class ProductEventArgs : EventArgs
    {
        public ProductEventArgs(IProduct product)
        {
            Product = product;
        }

        public IProduct Product { get; private set; }
    }

    public interface IShoppingCart
    {
        event EventHandler<ProductEventArgs> ProductAdded;
        void Add(IProduct product);
    }

    public class ShoppingCart : IShoppingCart
    {
        private readonly List<IProduct> Products = new List<IProduct>();
        public virtual event EventHandler<ProductEventArgs> ProductAdded = delegate { };

        public virtual void Add(IProduct product)
        {
            if (product.IsValid)
            {
                Products.Add(product);
                ProductAdded(this, new ProductEventArgs(product));
            }
        }
    }
}
