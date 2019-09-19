using System;
using System.Collections.Generic;

namespace MoqSamples
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly List<IProduct> Products = new List<IProduct>();
        public virtual event EventHandler<ProductEventArgs> ProductAdded = delegate { };

        public virtual void Add(IProduct product)
        {
            if (product.IsValid)
            {
                this.Products.Add(product);
                this.ProductAdded(this, new ProductEventArgs(product));
            }
        }
    }
}