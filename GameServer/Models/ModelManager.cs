using Core.Config;
using GameServer.Controllers.Attributes;
using GameServer.Settings;
using GameServer.Systems.Event;
using Microsoft.Extensions.Options;

namespace GameServer.Models;
internal class ModelManager
{
    private readonly IOptions<PlayerStartingValues> _playerStartingValues;
    private readonly ConfigManager _configManager;

    private PlayerModel? _playerModel;
    private CreatureModel? _creatureModel;

    public ModelManager(IOptions<PlayerStartingValues> playerStartingValues, ConfigManager configManager)
    {
        _playerStartingValues = playerStartingValues;
        _configManager = configManager;
    }

    [GameEvent(GameEventType.Login)]
    public void OnLogin()
    {
        _playerModel = PlayerModel.CreateDefaultPlayer(_playerStartingValues.Value);
        _creatureModel = new CreatureModel(_playerModel.Id);

        Formation.Set(_playerStartingValues.Value.Characters);
    }

    public PlayerModel Player => _playerModel ?? throw new InvalidOperationException($"Trying to access {nameof(PlayerModel)} instance before initialization!");

    public CreatureModel Creature => _creatureModel ?? throw new InvalidOperationException($"Trying to access {nameof(CreatureModel)} instance before initialization!");

    public RoleModel Roles { get; } = new();
    public FormationModel Formation { get; } = new();
    public InventoryModel Inventory { get; } = new();
    public ChatModel Chat { get; } = new();
}
