using System;
using System.Text.RegularExpressions;

namespace MoqSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            var mock = new Mock<IFoo>();

            mock.Setup(foo => foo.DoSomething("ping")).Returns(true);

            // out arguments  
            var outString = "ack";
            // TryParse will return true, and the out argument will return "ack", lazy evaluated 
            var m2 = mock.Setup(foo => foo.TryParse("ping", out outString)).Returns(true);

            // ref arguments 
            var instance = new Bar();
            // Only matches if the ref argument to the invocation is the same instance 
            mock.Setup(foo => foo.Submit(ref instance)).Returns(true);
            // access invocation arguments when returning a value 
            mock.Setup(x => x.DoSomething(It.IsAny<string>()))
                .Returns((string s) => s.ToLower()); // Multiple parameters overloads available   
            // throwing when invoked 
            mock.Setup(foo => foo.DoSomething("reset")).Throws<InvalidOperationException>();
            mock.Setup(foo => foo.DoSomething("")).Throws(new ArgumentException("command"));
            // lazy evaluating return value 
            int count = 0;
            mock.Setup(foo => foo.GetCount()).Returns(() => count);
            // returning different values on each invocation 
            var mock1 = new Mock<IFoo>();
            var calls = 0;
            mock1.Setup(foo => foo.GetCountThing())
                .Returns(() => calls)
                .Callback(() => calls++);
            //// returns 0 on first invocation, 1 on the next, and so on 
            Console.WriteLine(mock1.Object.GetCountThing());
            Console.WriteLine(mock1.Object.GetCountThing());
            MatchinArguments();
            RaiseEvents();
        }

        private static void RaiseEvents()
        {
            var mock = new Mock<IFoo>();
            int fooValue = 0;
            // Raising an event on the mock 
            mock.Raise(m => m.MyEvent += null, new FooEventArgs(fooValue));
            // Raising an event on a descendant down the hierarchy 
            //..mock.Raise(m => m.Child.First.FooEvent += null, new FooEventArgs(fooValue));  
            // Causing an event to raise automatically when Submit is invoked
            var res = mock.Setup(foo => foo.Submit());

            //.Raises(f => f.Sent += null, EventArgs.Empty); 
            // The raised event would trigger behavior on the object under test, which  
            // you would make assertions about later (how its state changed as a consequence, typically)  
            // Raising a custom event which does not adhere to the EventHandler pattern 
            // public delegate void MyEventHandler(int i, bool b); 
            //public interface IFoo {   event MyEventHandler MyEvent;  }  
            //var mock = new Mock<IFoo>(); ... 
            // Raise passing the custom arguments expected by the event delegate 
            //mock.Raise(foo => foo.MyEvent += null, 25, true);
        }

        private static void MatchinArguments()
        {
            var mock = new Mock<IFoo>();
            // any value 
            mock.Setup(foo => foo.DoSomething(It.IsAny<string>())).Returns(true);
            // matching Func<int>, lazy evaluated 
            mock.Setup(foo => foo.Add(It.Is<int>(i => i % 2 == 0))).Returns(true);
            // matching ranges 
            mock.Setup(foo => foo.Add(It.IsInRange<int>(0, 10, Range.Inclusive))).Returns(true);
            // matching regex 
            mock.Setup(x => x.DoSomething(It.IsRegex("[a-d]+", RegexOptions.IgnoreCase))).Returns("foo");
        }
    }
}