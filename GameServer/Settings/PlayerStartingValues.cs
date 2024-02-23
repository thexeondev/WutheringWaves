using Protocol;

namespace GameServer.Settings;
internal class PlayerStartingValues
{
    public required string Name { get; set; }
    public required int PlayerLevel { get; set; }
    public required int HeadPhoto { get; set; }
    public required int HeadFrame { get; set; }
    public required int[] Characters { get; set; }
    public required Vector Position { get; set; }
}
