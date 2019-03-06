using Xunit;

namespace Xe.VersionCheck.Tests
{
    public class GithubCheckLatestVersionTests
    {
        [Theory]
        [InlineData("xeeynamo", "kh3saveeditor", "v1.1.0")]
        public void GetLatestVersion(string author, string repository, string expectedVersion)
        {
            var checkLatestVersion = new GithubCheckLatestVersion(author, repository);
            var actualVersion = checkLatestVersion.GetLatestReleaseAsync().Result;
            Assert.True(Version.Parse(expectedVersion) >= actualVersion.Version);
        }
    }
}
