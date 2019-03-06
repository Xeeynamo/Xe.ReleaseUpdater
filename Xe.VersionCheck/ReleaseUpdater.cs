using System;

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

        public string LatestVersion => _checkLatestVersion.GetLatestVersion();

        public bool IsUpdateAvailable => Version.TryParse(CurrentVersion, out var cur) &&
                                       Version.TryParse(LatestVersion, out var latest) &&
                                       cur < latest;
    }
}
