using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Network.Messages;
using GameServer.Settings;
using GameServer.Systems.Entity;
using Microsoft.Extensions.Options;
using Protocol;

namespace GameServer.Controllers;
internal class WorldMapController : Controller
{
    public WorldMapController(PlayerSession session) : base(session)    
    {
        // WorldMapController.
    }

    [NetEvent(MessageId.MapTraceInfoRequest)]
    public RpcResult OnMapTraceInfoRequest() => Response(MessageId.MapTraceInfoResponse, new MapTraceInfoResponse()
    {
        // Don't.
        //MarkIdList = { 1, 2, 3, 1000, 1001, 1002, 1003, 1004, 1005, 1007, 1008, 1009, 3000, 3002, 3003, 3005, 3010, 3011, 3012, 4020, 4021, 4022, 4023, 5001, 5002, 5003, 5004, 5005, 5006, 5007, 5021, 5022, 5023, 5025, 5026, 5027, 5028, 5029, 5030, 5031, 380000, 380002, 380003, 380004, 380006, 380007, 380015, 301203, 380013, 380014, 301204, 300201, 300202, 300203, 300301, 300302, 300303, 300304, 300306, 300309, 300310, 300311, 300312, 300313, 300401, 300402, 300403, 300404, 300405, 300406, 300407, 300408, 300410, 300413, 300501, 300502, 300506, 300507, 300508, 300509, 300510, 300511, 300601, 300603, 300604, 300605, 300606, 300607, 300608, 300701, 300703, 300704, 300707, 300708, 300711, 300712, 300713, 300901, 300902, 300911, 300912, 300914, 300915, 300918, 301001, 301003, 301004, 301005, 301006, 301007, 301008, 301009, 301010, 301012, 301013, 301014, 301015, 10000, 10001, 10002, 10003, 10005, 10006, 300801, 301201, 300412, 3015, 3016, 3017, 300411 }
    });

    [NetEvent(MessageId.MapUnlockFieldInfoRequest)]
    public RpcResult OnMapUnlockFieldInfoRequest() => Response(MessageId.MapUnlockFieldInfoResponse, new MapUnlockFieldInfoResponse
    {
        FieldId = { Enumerable.Range(1, 12) }
    });

    [NetEvent(MessageId.MapMarkRequest)]
    public async Task<RpcResult> OnMapMarkRequest(MapMarkRequest request, IOptions<GameplayFeatureSettings> gameplayFeatures, CreatureController creatureController)
    {
        if (gameplayFeatures.Value.TeleportByMapMark)
        {
            PlayerEntity? entity = creatureController.GetPlayerEntity();

            if (entity != null)
            {
                await Session.Push(MessageId.TeleportNotify, new TeleportNotify
                {
                    PosX = request.MarkPointRequestInfo.PosX * 100,
                    PosY = request.MarkPointRequestInfo.PosY * 100,
                    PosZ = request.MarkPointRequestInfo.PosZ * 100,
                    PosA = 0,
                    MapId = 8,
                    Reason = (int)TeleportReason.Gm,
                    TransitionOption = new TransitionOptionPb
                    {
                        TransitionType = (int)TransitionType.Empty
                    }
                });
            }
        }

        return Response(MessageId.MapMarkResponse, new MapMarkResponse
        {
            Info = new MarkPointInfo
            {
                PosX = request.MarkPointRequestInfo.PosX,
                PosY = request.MarkPointRequestInfo.PosY,
                PosZ = request.MarkPointRequestInfo.PosZ,
                ConfigId = request.MarkPointRequestInfo.ConfigId,
                MapId = request.MarkPointRequestInfo.MapId,
                MarkId = 1,
                MarkInfo = request.MarkPointRequestInfo.MarkInfo,
                MarkType = request.MarkPointRequestInfo.MarkType
            }
        });
    }
}
