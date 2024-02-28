using Core.Config;
using GameServer.Controllers.Attributes;
using GameServer.Extensions.Logic;
using GameServer.Models;
using GameServer.Models.Chat;
using GameServer.Network;
using GameServer.Systems.Entity;
using GameServer.Systems.Entity.Component;
using Protocol;
using System.Threading;



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
    private readonly ModelManager _modelManager;

    public ChatSpawnCommandHandler(ModelManager modelManager, EntitySystem entitySystem, EntityFactory entityFactory, PlayerSession session, ConfigManager configManager, CreatureController creatureController)
    {
        _helperRoom = modelManager.Chat.GetBotChatRoom();
        _entitySystem = entitySystem;
        _entityFactory = entityFactory;
        _session = session;
        _configManager = configManager;
        _creatureController = creatureController;
        _modelManager = modelManager;
    }

    [ChatCommand("entity")]
    [ChatCommandDesc("/spawn entity [type] [id] [x] [y] [z] - spawns entity(monster,npc,animal) with specified id and coordinates")]
    public async Task OnSpawnEntityCommand(string[] args)
    {
        if (args.Length < 1 || !int.TryParse(args[1], out int levelEntityId) || args[0] == "")
        {
            _helperRoom.AddCommandReply(0, "Usage: /spawn entity [type] [id] [x] [y] [z]");
            return;
        }
        string entitytype = args[0];
        EntityBase? entity = null;
        switch (entitytype)
        {
            case "monster":
                {
                    MonsterEntity mentity = _entityFactory.CreateMonster(levelEntityId);
                    mentity.InitProps(_configManager.GetConfig<BasePropertyConfig>(600000100)!); // TODO: monster property config
                    entity = mentity;
                }
                break;
            case "npc":
                {
                    NpcEntity nentity = _entityFactory.CreateNpc(levelEntityId);
                    entity = nentity;
                }
                break;
            case "animal":
                {
                    AnimalEntity aentity = _entityFactory.CreateAnimal(levelEntityId);
                    entity = aentity;
                }
                break;
            case "role":
                {
                    PlayerEntity pentity = _entityFactory.CreatePlayer(levelEntityId,10001);
                    pentity.ComponentSystem.Get<EntityAttributeComponent>().SetAll(RoleInfoExtensions.GetAttributeList(_modelManager.Roles.GetRoleById(levelEntityId)!));
                    _creatureController.CreateConcomitants(pentity);
                    pentity.WeaponId = _modelManager.Inventory.GetEquippedWeapon(levelEntityId)?.Id ?? 0;
                    entity = pentity;
                }
                break;
            //case "item":
            //    {
            //        SceneItemEntity sentity = _entityFactory.CreateSceneItem(levelEntityId);
            //        entity = sentity;
            //    }
            //    break;
            default:
                {
                    _helperRoom.AddCommandReply(0, "Usage: /spawn entity [type] [id] [x] [y] [z]");
                    return;
                }
        }


        if (entity == null)
        {
            _helperRoom.AddCommandReply(0, $"Failed to create entity with id {levelEntityId}");
            return;
        }
        if (args.Length >= 4 && int.TryParse(args[1], out int x) && int.TryParse(args[2], out int y) && int.TryParse(args[3], out int z))
        {
            entity.Pos.X = x * 100;
            entity.Pos.Y = y * 100;
            entity.Pos.Z = z * 100;
        }
        else
        {

            var playerEntity = _creatureController.GetPlayerEntity();
            if (playerEntity != null)
            {
                entity.Pos.X = playerEntity.Pos.X;
                entity.Pos.Y = playerEntity.Pos.Y;
                entity.Pos.Z = playerEntity.Pos.Z;
            }
            else
            {
                _helperRoom.AddCommandReply(0, "Failed to get player entity");
                return;
            }
        }


        _entitySystem.Add([entity]);

        await _creatureController.UpdateAiHate();

        _helperRoom.AddCommandReply(0, $"Successfully spawned {entitytype} with id {levelEntityId} at ({entity.Pos.X}, {entity.Pos.Y}, {entity.Pos.Z})");
    }




}


