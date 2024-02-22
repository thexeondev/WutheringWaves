using Core.Config;
using GameServer.Controllers.Attributes;
using GameServer.Models;
using GameServer.Models.Chat;
using GameServer.Network;
using GameServer.Systems.Entity;
using Microsoft.Extensions.Logging;
using Protocol;

namespace GameServer.Controllers.ChatCommands;

[ChatCommandCategory("spawn")]
internal class ChatSpawnCommandHandler
{
    private readonly ChatRoom _helperRoom;
    private readonly EntitySystem _entitySystem;
    private readonly EntityFactory _entityFactory;
    private readonly PlayerSession _session;
    private readonly ConfigManager _configManager;
    private readonly CreatureController _creatureController;

    public ChatSpawnCommandHandler(ModelManager modelManager, EntitySystem entitySystem, EntityFactory entityFactory, PlayerSession session, ConfigManager configManager, CreatureController creatureController)
    {
        _helperRoom = modelManager.Chat.GetChatRoom(1338);
        _entitySystem = entitySystem;
        _entityFactory = entityFactory;
        _session = session;
        _configManager = configManager;
        _creatureController = creatureController;
    }

    [ChatCommand("monster")]
    [ChatCommandDesc("/spawn monster [id] [lv] [x] [y] [z] - spawns monster with specified id and coordinates")]
    public async Task OnSpawnMonsterCommand(string[] args)
    {
        if (args.Length < 1 || !int.TryParse(args[0], out int levelEntityId))
        {
            _helperRoom.AddMessage(1338, 0, "Usage: /spawn monster [id] [lv] [x] [y] [z]");
            return;
        }

        MonsterEntity monster = _entityFactory.CreateMonster(levelEntityId);

        if (monster == null)
        {
            _helperRoom.AddMessage(1338, 0, $"Failed to create monster with id {levelEntityId}");
            return;
        }

        if (args.Length >= 5 && int.TryParse(args[2], out int x) && int.TryParse(args[3], out int y) && int.TryParse(args[4], out int z))
        {
            monster.Pos.X = x * 100;
            monster.Pos.Y = y * 100;
            monster.Pos.Z = z * 100;
        }
        else
        {
            var playerEntity = _creatureController.GetPlayerEntity();
            if (playerEntity != null)
            {
                monster.Pos.X = playerEntity.Pos.X;
                monster.Pos.Y = playerEntity.Pos.Y;
                monster.Pos.Z = playerEntity.Pos.Z;
            }
            else
            {
                _helperRoom.AddMessage(1338, 0, "Failed to get player entity");
                return;
            }
        }

        _entitySystem.Create(monster);
        BasePropertyConfig thisbasePropertyConfig = _configManager.GetConfig<BasePropertyConfig>(600000100)!;

        if (thisbasePropertyConfig == null)
        {
            _helperRoom.AddMessage(1338, 0, "Failed to get base property config");
            return;
        }

        if (args.Length >= 2 && int.TryParse(args[1], out int lv))
        {
            thisbasePropertyConfig.Lv = lv;
        }

        monster.InitProps(thisbasePropertyConfig);
        thisbasePropertyConfig.Lv = 1;
        await _session.Push(MessageId.EntityAddNotify, new EntityAddNotify
        {
            IsAdd = true,
            EntityPbs = { monster.Pb }
        });

        await _creatureController.UpdateAiHate();

        _helperRoom.AddMessage(1338, 0, $"Successfully spawned monster with id {levelEntityId} lv:{thisbasePropertyConfig.Lv} at ({monster.Pos.X}, {monster.Pos.Y}, {monster.Pos.Z})");
    }

}
