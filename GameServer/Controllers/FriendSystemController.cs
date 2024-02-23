using GameServer.Controllers.Attributes;
using GameServer.GetServerBot;
using GameServer.Network;
using GameServer.Network.Messages;
using Protocol;

namespace GameServer.Controllers;
internal class FriendSystemController : Controller
{
    public FriendSystemController(PlayerSession session) : base(session)
    {
        // FriendController.
    }
    public readonly GameServer.GetServerBot.ServerBot.ServerFriend bot = GameServer.GetServerBot.ServerBot.GetServerBot()!;


    [NetEvent(MessageId.FriendAllRequest)]
    public RpcResult OnFriendAllRequest() => Response(MessageId.FriendAllResponse, new FriendAllResponse
    {
        FriendInfoList = 
        {
            CreateDummyFriendInfo(1338, $"{bot.BotConfig.Name}", "discord.gg/reversedrooms", 1402)
        }
    });

    private static FriendInfo CreateDummyFriendInfo(int id, string name, string signature, int headIconId) => new()
    {
        Info = new()
        {
            PlayerId = id,
            Name = name,
            Signature = signature,
            Level = 5,
            HeadId = headIconId,
            IsOnline = true,
            LastOfflineTime = -1
        }
    };
}
