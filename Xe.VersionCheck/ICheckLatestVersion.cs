using System;
using System.Threading.Tasks;
using Xe.VersionCheck.Model;

namespace Xe.VersionCheck
{
    public interface ICheckLatestVersion
    {
        Task<ReleaseVersion> GetLatestReleaseAsync();
    }
}
