using GameServer.Handlers.Attributes;
using GameServer.Network;
using Protocol;

namespace GameServer.Handlers;
internal class MapHandler : MessageHandlerBase
{
    private int Hour { get; set; } = 21;
    private int Minute { get; set; } = 0;

    public MapHandler(KcpSession session) : base(session)
    {
    }

    [MessageHandler(MessageId.ExploreProgressRequest)]
    public async Task OnExploreProgressRequest(ReadOnlyMemory<byte> data)
    {
        ExploreProgressRequest request = ExploreProgressRequest.Parser.ParseFrom(data.Span);
        
        Console.WriteLine(request);

        ExploreProgressResponse response = new();

        foreach (int areaId in request.AreaIds)
        {
            response.AreaProgress.Add(new AreaExploreInfo()
            {
                AreaId = areaId,
                ExplorePercent = 100,
                ExploreProgress =
                    {
                        new OneExploreItem()
                        {
                            ExploreProgressId = 1,
                            ExplorePercent = 100
                        }
                    }
            });
        }

        await Session.Rpc.ReturnAsync(MessageId.ExploreProgressResponse, response);
    }

    [MessageHandler(MessageId.MapMarkRequest)]
    public async Task OnMapMarkRequest(ReadOnlyMemory<byte> data)
    {
        MapMarkRequest request = MapMarkRequest.Parser.ParseFrom(data.Span);
        
        Console.WriteLine(request);

        var configId = request.MarkPointRequestInfo.ConfigId;
        var mapId = request.MarkPointRequestInfo.MapId;
        var x = request.MarkPointRequestInfo.PosX;
        var y = request.MarkPointRequestInfo.PosY;
        var z = request.MarkPointRequestInfo.PosZ;

        await Session.PushMessage(MessageId.TeleportNotify, new TeleportNotify()
        {
            MapId = mapId,
            PosX = x,
            PosY = y,
            PosZ = z,
            Reason = (int)TeleportReason.ApiTeleport,
        });

        await Session.PushMessage(MessageId.PushDataCompleteNotify, new PushDataCompleteNotify());

        await Session.Rpc.ReturnAsync(MessageId.MapMarkResponse, new MapMarkResponse()
        {
            Info = new MarkPointInfo()
            {
                ConfigId = configId,
                MapId = mapId,
                PosX = x,
                PosY = y,
                PosZ = z,
                MarkId = new Random().Next(1, 999),
                MarkInfo = request.MarkPointRequestInfo.MarkInfo,
                MarkType = request.MarkPointRequestInfo.MarkType
            },
            ErrorCode = ((int)ErrorCode.Success)
        });
    }

    [MessageHandler(MessageId.MapOpenPush)]
    public async Task OnMapOpenPush(ReadOnlyMemory<byte> data)
    {
        MapOpenPush request = MapOpenPush.Parser.ParseFrom(data.Span);
        
        Console.WriteLine(request);

        await Session.PushMessage(MessageId.FuncOpenNotify, new FuncOpenNotify()
        {
            Func =
            {
                new Function()
                {
                    Id = 10058,
                    Flag = 2
                }
            }
        });

        await Session.PushMessage(MessageId.PushDataCompleteNotify, new PushDataCompleteNotify());
    }

    [MessageHandler(MessageId.TimeStopPush)]
    public async Task OnTimeStopPush(ReadOnlyMemory<byte> data)
    {
        TimeStopPush request = TimeStopPush.Parser.ParseFrom(data.Span);
        
        Console.WriteLine(request);

        Hour += (int)request.TimeDilation;

        await Session.PushMessage(MessageId.SyncSceneTimeNotify, new SyncSceneTimeNotify()
        {
            TimeInfo = new SceneTimeInfo()
            {
                Hour = Hour,
                Minute = Minute,
                OwnerTimeClockTimeSpan = 12
            }
        });

        await Session.PushMessage(MessageId.PushDataCompleteNotify, new PushDataCompleteNotify());
    }

    [MessageHandler(MessageId.TeleportFinishRequest)]
    public async Task OnTeleportFinishRequest(ReadOnlyMemory<byte> data)
    {
        TeleportFinishRequest request = TeleportFinishRequest.Parser.ParseFrom(data.Span);
        
        Console.WriteLine(request);

        await Session.Rpc.ReturnAsync(MessageId.TeleportFinishResponse, new TeleportFinishResponse
        {
            ErrorCode = ((int)ErrorCode.Success)
        });
    }
}
