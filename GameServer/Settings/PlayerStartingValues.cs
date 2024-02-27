using Protocol;



namespace GameServer.Settings;
internal class PlayerStartingValues
{
    public required int PlayerId { get; set; }
    public required string Name { get; set; }
    public required int PlayerLevel { get; set; }
    public required int Birthday { get; set; }
    public required int CurWorldLeve { get; set; }
    //public required int OriginWorldLevel { get; set; }
    public required int HeadPhoto { get; set; }
    public required int HeadFrame { get; set; }
    public required int[] Characters { get; set; }
    public required Vector Position { get; set; }
    public required string Signature { get; set; }




}


