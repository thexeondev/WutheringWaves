using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Network.Messages;
using GameServer.Systems.Event;
using Microsoft.Extensions.Logging;
using Protocol;

namespace GameServer.Controllers;
internal class LoginController : Controller
{
    public LoginController(PlayerSession session) : base(session)
    {
        // LoginController.
    }

    [NetEvent(MessageId.LoginRequest)]
    public async Task<ResponseMessage> OnLoginRequest(EventSystem eventSystem)
    {
        await eventSystem.Emit(GameEventType.Login);

        return Response(MessageId.LoginResponse, new LoginResponse
        {
            Code = 0,
            Platform = "CBT3_EU",
            Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds()
        });
    }

    [NetEvent(MessageId.EnterGameRequest)]
    public async Task<ResponseMessage> OnEnterGameRequest(EnterGameRequest request, ILogger<LoginController> logger, EventSystem eventSystem)
    {
        logger.LogInformation("Enter Game Request:\n{req}", request);

        await eventSystem.Emit(GameEventType.EnterGame);
        await Session.Push(MessageId.PushDataCompleteNotify, new PushDataCompleteNotify());

        return Response(MessageId.EnterGameResponse, new EnterGameResponse());
    }

    [NetEvent(MessageId.HeartbeatRequest)]
    public ResponseMessage OnHeartbeatRequest() => Response(MessageId.HeartbeatResponse, new HeartbeatResponse());
}