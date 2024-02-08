using GameServer.Handlers.Attributes;
using GameServer.Network;
using Protocol;

namespace GameServer.Handlers;
internal class FriendHandler : MessageHandlerBase
{
    public FriendHandler(KcpSession session) : base(session)
    {
    }

    [MessageHandler(MessageId.FriendAllRequest)]
    public async Task OnFriendAllRequest(ReadOnlyMemory<byte> data)
    {
        FriendAllRequest request = FriendAllRequest.Parser.ParseFrom(data.Span);
        
        Console.WriteLine(request);

        await Session.Rpc.ReturnAsync(MessageId.FriendAllResponse, new FriendAllResponse
        {
            ErrorCode = ((int)ErrorCode.Success),
            FriendApplyList = {},
            FriendInfoList =
            {
                new FriendInfo()
                {
                    Info = new PlayerDetails()
                    {
                        Birthday = 0,
                        IsCanLobbyOnline = false,
                        IsOnline = true,
                        Level = 1,
                        Name = "Pepega",
                        PlayerId = 9999,
                        Signature = "PepegaClap",
                        OriginWorldLevel = 2,
                        LastOfflineTime = -1,
                        CardShowList = {1000},
                        CurCard = 1000,
                        CurWorldLevel = 1,
                        HeadFrameId = 1000,
                        HeadId = 1000,
                        LevelGap = 0,
                        RoleShowList = {},
                        TeamMemberCount = 0
                    },
                    Remark = "Pepega"
                }
            }
        });
    }
}
