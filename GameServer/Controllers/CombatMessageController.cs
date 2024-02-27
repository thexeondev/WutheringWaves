using GameServer.Controllers.Attributes;
using GameServer.Controllers.Combat;
using GameServer.Models;
using GameServer.Network;
using GameServer.Settings;
using GameServer.Systems.Entity;
using GameServer.Systems.Event;
using Microsoft.Extensions.Logging;
using Protocol;

namespace GameServer.Controllers;
internal class CombatMessageController : Controller
{
    private readonly ILogger _logger;

    public CombatMessageController(PlayerSession session, ILogger<CombatMessageController> logger) : base(session)
    {
        _logger = logger;
    }

    [NetEvent(MessageId.MovePackagePush)]
    public async Task OnMovePackagePush(MovePackagePush push, EntitySystem entitySystem, EventSystem eventSystem, ModelManager modelManager)
    {
        foreach (MovingEntityData movingEntityData in push.MovingEntities)
        {
            EntityBase? entity = entitySystem.Get<EntityBase>(movingEntityData.EntityId);
            if (entity == null)
            {
                _logger.LogWarning("OnMovePackagePush: moving entity not found! Id: {entityId}", movingEntityData.EntityId);
                continue;
            }

            MoveReplaySample lastMoveReplay = movingEntityData.MoveInfos.Last();
            entity.Pos.MergeFrom(lastMoveReplay.Location);
            entity.Rot.MergeFrom(lastMoveReplay.Rotation);

            if (entity.Id == modelManager.Creature.PlayerEntityId)
                await eventSystem.Emit(GameEventType.PlayerPositionChanged);
        }
    }

    [NetEvent(MessageId.CombatSendPackRequest)]
    public async Task<RpcResult> OnCombatSendPackRequest(CombatSendPackRequest request, CombatManager combatManager)
    {
        CombatReceivePackNotify combatPackNotify = new();

        foreach (CombatSendData sendData in request.Data)
        {
            if (sendData.Request != null)
            {
                CombatRequestContext context = new(sendData.Request);
                CombatResponseData? responseData = await combatManager.HandleRequest(context);

                combatPackNotify.Data.AddRange(context.Notifies.Select(notify => new CombatReceiveData
                {
                    CombatNotifyData = notify
                }));

                if (responseData != null)
                {
                    combatPackNotify.Data.Add(new CombatReceiveData
                    {
                        CombatResponseData = responseData
                    });
                }
            }
        }
        //if ((bool)DBManager.GetMember("Features.MultipleHits")!)
        //{
        //    int times = (int)DBManager.GetMember("Features.MultipleHits.Times")!;
        //    for (int i = 0; i < times; i++)
        //    {
        //        await Session.Push(MessageId.CombatReceivePackNotify, combatPackNotify);
        //    }
        //}
        //else
        //{
            await Session.Push(MessageId.CombatReceivePackNotify, combatPackNotify);
       // }
        
        return Response(MessageId.CombatSendPackResponse, new CombatSendPackResponse());
    }
}
