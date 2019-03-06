using NSubstitute;
using NSubstitute.Core.Arguments;
using Xunit;

namespace Xe.VersionCheck.Tests
{
    public class VersionCheckerTests
    {
        [Theory]
        [InlineData("v1", "v2")]
        [InlineData("v1.0", "v2.0")]
        [InlineData("1.0", "v2.0")]
        [InlineData("v1.0", "2.0")]
        [InlineData("v1.0", "v1.0.1")]
        [InlineData("v1.9", "v1.10")]
        [InlineData("v0.9.9.8", "v0.9.9.9")]
        [InlineData("v0.9.9.9", "v1.0.0.0")]
        [InlineData("1.0", "2.0")]
        [InlineData("1.9", "1.10")]
        [InlineData("1", "1.1")]
        public void ShouldReturnTrueWhenANewVersionIsFound(string current, string latest)
        {
            var checkCurrentVersion = Substitute.For<ICheckCurrentVersion>();
            var checkLatestRelease = Substitute.For<ICheckLatestVersion>();
            var VersionCheck = new VersionChecker(checkCurrentVersion, checkLatestRelease);

            checkCurrentVersion.GetCurrentVersion().Returns(x => current);
            checkLatestRelease.GetLatestVersion().Returns(x => latest);
;
            Assert.True(VersionCheck.IsUpdateAvailable);
        }

        [Theory]
        [InlineData("v1.0", "v1.0")]
        [InlineData("v1.0", "v1.0.0")]
        [InlineData("v1.0", "v1.0.0.0")]
        [InlineData("v1", "v1.0.0.0")]
        [InlineData("v1.1", "v1.1")]
        [InlineData("v1.1", "v1.01")]
        [InlineData("v2.0", "v1.9.9.9")]
        [InlineData("2.0", "v1.9.9.9")]
        [InlineData("v2.0", "1.9.9.9")]
        public void ShouldReturnFalseWhenANewVersionIsNotFound(string current, string latest)
        {
            var checkCurrentVersion = Substitute.For<ICheckCurrentVersion>();
            var checkLatestRelease = Substitute.For<ICheckLatestVersion>();
            var VersionCheck = new VersionChecker(checkCurrentVersion, checkLatestRelease);

            checkCurrentVersion.GetCurrentVersion().Returns(x => current);
            checkLatestRelease.GetLatestVersion().Returns(x => latest);

            Assert.False(VersionCheck.IsUpdateAvailable);
        }
    }
}
