using System;
using System.Net.Http;

namespace MoqSamples.Basic
{
    /// <summary>
    /// Wrapper implementation of <see cref="IHttpClient"/>
    /// based on <see cref="System.Net.Http.HttpClient"/>.
    /// </summary>
    public class HttpClientWrapper : IHttpClient
    {
        private readonly HttpClient httpClient;

        public HttpClientWrapper()
        {
            this.httpClient = new HttpClient();
        }

        public bool DownloadExists(string version)
        {
            throw new NotImplementedException();

            // TODO: For a real implementation of this method, we would probably use an async Task<byte[]>
            //       as return value since HttpClient provides async operations to perform HTTP GET, POST, PUT, etc...

            //var requestUri = $"https://img.shields.io/badge/cas.net-{version}-purple";
            //var response = await this.httpClient.GetAsync(requestUri);
            //response.EnsureSuccessStatusCode();

            //var imageBytes = await response.Content.ReadAsByteArrayAsync();
            //return imageBytes != null;
        }
    }
}