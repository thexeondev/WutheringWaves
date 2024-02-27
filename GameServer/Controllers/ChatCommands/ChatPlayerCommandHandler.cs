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
        _helperRoom = modelManager.Chat.GetBotChatRoom();
        _session = session;
        _creatureController = creatureController;
    }

    [ChatCommand("getpos")]
    [ChatCommandDesc("/player getpos - shows current player coordinates")]
    public void OnPlayerGetPosCommand(string[] _)
    {
        PlayerEntity? entity = _creatureController.GetPlayerEntity();
        if (entity == null) return;

        var x = (float)Math.Round(entity.Pos.X / 100, 2);
        var y = (float)Math.Round(entity.Pos.Y / 100, 2);
        var z = (float)Math.Round(entity.Pos.Z / 100, 2);
        _helperRoom.AddCommandReply(0, $"Your current position: ({x}, {y}, {z})");
    }

    [ChatCommand("tp")]
    [ChatCommandDesc("/player tp [x] [y] [z] - performing fast travel to specified position")]
    public async Task OnPlayerTeleportCommand(string[] args)
    {
        if (args.Length != 4 || !float.TryParse(args[1], out float x)
            || !float.TryParse(args[2], out float y)
            || !float.TryParse(args[3], out float z) || !int.TryParse(args[0],out int map))
        {
            _helperRoom.AddCommandReply(0, $"Usage: /player tp [mapId] [x] [y] [z]");
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
                MapId = map,
                Reason = (int)TeleportReason.Gm,
                TransitionOption = new TransitionOptionPb
                {
                    TransitionType = (int)TransitionType.Empty
                }
            });
        }

        _helperRoom.AddCommandReply(0, $"Successfully performed fast travel to ({map},{x}, {y}, {z})");
    }
    //通过命令修改当前角色的属性数值，属性参考baseproperty.json中的属性，数值改成[value]的值，并且刷新角色使属性生效
    //[ChatCommand("property")]
    //[ChatCommandDesc("/player property [property] [value] - Modify the current character property ")]

    //public void OnPlayerPropertyCommand(string[] args)
    //{
    //    if (args.Length != 2)
    //    {
    //        _helperRoom.AddCommandReply(0, $"Usage: /player property [property] [value]");
    //        return;
    //    }
    //    if (!int.TryParse(args[1], out int value))
    //    {
    //        _helperRoom.AddCommandReply(0, "Invalid value");
    //        return;
    //    }
        //PlayerEntity? entity = _creatureController.GetPlayerEntity();
        //if (property == null) return;
        //switch (args[0])
        //{
        //    case "Lv":
        //        property.Lv = value;
        //        break;
        //    case "Hp":
        //        property.Life = value;
        //        property.LifeMax = value;
        //        break;
        //    case "Atk":
        //        property.Atk = value;
        //        break;
        //    case "Def":
        //        property.Def = value;
        //        break;
        //    case "Cd":
        //        property.CdReduse = value;
        //        break;
        //    case "SpeedRatio":
        //        property.SpeedRatio = value;
        //        break;
        //    case "AutoAttackSpeed":
        //        property.AutoAttackSpeed = value;
        //        break;
        //    default:
        //        _helperRoom.AddCommandReply(0, "Error: Invalid property");
        //        return;
        //}
        //entity.RefreshProperties();
    //    _helperRoom.AddCommandReply(0, $"Successfully modified property {args[0]} to {value}");
    //}


}
