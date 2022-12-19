using Newtonsoft.Json;

namespace SecurityApp.Web.Entities.Settings
{
    public class ApplicationSettings
    {
        [JsonProperty("ConnectionString")]
        public string ConnectionString { get; set; }

        [JsonProperty("Server")]
        public string Server { get; set; }

        [JsonProperty("Port")]
        public string Port { get; set; }

        [JsonProperty("Database")]
        public string Database { get; set; }

        [JsonProperty("UserId")]
        public string UserId { get; set; }

        [JsonProperty("PasswordDb")]
        public string PasswordDb { get; set; }
    }
}
