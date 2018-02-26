using Newtonsoft.Json;

namespace Cake.AppCenter
{
    public class AppSettings
    {
        /// <summary>
        /// A short text describing the app.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// The descriptive name of the app. This can contain any characters.
        /// </summary>
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// The name of the app used in URLs.
        /// </summary>
        /// <remarks>AppCenter does not allow spaces in the name.</remarks>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The OS the app will be running on.
        /// </summary>.
        [JsonProperty("os")]
        public string OS { get; set; }

        /// <summary>
        /// The platform of the app.
        /// </summary>
        [JsonProperty("platform")]
        public string Platform { get; set; }

    }
}