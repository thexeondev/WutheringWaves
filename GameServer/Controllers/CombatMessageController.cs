using GameServer.Controllers.Attributes;
using GameServer.Controllers.Combat;
using GameServer.Network;
using GameServer.Network.Messages;
using Protocol;

namespace GameServer.Controllers;
internal class CombatMessageController : Controller
{
    public CombatMessageController(PlayerSession session) : base(session)
    {
        // CombatMessageController.
    }

    [NetEvent(MessageId.CombatSendPackRequest)] // TODO: CombatSendPackRequest is important
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

        await Session.Push(MessageId.CombatReceivePackNotify, combatPackNotify);
        return Response(MessageId.CombatSendPackResponse, new CombatSendPackResponse());
    }
}
