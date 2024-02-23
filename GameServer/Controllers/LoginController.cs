using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Systems.Event;
using Protocol;

namespace GameServer.Controllers;
internal class LoginController : Controller
{
    public LoginController(PlayerSession session) : base(session)
    {
        // LoginController.
    }

    [NetEvent(MessageId.LoginRequest)]
    public async Task<RpcResult> OnLoginRequest(EventSystem eventSystem)
    {
        await eventSystem.Emit(GameEventType.Login);

        // Debug
        await eventSystem.Emit(GameEventType.DebugUnlockAllRoles);
        await eventSystem.Emit(GameEventType.DebugUnlockAllItems);

        return Response(MessageId.LoginResponse, new LoginResponse
        {
            Code = 0,
            Platform = "CBT3_EU",
            Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds()
        });
    }

    [NetEvent(MessageId.EnterGameRequest)]
    public RpcResult OnEnterGameRequest()
    {
        return Response(MessageId.EnterGameResponse, new EnterGameResponse())
                .AddPostEvent(GameEventType.EnterGame)
                .AddPostEvent(GameEventType.PushDataDone);
    }

    [GameEvent(GameEventType.PushDataDone)]
    public async Task OnPushDataDone()
    {
        await Session.Push(MessageId.PushDataCompleteNotify, new PushDataCompleteNotify());
    }

    [NetEvent(MessageId.HeartbeatRequest)]
    public RpcResult OnHeartbeatRequest() => Response(MessageId.HeartbeatResponse, new HeartbeatResponse());
}