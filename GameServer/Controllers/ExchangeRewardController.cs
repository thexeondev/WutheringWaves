using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Network.Messages;
using Protocol;

namespace GameServer.Controllers;
internal class ExchangeRewardController : Controller
{
    public ExchangeRewardController(PlayerSession session) : base(session)
    {
        // ExchangeRewardMessageHandler.
    }

    [NetEvent(MessageId.ExchangeRewardInfoRequest)]
    public ResponseMessage OnExchangeRewardInfoRequest() => Response(MessageId.ExchangeRewardInfoResponse, new ExchangeRewardInfoResponse());
}
