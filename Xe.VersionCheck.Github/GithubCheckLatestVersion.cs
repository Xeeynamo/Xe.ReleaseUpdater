using System.Linq;
using System.Threading.Tasks;
using Xe.Http;
using Xe.VersionCheck.GitHub.Models;

namespace Xe.VersionCheck
{
    public class GithubCheckLatestVersion : ICheckLatestVersion
    {
        private readonly string BaseApi = "https://api.github.com";

        public GithubCheckLatestVersion(string author, string repository)
        {
            Author = author;
            Repository = repository;
        }

        public string Author { get; }
        public string Repository { get; }

        public string GetLatestVersion() => GetLatestVersionAsync().Result;

        public async Task<string> GetLatestVersionAsync()
        {
            using (var httpClient = new HttpClientWrapper(BaseApi))
            {
                var releases = await httpClient.GetJson<RepoReleaseModel[]>($"/repos/{Author}/{Repository}/releases");
                return releases?.FirstOrDefault()?.TagName;
            }
        }
    }
}
