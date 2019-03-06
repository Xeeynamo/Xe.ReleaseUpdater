using System;
using System.Threading.Tasks;
using Xe.VersionCheck.Model;

namespace Xe.VersionCheck
{
    public class VersionChecker
    {
        private readonly ICheckCurrentVersion _checkCurrentVersion;
        private readonly ICheckLatestVersion _checkLatestVersion;

        public VersionChecker(
            ICheckCurrentVersion checkCurrentVersion,
            ICheckLatestVersion checkLatestVersion)
        {
            _checkCurrentVersion = checkCurrentVersion;
            _checkLatestVersion = checkLatestVersion;
        }

        public string CurrentVersion => _checkCurrentVersion.GetCurrentVersion();

        public string LatestVersion => _checkLatestVersion.GetLatestReleaseAsync().Result.Version?.ToString();

        public bool IsUpdateAvailable => CurrentVersion != null && Version.TryParse(CurrentVersion, out var cur) &&
			cur < _checkLatestVersion.GetLatestReleaseAsync().Result.Version;

		public async Task<ReleaseVersion> GetLatestVersionAsync()
		{
			var lastRelease = await _checkLatestVersion.GetLatestReleaseAsync();
			if (Version.TryParse(CurrentVersion, out var currentVersion) &&
				lastRelease.Version > currentVersion)
			{
				return lastRelease;
			}

			return lastRelease;
		}
    }
}
