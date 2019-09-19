using System;

namespace MoqSamples
{
    public interface ILoveThisFramework
    {
        Person DownloadExists(string p);

        DateTime Now { get; set; }
    }
}