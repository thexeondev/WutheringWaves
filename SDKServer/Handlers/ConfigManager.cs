using System.IO;
using System.Text.Json;

namespace SDKServer.GetConfig
{
    public class ConfigManager
    {
        public class Config
        {
            public HttpConfig Http { get; set; } = new HttpConfig();
            public UdpConfig Udp { get; set; } = new UdpConfig();
        }

        public class HttpConfig
        {
            public string Ip { get; set; } = "";
            public string Name { get; set; } = "";
            public string Port { get; set; } = "";
        }

        public class UdpConfig
        {
            public string Ip { get; set; } = "";
            public int Port { get; set; } 
        }

        public static Config? GetConfig()
        {
            Config? config ;
            try
            {
                var configJson = File.ReadAllText("data/config.json");
                config = JsonSerializer.Deserialize<Config>(configJson);
            }
            catch
            {
                config = new Config
                {
                    Http = new HttpConfig
                    {
                        Ip = "127.0.0.1",
                        Port = "5200",
                        Name = "gktwo"
                    },
                    Udp = new UdpConfig
                    {
                        Ip = "127.0.0.1",
                        Port = 1337
                    }
                };
            }

            return config;
        }
    }
}
