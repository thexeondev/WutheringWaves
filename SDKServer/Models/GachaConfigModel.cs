using System.Text.Json;
using System.Text.Json.Serialization;


namespace SDKServer.Models
{
    public record GachaConfigModel
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int HelpGroup { get; set; }
        public required string SummaryTitle { get; set; }
        public required string SummaryDescribe { get; set; }
        public required string ThemeColor { get; set; }
        public string? ContentTexturePath { get; set; }
        public string? ContentTextureBgPath { get; set; }
        public required string UnderBgTexturePath { get; set; }
        public required string TagNotSelectedSpritePath { get; set; }
        public required string TagSelectedSpritePath { get; set; }
        public List<object> UpList { get; set; } = [];
        public List<int> ShowIdList { get; set; } = [];
    }
    public class GachaConfig
    {
        public static GachaConfigModel GetGachaConfig(string poolname)
        {
            try
            {
                var configJson = File.ReadAllText("data/gachaconfig.json");
                var config = JsonSerializer.Deserialize<Dictionary<string, GachaConfigModel>>(configJson)!;

                if (config.TryGetValue(poolname, out var gachaConfig))
                {
                    return gachaConfig;
                }
                else
                {
                    // Return a default GachaConfigModel if the specified poolname is not found
                    return new GachaConfigModel
                    {
                        Id = 0,
                        Type = 0,
                        HelpGroup = 0,
                        SummaryTitle = "Default Title",
                        SummaryDescribe = "Default Description",
                        ThemeColor = "Default Color",
                        ContentTexturePath = "Default Content Texture Path",
                        ContentTextureBgPath = "Default Content Texture Background Path",
                        UnderBgTexturePath = "Default Under Background Texture Path",
                        TagNotSelectedSpritePath = "Default Tag Not Selected Sprite Path",
                        TagSelectedSpritePath = "Default Tag Selected Sprite Path",
                        UpList = [],
                        ShowIdList = []
                    };
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., file not found, invalid JSON, etc.)
                Console.WriteLine($"Error reading or parsing gachaconfig file: {ex.Message}");
                return new GachaConfigModel
                {
                    Id = 0,
                    Type = 0,
                    HelpGroup = 0,
                    SummaryTitle = "Default Title",
                    SummaryDescribe = "Default Description",
                    ThemeColor = "Default Color",
                    ContentTexturePath = "Default Content Texture Path",
                    ContentTextureBgPath = "Default Content Texture Background Path",
                    UnderBgTexturePath = "Default Under Background Texture Path",
                    TagNotSelectedSpritePath = "Default Tag Not Selected Sprite Path",
                    TagSelectedSpritePath = "Default Tag Selected Sprite Path",
                    UpList = [],
                    ShowIdList = []
                };
            }
        }
    }


}