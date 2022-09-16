namespace MoqSamples.Basic
{
    public interface ILoveThisFramework
    {
        bool DownloadExists(string version);
    }

    public class MySqlLoveThisFramework : ILoveThisFramework
    {
        public bool DownloadExists(string version)
        {
            // SELECT * FROM Database...
            throw new System.NotImplementedException();
        }
    }
}