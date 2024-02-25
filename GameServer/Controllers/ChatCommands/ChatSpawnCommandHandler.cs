using Core.Config;
using GameServer.Controllers.Attributes;
using GameServer.Models;
using GameServer.Models.Chat;
using GameServer.Network;
using GameServer.Systems.Entity;

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
    [ChatCommandDesc("/spawn monster [id] ([x] [y] [z] | here) - spawns monster with specified id and coordinates")]
    public void OnSpawnMonsterCommand(string[] args)
    {
        if (args.Length == 2 && int.TryParse(args[0], out int levelEntityId1) && args[1] == "here")
        {
            PlayerEntity? entity = _creatureController.GetPlayerEntity();
            if (entity == null) return;

            _ = SpawnMonster(levelEntityId1, entity.Pos.X / 100, entity.Pos.Y / 100, entity.Pos.Z / 100);
            return;
        }

        if (args.Length != 4 ||
            !(int.TryParse(args[0], out int levelEntityId) &&
            float.TryParse(args[1], out float x) &&
            float.TryParse(args[2], out float y) &&
            float.TryParse(args[3], out float z)))
        {
            _helperRoom.AddMessage(1338, 0, "Usage: /spawn monster [id] ([x] [y] [z] | here)");
            return;
        }

        _ = SpawnMonster(levelEntityId, x, y, z);
    }

    private async Task SpawnMonster(int levelEntityId, float x, float y, float z)
    {
        MonsterEntity monster = _entityFactory.CreateMonster(levelEntityId);
        monster.Pos = new()
        {
            X = x * 100,
            Y = y * 100,
            Z = z * 100
        };

        monster.InitProps(_configManager.GetConfig<BasePropertyConfig>(600000100)!); // TODO: monster property config
        _entitySystem.Add([monster]);

        await _creatureController.UpdateAiHate();

        _helperRoom.AddMessage(1338, 0, $"Successfully spawned monster with id {levelEntityId} at ({x}, {y}, {z})");
    }
}
