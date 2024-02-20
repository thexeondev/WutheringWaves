using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Network.Messages;
using Protocol;

namespace GameServer.Controllers;
internal class ExchangeRewardController : Controller
{
    public ExchangeRewardController(PlayerSession session) : base(session)
    {
        // ExchangeRewardController.
    }

    [NetEvent(MessageId.ExchangeRewardInfoRequest)]
    public RpcResult OnExchangeRewardInfoRequest() => Response(MessageId.ExchangeRewardInfoResponse, new ExchangeRewardInfoResponse());
}
