using GameServer.Controllers.Attributes;
using GameServer.Controllers.Event;
using GameServer.Models;
using GameServer.Network;
using Protocol;

namespace GameServer.Controllers;
internal class PlayerInfoController : Controller
{
    public PlayerInfoController(PlayerSession session) : base(session)
    {
        // PlayerInfoController.
    }

    [GameEvent(GameEventType.EnterGame)]
    public async Task OnEnterGame(ModelManager modelManager)
    {
        PlayerModel player = modelManager.Player;

        await Session.Push(MessageId.BasicInfoNotify, new BasicInfoNotify
        {
            RandomSeed = 1337,
            Id = player.Id,
            Birthday = 0,
            Attributes =
            {
                new PlayerAttr
                {
                    Key = (int)PlayerAttrKey.Name,
                    ValueType = (int)PlayerAttrType.String,
                    StringValue = player.Name
                },
                new PlayerAttr
                {
                    Key = (int)PlayerAttrKey.Level,
                    ValueType = (int)PlayerAttrType.Int32,
                    Int32Value = 10
                }
            },
            RoleShowList =
            {
                new RoleShowEntry
                {
                    Level = 1,
                    RoleId = player.CharacterId
                }
            },
        });
    }
}
