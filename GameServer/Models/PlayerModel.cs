using Protocol;
using GameServer.Settings;

namespace GameServer.Models;
internal class PlayerModel
{
    private const int MaxPlayerLevel = 80;

    public int Id { get; private set; }
    public string Name { get; set; }
    public int Level { get; private set; }
    public int[] Characters { get; private set; }
    public Vector Position { get; private set; }

    public PlayerModel()
    {
        Name = string.Empty;
        Characters = [];
        Position = new Vector();
    }

    public void LevelUp()
    {
        if (Level == MaxPlayerLevel) return;
        Level++;
    }

    public static PlayerModel CreateDefaultPlayer(PlayerStartingValues startingValues)
    {
        return new PlayerModel
        {
            Id = 1337,
            Name = startingValues.Name,
            Level = startingValues.PlayerLevel,
            Characters = startingValues.Characters,
            Position = startingValues.Position.Clone()
        };
    }
}
