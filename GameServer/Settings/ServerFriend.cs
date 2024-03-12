using System.IO;
using System.Text.Json;

namespace GameServer.Settings;

    public class ServerBot
    {
        public class ServerFriend
        {
            public Bot BotConfig { get; set; } = new Bot();
        }
        public class Bot
        {
            public string Name { get; set; } = "";
            public string Signature { get; set; } = "";
            public int HeadId { get; set; }
            public int PlayerId { get; set; }
            public bool IsOnline { get; set; }
            public long LastOfflineTime { get; set; }
            public int Level { get; set; }
        }
        public static ServerFriend? GetServerBot()
        {
            ServerFriend? Server;
            try
            {
                var configJson = File.ReadAllText("data/gameplay.json");
                Server = JsonSerializer.Deserialize<ServerFriend>(configJson);
            }
            catch
            {
                Server = new ServerFriend
                {
                    BotConfig = new Bot
                    {
                        Name = "Server",
                        Signature = "Server Bot",
                        HeadId = 1402,
                        PlayerId = 10000,
                        IsOnline = true,
                        LastOfflineTime = -1,
                        Level = 99
                    }
                };

            }

            return Server;
        }

    }

