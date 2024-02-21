using GameServer.Controllers.Attributes;
using GameServer.Models;
using GameServer.Network;
using GameServer.Network.Messages;
using GameServer.Systems.Event;
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

        BasicInfoNotify basicInfo = new()
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
                },
                new PlayerAttr
                {
                    Key = (int)PlayerAttrKey.HeadPhoto,
                    ValueType = (int)PlayerAttrType.Int32,
                    Int32Value = 1402
                },
                new PlayerAttr
                {
                    Key = (int)PlayerAttrKey.Sex,
                    ValueType = (int)PlayerAttrType.Int32,
                    Int32Value = 1
                }
            },
            RoleShowList =
            {
                new RoleShowEntry
                {
                    Level = 1,
                    RoleId = 1501 // Rover
                }
            },
            CurCardId = 80060000,
            CardUnlockList =
            {
                new CardShowEntry
                {
                    CardId = 80060000,
                    IsRead = true
                }
            }
        };

        await Session.Push(MessageId.BasicInfoNotify, basicInfo);
    }

    [NetEvent(MessageId.PlayerBasicInfoGetRequest)]
    public RpcResult OnPlayerBasicInfoGetRequest()
    {
        return Response(MessageId.PlayerBasicInfoGetResponse, new PlayerBasicInfoGetResponse
        {
            Info = new PlayerDetails
            {
                Name = "Yangyang",
                Signature = "discord.gg/reversedrooms",
                HeadId = 1402,
                PlayerId = 1338,
                IsOnline = true,
                LastOfflineTime = -1,
                Level = 5
            }
        });
    }
}
