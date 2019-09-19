using System;

namespace MoqSamples
{
    public class ProductEventArgs : EventArgs
    {
        public ProductEventArgs(IProduct product)
        {
            this.Product = product;
        }

        public IProduct Product { get; private set; }
    }
}