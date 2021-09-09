using System.Diagnostics;
using MoqSamples.Basic;

namespace MoqSamples.Basic
{
    public class RapidshareService
    {
        private readonly ILoveThisFramework loveThisFramework;

        public RapidshareService(ILoveThisFramework loveThisFramework)
        {
            this.loveThisFramework = loveThisFramework;
        }

        public bool DownloadExists(string version)
        {
            var downloadExists = this.loveThisFramework.DownloadExists(version);
            if (downloadExists)
            {
                Debug.WriteLine($"Download for version '{version}' exists");
            }
            else
            {
                Debug.WriteLine($"Download for version '{version}' does not exist");
            }

            return downloadExists;
        }
    }
}