using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MoqSamples
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Adding_A_Valid_Product_Fire_Event()
        {
            // Setup our product so that it always returns true on a IsValid verification
            Mock<IProduct> product = new Mock<IProduct>();
            product.Setup(currentProduct => currentProduct.IsValid).Returns(true);

            // setup an event argument for our event
            ProductEventArgs productEventArgs = new ProductEventArgs(product.Object);

            // setup a mocked shopping cart to create our mocked event handler and a true shopping cart to test
            Mock<ShoppingCart> mockedShoppingCart = new Mock<ShoppingCart>();

            //creating the event a mocked event
            // Raising an event on the mock mock.Raise(m => m.FooEvent += null, new FooEventArgs(fooValue));
            // Causing an event to raise automatically when Submit is invoked mock.Setup(foo => foo.Submit()).Raises(f => f.Sent += null, EventArgs.Empty);
            //DEPRICATED MockedEvent<ProductEventArgs> mockedEvent = mockedShoppingCart.CreateEventHandler<ProductEventArgs>();
            //mockedShoppingCart.Object.ProductAdded += mockedEvent;
            mockedShoppingCart.Setup(shopping => shopping.Add(product.Object))
                .Raises(f => f.ProductAdded += null, new ProductEventArgs(product.Object))
                .Verifiable();

            //making the test
            IShoppingCart myShoppingCart = mockedShoppingCart.Object;
            myShoppingCart.Add(product.Object);

            mockedShoppingCart.Verify();
        }

        [TestMethod]
        public void Adding_A_Valid_Product_Fire_Event1()
        {
            // Setup our product so that it always returns true on a IsValid verification
            Mock<IProduct> product = new Mock<IProduct>();
            product.Setup(currentProduct => currentProduct.IsValid).Returns(true);

            // setup an event argument for our event
            ProductEventArgs productEventArgs = new ProductEventArgs(product.Object);

            // creating our objects and events
            ShoppingCart myShoppingCart = new ShoppingCart();
            bool isCalled = false;
            myShoppingCart.ProductAdded += (sender, e) => isCalled = true;

            // Testing the Add method if it fire the event
            myShoppingCart.Add(product.Object);

            // make sure the event was called
            Assert.AreEqual(isCalled, true);
        }

        [TestMethod]
        public void Moq_Test_With_Factories()
        {
            // Initialize factories with default behaviours
            // MockFactory mockFactory = new MockFactory(MockBehavior.Default);
            MockRepository mockFactory = new MockRepository(MockBehavior.Default);
            // Setup parameters for mocking
            mockFactory.CallBase = true;
            mockFactory.DefaultValue = DefaultValue.Mock;

            // create mocks with the factory
            Mock<IProduct> product = mockFactory.Create<IProduct>();
        }
    }
}