namespace GameServer.Models;
internal class ModelManager
{
    private PlayerModel? _playerModel;

    public void OnLogin()
    {
        _playerModel = PlayerModel.CreateDefaultPlayer();
    }

    public PlayerModel Player => _playerModel ?? throw new InvalidOperationException($"Trying to access {nameof(PlayerModel)} instance before initialization!");
}
