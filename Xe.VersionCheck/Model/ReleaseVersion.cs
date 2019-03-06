namespace Xe.VersionCheck.Model
{
	public class ReleaseVersion
	{
		public Version Version { get; set; }

		public string Title { get; set; }

		public string Changelog { get; set; }

		public string PageUri { get; set; }

		public string DownloadUri { get; set; }
	}
}
