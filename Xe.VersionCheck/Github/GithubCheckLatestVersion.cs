using System.Linq;
using System.Threading.Tasks;
using Xe.Http;
using Xe.VersionCheck.GitHub.Models;
using Xe.VersionCheck.Model;

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

        public async Task<ReleaseVersion> GetLatestReleaseAsync()
		{
            using (var httpClient = new HttpClientWrapper(BaseApi))
            {
                var releases = await httpClient.GetJson<RepoReleaseModel[]>($"/repos/{Author}/{Repository}/releases");
				var lastRelease = releases.FirstOrDefault(x => !x.Draft);
				if (lastRelease == null)
					return null;

				Version.TryParse(lastRelease.TagName, out var version);
				return new ReleaseVersion()
				{
					Version = version,
					Title = lastRelease.Name,
					Changelog = lastRelease.Body,
					PageUri = lastRelease.HtmlUrl,
					DownloadUri = lastRelease.Assets?.FirstOrDefault()?.BrowserDownloadUrl
				};
            }
        }
    }
}
