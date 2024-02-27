using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Settings;
using Protocol;

namespace GameServer.Controllers;
internal class FriendSystemController : Controller
{
    public FriendSystemController(PlayerSession session) : base(session)
    {
        // FriendController.
    }
    public readonly ServerBot.ServerFriend bot = ServerBot.GetServerBot()!;


    [NetEvent(MessageId.FriendAllRequest)]
    public RpcResult OnFriendAllRequest() => Response(MessageId.FriendAllResponse, new FriendAllResponse
    {
        FriendInfoList = 
        {
            CreateDummyFriendInfo(bot.BotConfig.PlayerId, $"{bot.BotConfig.Name}", $"{bot.BotConfig.Signature}", bot.BotConfig.Level,bot.BotConfig.HeadId , bot.BotConfig.IsOnline)
        }
    });

    private static FriendInfo CreateDummyFriendInfo(int id, string name, string signature, int level,int headIconId,bool isOnline) => new()
    {
        Info = new()
        {
            PlayerId = id,
            Name = name,
            Signature = signature,
            Level = level,
            HeadId = headIconId,
            IsOnline = isOnline,
            LastOfflineTime = -1
        }
    };
}
