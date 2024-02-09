namespace GameServer.Models;
internal class PlayerModel
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int CharacterId { get; set; }

    public PlayerModel()
    {
        Name = string.Empty;
    }

    public static PlayerModel CreateDefaultPlayer()
    {
        return new PlayerModel
        {
            Id = 1337,
            Name = "ReversedRooms",
            CharacterId = 1601
        };
    }
}
