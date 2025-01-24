using Newtonsoft.Json;

namespace SecurityWebApp.Infrastructure.Entities.Settings
{
    public class ApplicationSettings
    {
        [JsonProperty("Secret")]
        public string Secret { get; set; }

        [JsonProperty("ExpireIn")]
        public int ExpireIn { get; set; }

        [JsonProperty("AuthAttempts")]
        public int AuthAttempts { get; set; }
        
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
