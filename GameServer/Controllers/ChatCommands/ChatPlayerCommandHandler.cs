using GameServer.Controllers.Attributes;
using GameServer.Extensions.Logic;
using GameServer.Models;
using GameServer.Models.Chat;
using GameServer.Network;
using GameServer.Systems.Entity;
using GameServer.Systems.Entity.Component;
using Protocol;
using GameServer.Controllers;
using GameServer.Systems.Notify;
using Core.Config;

namespace GameServer.Controllers.ChatCommands;

[ChatCommandCategory("player")]
internal class ChatPlayerCommandHandler
{
    private readonly ChatRoom _helperRoom;
    private readonly PlayerSession _session;
    private readonly CreatureController _creatureController;
    private readonly ModelManager _modelManager;
    private readonly RoleController _roleManager;
    private readonly IGameActionListener _listener;
    private readonly EntitySystem _entitySystem;
    private readonly ConfigManager _configManager;

    public ChatPlayerCommandHandler(ModelManager modelManager, PlayerSession session, CreatureController creatureController, RoleController roleManager, IGameActionListener listener, EntitySystem entitySystem, ConfigManager configManager)
    {
        _helperRoom = modelManager.Chat.GetBotChatRoom();
        _session = session;
        _creatureController = creatureController;
        _modelManager = modelManager;
        _roleManager = roleManager;
        _listener = listener;
        _entitySystem = entitySystem;
        _configManager = configManager;
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
    [ChatCommandDesc("/player tp [mapId] [x] [y] [z] - performing fast travel to specified position")]
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
           // switchmap()
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

    //Modify the current role's level  and refresh the role to make its attributes effective
    [ChatCommand("rolelv")]
    [ChatCommandDesc("/player rolelv  [Lv] - Modify the current role Lv (1~90)")]
    public void OnCurrentRolelv(string[] args)
    {
        if (args.Length != 1)
        {
            _helperRoom.AddCommandReply(0, $"Usage: /player rolelv  [Lv] (1~90)");
            return;
        }
        if (!int.TryParse(args[0], out int value))
        {
            _helperRoom.AddCommandReply(0, "Invalid value");
            return;
        }
        else if (value < 1 || value > 90)
        {
            _helperRoom.AddCommandReply(0, "Invalid value");
            return;
        }

        PlayerEntity? entity = _creatureController.GetPlayerEntity();
        if (entity == null)
        {
            _helperRoom.AddCommandReply(0, "[Debug]: can't find role entity");
            return;
        }


        roleInfo? role = _modelManager.Roles.GetRoleById(entity.ConfigId);

        if (role == null)
        {
            _helperRoom.AddCommandReply(0, "[Debug]: can't find role info");
            return;
        }
        role.BaseProp.Clear();
        role.BaseProp.AddRange(RoleController.CreateBasePropList(_configManager.GetConfig<BasePropertyConfig>(entity.ConfigId)));
        role.Level = value;
        if (value <= 20)
            role.Breakthrough = 0;
        else if (value > 20 && value <= 40)
            role.Breakthrough = 1;
        else if (value > 40 && value <= 50)
            role.Breakthrough = 2;
        else if (value > 50 && value <= 60)
            role.Breakthrough = 3;
        else if (value > 60 && value <= 70)
            role.Breakthrough = 4;
        else if (value > 70 && value <= 80)
            role.Breakthrough = 5;
        else if (value > 80 && value <= 90)
            role.Breakthrough = 6;
        else
        {
            _helperRoom.AddCommandReply(0, "Invalid value");
            return;
        }

        _roleManager.ApplyLvGrowthProperties(role);

        _listener.OnRolePropertiesUpdated(entity.ConfigId, role.BaseProp, role.AddProp);
        entity?.ChangeGameplayAttributes(role.GetAttributeList());

        if(entity!=null)
        _helperRoom.AddCommandReply(0, $"Successfully modified role: {entity.ConfigId}  LV to {value}");
    }


}
