using System;

namespace MoqSamples
{
    public interface IFoo
    {
        object DoSomething(string p);
        bool TryParse(string p, out string outString);
        bool Submit(ref Bar instance);
        int GetCount();
        int GetCountThing();

        bool Add(int p);

        // int Returns();
        event EventHandler MyEvent;

        object FooEvent { get; set; }

        bool Submit();
    }
}