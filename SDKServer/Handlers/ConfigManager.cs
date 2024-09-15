using System.IO;
using System.Text.Json;

namespace SDKServer.Handlers
{
    public class ConfigManager
    {
        public class Config
        {
            public SDKServerConfig SDKServer { get; set; } = new SDKServerConfig();
            public GameServerConfig GameServer { get; set; } = new GameServerConfig();
        }

        public class SDKServerConfig
        {
            public string Host { get; set; } = "";
            public string Name { get; set; } = "";
            public string Port { get; set; } = "";
        }

        public class GameServerConfig
        {
            public string Host { get; set; } = "";
            public int Port { get; set; }
        }

        public static Config? GetConfig()
        {
            Config? config;
            try
            {
                var configJson = File.ReadAllText("data/config.json");
                config = JsonSerializer.Deserialize<Config>(configJson);
            }
            catch
            {
                config = new Config
                {
                    SDKServer = new SDKServerConfig
                    {
                        Host = "127.0.0.1",
                        Port = "5200",
                        Name = "gktwo"
                    },
                    GameServer = new GameServerConfig
                    {
                        Host = "127.0.0.1",
                        Port = 1337
                    }
                };
            }

            return config;
        }
    }
}
