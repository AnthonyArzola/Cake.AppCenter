using Newtonsoft.Json;

namespace Cake.AppCenter.Model
{
    public class Owner
    {
        public string ID { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }
    }
}