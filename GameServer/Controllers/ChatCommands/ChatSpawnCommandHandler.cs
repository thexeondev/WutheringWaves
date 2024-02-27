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
        _helperRoom = modelManager.Chat.GetBotChatRoom();
        _entitySystem = entitySystem;
        _entityFactory = entityFactory;
        _session = session;
        _configManager = configManager;
        _creatureController = creatureController;
    }

    [ChatCommand("monster")]
    [ChatCommandDesc("/spawn monster [id] [x] [y] [z] - spawns monster with specified id and coordinates")]
    public async Task OnSpawnMonsterCommand(string[] args)
    {
        if (args.Length < 1 || !int.TryParse(args[0], out int levelEntityId))
        {
            _helperRoom.AddCommandReply( 0, "Usage: /spawn monster [id] [x] [y] [z]");
            return;
        }

        MonsterEntity monster = _entityFactory.CreateMonster(levelEntityId);
        if (monster == null)
        {
            _helperRoom.AddCommandReply( 0, $"Failed to create monster with id {levelEntityId}");
            return;
        }
        if (args.Length >= 4 && int.TryParse(args[1], out int x) && int.TryParse(args[2], out int y) && int.TryParse(args[3], out int z))
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
                _helperRoom.AddCommandReply( 0, "Failed to get player entity");
                return;
            }
        }

        monster.InitProps(_configManager.GetConfig<BasePropertyConfig>(600000100)!); // TODO: monster property config
        _entitySystem.Add([monster]);

        await _creatureController.UpdateAiHate();

        _helperRoom.AddCommandReply( 0, $"Successfully spawned monster with id {levelEntityId} at ({monster.Pos.X}, {monster.Pos.Y}, {monster.Pos.Z})");
    }
}


