using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MoqSample2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize our product
            Product myProduct = new Product { ID = 1, Name = "Simple Product", RawPrice = 25.0M };

            //Create a mock with Moq
            Mock<ITaxCalculator> fakeTaxCalculator = new Mock<ITaxCalculator>();

            // make sure to return 5$ of tax for a 25$ product
            fakeTaxCalculator.Setup(tax => tax.GetTax(25.0M)).Returns(5.0M);

            // Retrived the calculated tax
            decimal calculatedTax = myProduct.GetPriceWithTax(fakeTaxCalculator.Object);

            // Verify that the "GetTax" method was called from  the interface
            fakeTaxCalculator.Verify(tax => tax.GetTax(25.0M));

            // Retrived the calculated tax
            calculatedTax = myProduct.GetPriceWithTax(fakeTaxCalculator.Object);

            // Make sure that the taxes were calculated
            Assert.AreEqual(calculatedTax, 30.0M); // Retrived the calculated tax

            //http://blog.decayingcode.com/2009/02/part-2-basic-of-mocking-with-moq.html

            Console.ReadKey();
        }
    }
}