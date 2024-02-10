using GameServer.Controllers.Attributes;
using GameServer.Settings;
using GameServer.Systems.Event;
using Microsoft.Extensions.Options;

namespace GameServer.Models;
internal class ModelManager
{
    private readonly IOptions<PlayerStartingValues> _playerStartingValues;

    private PlayerModel? _playerModel;
    private CreatureModel? _creatureModel;

    public ModelManager(IOptions<PlayerStartingValues> playerStartingValues)
    {
        _playerStartingValues = playerStartingValues;
    }

    [GameEvent(GameEventType.Login)]
    public void OnLogin()
    {
        _playerModel = PlayerModel.CreateDefaultPlayer(_playerStartingValues.Value);
        _creatureModel = new CreatureModel(_playerModel.Id);
    }

    public PlayerModel Player => _playerModel ?? throw new InvalidOperationException($"Trying to access {nameof(PlayerModel)} instance before initialization!");

    public CreatureModel Creature => _creatureModel ?? throw new InvalidOperationException($"Trying to access {nameof(CreatureModel)} instance before initialization!");
}
