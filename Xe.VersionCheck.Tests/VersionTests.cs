using System;
using Xunit;

namespace Xe.VersionCheck.Tests
{
    public class VersionTests
    {
        [Theory]
        [InlineData(1, 0, 0)]
        [InlineData(1, 2, 3)]
        public void ShouldInitializeVersionFromNumbers(int major, int minor, int revision)
        {
            var version = new Version(major, minor, revision);
            Assert.Equal(major, version.Major);
            Assert.Equal(minor, version.Minor);
            Assert.Equal(revision, version.Revision);
        }

        [Theory]
        [InlineData("v1", 1, 0, 0)]
        [InlineData("v1.0", 1, 0, 0)]
        [InlineData("v1.0.0", 1, 0, 0)]
        [InlineData("v1.2.3", 1, 2, 3)]
        [InlineData("ver1.2.3", 1, 2, 3)]
        [InlineData("version1.2.3", 1, 2, 3)]
        [InlineData("1", 1, 0, 0)]
        [InlineData("1.0", 1, 0, 0)]
        [InlineData("1.0.0", 1, 0, 0)]
        [InlineData("v2.34.567", 2, 34, 567)]
        [InlineData("02.034.506", 2, 34, 506)]
        public void ShouldParseVersion(string strVersion, int major, int minor, int revision)
        {
            Assert.True(Version.TryParse(strVersion, out var version));
            Assert.Equal(major, version.Major);
            Assert.Equal(minor, version.Minor);
            Assert.Equal(revision, version.Revision);
        }

        [Fact]
        public void VersionComparisonOperatorsShouldWork()
        {
            Assert.True(new Version(1, 2, 3) > new Version(1, 2, 2));
            Assert.False(new Version(1, 2, 3) < new Version(1, 2, 2));
            Assert.True(new Version(1, 2, 3) >= new Version(1, 2, 2));
            Assert.True(new Version(1, 2, 3) >= new Version(1, 2, 3));
            Assert.True(new Version(1, 2, 3) <= new Version(1, 2, 3));
            Assert.True(new Version(1, 2, 3) == new Version(1, 2, 3));
            Assert.False(new Version(1, 2, 3) != new Version(1, 2, 3));
        }
    }
}
