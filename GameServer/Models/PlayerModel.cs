using GameServer.Settings;
using Protocol;

namespace GameServer.Models;
internal class PlayerModel
{
    private const int MaxPlayerLevel = 80;

    public List<PlayerAttr> Attributes { get; } = [];

    public int Id { get; private set; }
    public int BirthDay { get; set; }
    public int[] Characters { get; private set; }
    public Vector Position { get; private set; }

    public string Name => GetStringAttribute(PlayerAttrKey.Name);

    public PlayerModel()
    {
        Characters = [];
        Position = new Vector();
    }

    public void SetAttribute(PlayerAttrKey key, int value)
    {
        SetAttribute(key, PlayerAttrType.Int32, value, null);
    }

    public void SetAttribute(PlayerAttrKey key, string value)
    {
        SetAttribute(key, PlayerAttrType.String, 0, value);
    }

    public int GetIntAttribute(PlayerAttrKey key)
    {
        return GetAttributeOfType(key, PlayerAttrType.Int32)?.Int32Value ?? 0;
    }

    public string GetStringAttribute(PlayerAttrKey key)
    {
        return GetAttributeOfType(key, PlayerAttrType.String)?.StringValue ?? string.Empty;
    }

    private void SetAttribute(PlayerAttrKey key, PlayerAttrType type, int intValue, string? stringValue)
    {
        PlayerAttr? attr = GetAttributeOfType(key, type);
        if (attr == null)
        {
            attr = new PlayerAttr
            {
                Key = (int)key,
                ValueType = (int)type,
            };

            Attributes.Add(attr);
        }

        switch (type)
        {
            case PlayerAttrType.Int32:
                attr.Int32Value = intValue; break;
            case PlayerAttrType.String:
                attr.StringValue = stringValue; break;
        }
    }

    private PlayerAttr? GetAttributeOfType(PlayerAttrKey key, PlayerAttrType type)
    {
        PlayerAttr? attr = Attributes.SingleOrDefault(attr => attr.Key == (int)key);
        if (attr != null)
        {
            if (attr.ValueType != (int)type)
                throw new ArgumentException($"PlayerAttr type mismatch! Key: {key}, type: {(PlayerAttrType)attr.ValueType}, argument type: {type}");
        }

        return attr;
    }

    public void LevelUp()
    {
        int level = GetIntAttribute(PlayerAttrKey.Level);

        if (level == MaxPlayerLevel) return;
        SetAttribute(PlayerAttrKey.Level, level + 1);
    }

    public static PlayerModel CreateDefaultPlayer(PlayerStartingValues startingValues)
    {
        PlayerModel playerModel = new()
        {
            Id = 1337,
            Characters = startingValues.Characters,
            Position = startingValues.Position.Clone()
        };

        playerModel.SetAttribute(PlayerAttrKey.Name, startingValues.Name);
        playerModel.SetAttribute(PlayerAttrKey.Level, startingValues.PlayerLevel);
        playerModel.SetAttribute(PlayerAttrKey.HeadPhoto, startingValues.HeadPhoto);
        playerModel.SetAttribute(PlayerAttrKey.HeadFrame, startingValues.HeadFrame);

        return playerModel;
    }
}
