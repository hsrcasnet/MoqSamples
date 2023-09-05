using System.Diagnostics;

namespace MoqSamples.Basic
{
    public class RapidshareService
    {
        private readonly IHttpClient httpClient;

        public RapidshareService(IHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public bool DownloadExists(string version)
        {
            var downloadExists = this.httpClient.DownloadExists(version);
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