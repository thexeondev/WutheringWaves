using GameServer.Controllers.Attributes;
using GameServer.Systems.Event;

namespace GameServer.Models;
internal class ModelManager
{
    private PlayerModel? _playerModel;
    private CreatureModel? _creatureModel;

    [GameEvent(GameEventType.Login)]
    public void OnLogin()
    {
        _playerModel = PlayerModel.CreateDefaultPlayer();
        _creatureModel = new CreatureModel(_playerModel.Id);
    }

    public PlayerModel Player => _playerModel ?? throw new InvalidOperationException($"Trying to access {nameof(PlayerModel)} instance before initialization!");

    public CreatureModel Creature => _creatureModel ?? throw new InvalidOperationException($"Trying to access {nameof(CreatureModel)} instance before initialization!");
}
