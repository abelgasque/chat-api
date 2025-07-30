using System;
using Newtonsoft.Json;

namespace ChatApi.Domain.Entities.Settings
{
    public class ApplicationSettings
    {
        [JsonProperty("Secret")]
        public string Secret { get; set; }

        [JsonProperty("RefreshSecret")]
        public string RefreshSecret { get; set; }

        [JsonProperty("ExpireIn")]
        public int ExpireIn { get; set; }

        [JsonProperty("ExpireDays")]
        public int ExpireDays { get; set; }

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

        [JsonProperty("TenantDb")]
        public string TenantDb { get; set; }

        [JsonProperty("UserId")]
        public string UserId { get; set; }

        [JsonProperty("PasswordDb")]
        public string PasswordDb { get; set; }

        [JsonProperty("Redis")]
        public string Redis { get; set; }

        public string GetConnectionString(string database = null)
        {
            var server = Environment.GetEnvironmentVariable("DbServer") ?? Server;
            var port = Environment.GetEnvironmentVariable("DbPort") ?? Port;
            var dbName = string.IsNullOrEmpty(database)
                ? (Environment.GetEnvironmentVariable("Database") ?? Database)
                : database;
            var user = Environment.GetEnvironmentVariable("DbUser") ?? UserId;
            var password = Environment.GetEnvironmentVariable("DbPassword") ?? PasswordDb;

            return string.Format(ConnectionString, server, port, dbName, user, password);
        }

        public string GetConnectionStringTenant()
        {
            var server = Environment.GetEnvironmentVariable("DbServer") ?? Server;
            var port = Environment.GetEnvironmentVariable("DbPort") ?? Port;
            var tenant = Environment.GetEnvironmentVariable("DbTenant") ?? TenantDb;
            var user = Environment.GetEnvironmentVariable("DbUser") ?? UserId;
            var password = Environment.GetEnvironmentVariable("DbPassword") ?? PasswordDb;

            return string.Format(ConnectionString, server, port, tenant, user, password);
        }

        public string GetConnectionStringRedis()
        {
            return Environment.GetEnvironmentVariable("Redis") ?? Redis;
        }
    }
}
