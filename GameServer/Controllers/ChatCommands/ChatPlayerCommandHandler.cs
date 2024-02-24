using Core.Config;
using GameServer.Controllers.Attributes;
using GameServer.Models;
using GameServer.Models.Chat;
using GameServer.Network;
using GameServer.Systems.Entity;
using Protocol;

namespace GameServer.Controllers.ChatCommands;

[ChatCommandCategory("player")]
internal class ChatPlayerCommandHandler
{
    private readonly ChatRoom _helperRoom;
    private readonly PlayerSession _session;
    private readonly CreatureController _creatureController;

    public ChatPlayerCommandHandler(ModelManager modelManager, PlayerSession session, CreatureController creatureController)
    {
        _helperRoom = modelManager.Chat.GetChatRoom(1338);
        _session = session;
        _creatureController = creatureController;
    }

    [ChatCommand("getpos")]
    [ChatCommandDesc("/player getpos - shows current player coordinates")]
    public void OnPlayerGetPosCommand(string[] _)
    {
        PlayerEntity? entity = _creatureController.GetPlayerEntity();
        if (entity == null) return;

        _helperRoom.AddMessage(1338, 0, $"Your current position: ({entity.Pos.X / 100}, {entity.Pos.Y / 100}, {entity.Pos.Z / 100})");
    }

    [ChatCommand("teleport")]
    [ChatCommandDesc("/player teleport [x] [y] [z] - performing fast travel to specified position")]
    public async Task OnPlayerTeleportCommand(string[] args)
    {
        if (args.Length != 3 || !float.TryParse(args[0], out float x)
            || !float.TryParse(args[1], out float y)
            || !float.TryParse(args[2], out float z))
        {
            _helperRoom.AddMessage(1338, 0, $"Usage: /player teleport [x] [y] [z]");
            return;
        }

        PlayerEntity? entity = _creatureController.GetPlayerEntity();

        if (entity != null)
        {
            await _session.Push(MessageId.TeleportNotify, new TeleportNotify
            {
                PosX = x * 100,
                PosY = y * 100,
                PosZ = z * 100,
                PosA = 0,
                MapId = 8,
                Reason = (int)TeleportReason.Gm,
                TransitionOption = new TransitionOptionPb
                {
                    TransitionType = (int)TransitionType.Empty
                }
            });
        }

        _helperRoom.AddMessage(1338, 0, $"Successfully performed fast travel to ({x}, {y}, {z})");
    }
}
