using Newtonsoft.Json.Linq;
using System.IO;

namespace GameServer.Settings;

public class DBManager
{
    public static void UpdateDB(string member, JToken value)
    {
        string jsonFilePath = "data/gameplay.json";
        string json = File.ReadAllText(jsonFilePath);
        JObject jsonObj = JObject.Parse(json);

        // 将成员名称拆分为父级和子级
        string[] parts = member.Split('.');
        string parent = parts[0];
        string? child = parts.Length > 1 ? string.Join(".", parts.Skip(1)) : null;

        if (child == null)
        {
            // 如果没有子级，按照以前的方式处理
            JToken? token = jsonObj.SelectToken(parent);
            if (token != null)
            {
                token.Replace(value);
            }
            else
            {
                jsonObj.Add(parent, value);
            }
        }
        else
        {
            // 如果有子级，请确保父级存在
            if (jsonObj[parent] is not JObject parentObj)
            {
                parentObj = [];
                jsonObj.Add(parent, parentObj);
            }
            // 现在添加或替换子级
            JToken? token = parentObj.SelectToken(child);
            if (token != null)
            {
                token.Replace(value);
            }
            else
            {
                parentObj.Add(child, value);
            }
        }

        File.WriteAllText(jsonFilePath, jsonObj.ToString());
    }



    public static JToken? GetMember(string jsonFilePath, string memberPath)
    {
        if (!File.Exists(jsonFilePath))
        {
            throw new FileNotFoundException($"The file {jsonFilePath} does not exist.");
        }

        string json = File.ReadAllText(jsonFilePath);
        JObject jsonObj = JObject.Parse(json);
        JToken? token = jsonObj.SelectToken(memberPath);
        return token;
    }


}

