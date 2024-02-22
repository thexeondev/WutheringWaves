using Protocol;
using GameServer.Settings;

namespace GameServer.Models;
internal class PlayerModel
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int[] Characters { get; private set; }
    public Vector Position { get; private set; }

    public PlayerModel()
    {
        Name = string.Empty;
        Characters = [];
        Position = new Vector();
    }

    public static PlayerModel CreateDefaultPlayer(PlayerStartingValues startingValues)
    {
        return new PlayerModel
        {
            Id = 1337,
            Name = startingValues.Name,
            Characters = startingValues.Characters,
            Position = startingValues.Position.Clone()
        };
    }
}
