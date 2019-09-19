using System;
using Moq;

namespace QuickStart
{

    public interface ILoveThisFramework
    {
        Person DownloadExists(string p);

        DateTime Now { get; set; }
    }

    public class Person
    {
        public string Name { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var mock = new Mock<ILoveThisFramework>(MockBehavior.Loose);
            mock.SetupGet(f => f.Now).Returns(new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Local));
            mock.Setup(framework => framework.DownloadExists("2.0.0.0"))
                .Returns(new Person { Name = "Thomas" });
            // Hand mock.Object as a collaborator and exercise it, 
            // like calling methods on it...

            ILoveThisFramework lovable = mock.Object;
            Person download = lovable.DownloadExists("2.0.0.0");
            var p = lovable.Now;

            //        download = lovable.DownloadExists("2.0.0.0");
            // Verify that the given method was indeed called with the expected value
            mock.Verify(framework => framework.DownloadExists(It.Is<string>(s => s.Contains("2"))), Times.Once);

            download = lovable.DownloadExists("2.0.0.1");

            Console.ReadKey();
        }
    }
}
