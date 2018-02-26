using System.Collections.Generic;

using Cake.AppCenter.Model;
using Newtonsoft.Json;

namespace Cake.AppCenter.Response
{
    public class AppResponse
    {
        public string ID { get; set; }

        public string Description { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }

        public string Name { get; set; }

        public string OS { get; set; }

        public Owner Owner { get; set; }

        [JsonProperty("app_secret")]
        public string AppSecret { get; set; }

        public string Platform { get; set; }

        public string Origin { get; set; }

        [JsonProperty("member_permissions")]
        public List<string> MemberPermissions { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }
}