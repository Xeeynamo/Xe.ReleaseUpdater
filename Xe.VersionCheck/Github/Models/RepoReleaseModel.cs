using System.Collections.Generic;
using Newtonsoft.Json;

namespace Xe.VersionCheck.GitHub.Models
{
    public class RepoReleaseModel
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("tag_name")]
        public string TagName { get; set; }

        [JsonProperty("assets")]
        public List<AssetModel> Assets { get; set; }
    }
}
