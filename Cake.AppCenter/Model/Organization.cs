using Newtonsoft.Json;

namespace Cake.AppCenter.Model
{
    public class Organization
    {
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        public string Name { get; set; }

        public string Origin { get; set; } 
    }
}