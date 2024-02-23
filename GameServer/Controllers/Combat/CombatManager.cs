using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Systems.Entity;
using GameServer.Systems.Entity.Component;
using Microsoft.Extensions.Logging;
using Protocol;

namespace GameServer.Controllers.Combat;
internal class CombatManager
{
    private delegate Task<CombatResponseData> CombatRequestHandler(CombatManager combatManager, CombatRequestContext context);
    private static readonly ImmutableDictionary<CombatRequestData.MessageOneofCase, CombatRequestHandler> s_requestHandlers;

    private readonly ILogger _logger;
    private readonly EntitySystem _entitySystem;
    private readonly PlayerSession _session;
    private readonly CreatureController _creatureController;

    static CombatManager()
    {
        s_requestHandlers = MapRequestHandlers();
    }

    public CombatManager(ILogger<CombatManager> logger, EntitySystem entitySystem, PlayerSession session, CreatureController creatureController)
    {
        _logger = logger;
        _entitySystem = entitySystem;
        _session = session;
        _creatureController = creatureController;
    }

    [CombatRequest(CombatRequestData.MessageOneofCase.CreateBulletRequest)]
    public CombatResponseData OnCreateBulletRequest(CombatRequestContext context)
    {
        return new CombatResponseData
        {
            CombatCommon = context.Request.CombatCommon,
            CreateBulletResponse = new()
        };
    }

    [CombatRequest(CombatRequestData.MessageOneofCase.DamageExecuteRequest)]
    public async Task<CombatResponseData> OnDamageExecuteRequest(CombatRequestContext context)
    {
        DamageExecuteRequest request = context.Request.DamageExecuteRequest;

        EntityBase? entity = _entitySystem.Get<EntityBase>(request.TargetEntityId);
        if (entity == null)
        {
            return new CombatResponseData
            {
                RequestId = context.Request.RequestId,
                CombatCommon = context.Request.CombatCommon,
                DamageExecuteResponse = new()
                {
                    ErrorCode = (int)ErrorCode.ErrThrowDamageReqEntityIsAlreadyDead
                }
            };
        }

        EntityAttributeComponent attr = entity.ComponentSystem.Get<EntityAttributeComponent>();
        attr.SetAttribute(EAttributeType.Life, (int)request.DamageId); // Pakchunk patch! (cur hp instead of damageid)

        if (request.DamageId <= 0 && entity.Type != EEntityType.Player) // Player death not implemented
        {
            _entitySystem.Destroy(entity);
            await _session.Push(MessageId.EntityRemoveNotify, new EntityRemoveNotify
            {
                IsRemove = true,
                RemoveInfos =
                {
                    new EntityRemoveInfo
                    {
                        EntityId = entity.Id,
                        Type = (int)ERemoveEntityType.RemoveTypeNormal
                    }
                }
            });
        }

        return new CombatResponseData
        {
            RequestId = context.Request.RequestId,
            CombatCommon = context.Request.CombatCommon,
            DamageExecuteResponse = new()
            {
                AttackerEntityId = request.AttackerEntityId,
                TargetEntityId = request.TargetEntityId,
                ShieldCoverDamage = 0,
                Damage = (int)request.DamageId,
                PartId = request.PartId
            }
        };
    }

    [CombatRequest(CombatRequestData.MessageOneofCase.HitRequest)]
    public CombatResponseData OnHitRequest(CombatRequestContext context)
    {
        return new CombatResponseData
        {
            RequestId = context.Request.RequestId,
            CombatCommon = context.Request.CombatCommon,
            HitResponse = new()
            {
                HitInfo = context.Request.HitRequest.HitInfo
            }
        };
    }


    [CombatRequest(CombatRequestData.MessageOneofCase.LogicStateInitRequest)]
    public CombatResponseData OnLogicStateInitRequest(CombatRequestContext context)
    {
        CombatResponseData rsp = new()
        {
            CombatCommon = context.Request.CombatCommon,
            RequestId = context.Request.RequestId,
            LogicStateInitResponse = new()
        };

        EntityBase? entity = _entitySystem.Get<EntityBase>(context.Request.CombatCommon.EntityId);
        if (entity == null) return rsp;

        EntityLogicStateComponent logicStateComponent = entity.ComponentSystem.Get<EntityLogicStateComponent>();
        logicStateComponent.States = [.. context.Request.LogicStateInitRequest.InitData.States];

        context.Notifies.Add(new CombatNotifyData
        {
            CombatCommon = context.Request.CombatCommon,
            LogicStateInitNotify = new()
            {
                CombatCommon = context.Request.CombatCommon,
                EntityId = entity.Id,
                InitData = logicStateComponent.Pb.LogicStateComponentPb
            }
        });

        return rsp;
    }

    [CombatRequest(CombatRequestData.MessageOneofCase.SwitchLogicStateRequest)]
    public CombatResponseData OnSwitchLogicStateRequest(CombatRequestContext context)
    {
        CombatResponseData rsp = new()
        {
            CombatCommon = context.Request.CombatCommon,
            RequestId = context.Request.RequestId,
            SwitchLogicStateResponse = new()
        };

        EntityBase? entity = _entitySystem.Get<EntityBase>(context.Request.CombatCommon.EntityId);
        if (entity == null)
        {
            rsp.SwitchLogicStateResponse.ErrorCode = (int)ErrorCode.ErrActionEntityNoExist;
            return rsp;
        }

        EntityLogicStateComponent logicStateComponent = entity.ComponentSystem.Get<EntityLogicStateComponent>();
        logicStateComponent.States = [.. context.Request.SwitchLogicStateRequest.States];

        context.Notifies.Add(new CombatNotifyData
        {
            CombatCommon = context.Request.CombatCommon,
            SwitchLogicStateNotify = new()
            {
                States = { logicStateComponent.States }
            }
        });

        return rsp;
    }

    [CombatRequest(CombatRequestData.MessageOneofCase.BattleStateChangeRequest)]
    public CombatResponseData OnBattleStateChangeRequest(CombatRequestContext context)
    {
        return new CombatResponseData
        {
            CombatCommon = context.Request.CombatCommon,
            RequestId = context.Request.RequestId,
            BattleStateChangeResponse = new()
        };
    }

    [CombatRequest(CombatRequestData.MessageOneofCase.ChangeStateConfirmRequest)]
    public CombatResponseData OnChangeStateConfirmRequest(CombatRequestContext context)
    {
        CombatResponseData rsp = new()
        {
            CombatCommon = context.Request.CombatCommon,
            RequestId = context.Request.RequestId,
            ChangeStateConfirmResponse = new()
            {
                Error = new()
            }
        };

        EntityBase? entity = _entitySystem.Get<EntityBase>(context.Request.CombatCommon.EntityId);
        if (entity == null)
        {
            rsp.ChangeStateConfirmResponse.Error.ErrorCode = (int)ErrorCode.ErrEntityIsNotAlive;
            return rsp;
        }

        ChangeStateConfirmRequest request = context.Request.ChangeStateConfirmRequest;
        if (entity.ComponentSystem.TryGet(out EntityFsmComponent? fsmComponent))
        {
            DFsm? dfsm = fsmComponent.Fsms.FirstOrDefault(fsm => fsm.FsmId == request.FsmId);
            dfsm ??= new()
            {
                FsmId = request.FsmId,
                Status = 1,
                Flag = (int)EFsmStateFlag.Confirmed
            };

            dfsm.CurrentState = request.State;
            context.Notifies.Add(new CombatNotifyData
            {
                CombatCommon = context.Request.CombatCommon,
                ChangeStateConfirmNotify = new ChangeStateConfirmNotify
                {
                    State = request.State,
                    FsmId = request.FsmId
                }
            });
        }

        rsp.ChangeStateConfirmResponse.State = context.Request.ChangeStateConfirmRequest.State;
        rsp.ChangeStateConfirmResponse.FsmId = context.Request.ChangeStateConfirmRequest.FsmId;

        return rsp;
    }

    [CombatRequest(CombatRequestData.MessageOneofCase.FsmConditionPassRequest)]
    public CombatResponseData OnFsmConditionPassRequest(CombatRequestContext context)
    {
        return new CombatResponseData
        {
            CombatCommon = context.Request.CombatCommon,
            RequestId = context.Request.RequestId,
            FsmConditionPassResponse = new()
            {
                FsmId = context.Request.FsmConditionPassRequest.FsmId,
                Error = new()
            }
        };
    }

    [CombatRequest(CombatRequestData.MessageOneofCase.AiInformationRequest)]
    public CombatResponseData OnAiInformationRequest(CombatRequestContext context)
    {
        // Currently like this, TODO!
        context.Notifies.Add(new CombatNotifyData
        {
            CombatCommon = new CombatCommon
            {
                EntityId = context.Request.CombatCommon.EntityId, // id of monster
            },
            AiHateNotify = new AiHateNotify
            {
                HateList =
                {
                    new AiHateEntity
                    {
                        EntityId = _creatureController.GetPlayerEntity()!.Id, // id of hated entity (the player)
                        HatredValue = 99999
                    }
                }
            }
        });

        return new CombatResponseData
        {
            CombatCommon = context.Request.CombatCommon,
            RequestId = context.Request.RequestId,
            AiInformationResponse = new() // ?? contains nothing
        };
    }

    public Task<CombatResponseData?> HandleRequest(CombatRequestContext context)
    {
        if (s_requestHandlers.TryGetValue(context.Request.MessageCase, out CombatRequestHandler? handler))
            return handler(this, context)!;

        _logger.LogWarning("Combat request not handled: {type}", context.Request.MessageCase);
        return Task.FromResult<CombatResponseData?>(null);
    }

    private static ImmutableDictionary<CombatRequestData.MessageOneofCase, CombatRequestHandler> MapRequestHandlers()
    {
        var builder = ImmutableDictionary.CreateBuilder<CombatRequestData.MessageOneofCase, CombatRequestHandler>();

        MethodInfo taskFromResultMethod = typeof(Task).GetMethod(nameof(Task.FromResult))!.MakeGenericMethod(typeof(CombatResponseData));

        foreach (MethodInfo method in typeof(CombatManager).GetMethods())
        {
            CombatRequestAttribute? attribute = method.GetCustomAttribute<CombatRequestAttribute>();
            if (attribute == null) continue;

            ParameterExpression combatManagerParam = Expression.Parameter(typeof(CombatManager));
            ParameterExpression contextParam = Expression.Parameter(typeof(CombatRequestContext));

            Expression call = Expression.Call(combatManagerParam, method, contextParam);
            if (method.ReturnType == typeof(CombatResponseData))
                call = Expression.Call(taskFromResultMethod, call);

            Expression<CombatRequestHandler> lambda = Expression.Lambda<CombatRequestHandler>(call, combatManagerParam, contextParam);
            builder.Add(attribute.MessageCase, lambda.Compile());
        }

        return builder.ToImmutable();
    }
}
