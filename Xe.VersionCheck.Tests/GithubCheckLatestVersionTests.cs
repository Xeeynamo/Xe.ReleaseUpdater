using Xunit;

namespace Xe.VersionCheck.Tests
{
    public class GithubCheckLatestVersionTests
    {
        [Theory]
        [InlineData("xeeynamo", "kh3saveeditor", "v1.1")]
        public void GetLatestVersion(string author, string repository, string expectedVersion)
        {
            var checkLatestVersion = new GithubCheckLatestVersion(author, repository);
            var actualVersion = checkLatestVersion.GetLatestVersion();
            Assert.Equal(expectedVersion, actualVersion);
        }
    }
}
