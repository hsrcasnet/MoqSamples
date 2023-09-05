namespace MoqSamples.Basic
{
    /// <summary>
    /// Abstraction for a http client.
    /// </summary>
    public interface IHttpClient
    {
        bool DownloadExists(string version);
    }
}