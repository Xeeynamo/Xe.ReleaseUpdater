using System.Collections.Generic;
using Newtonsoft.Json;

namespace Xe.VersionCheck.GitHub.Models
{
    public class RepoReleaseModel
	{
		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("html_url")]
		public string HtmlUrl { get; set; }

		[JsonProperty("tag_name")]
		public string TagName { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("draft")]
		public bool Draft { get; set; }

		[JsonProperty("body")]
		public string Body { get; set; }

		[JsonProperty("assets")]
        public List<AssetModel> Assets { get; set; }
    }
}
