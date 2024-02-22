using GameServer.Controllers.Attributes;
using GameServer.Models;
using GameServer.Network;
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
            Birthday = player.BirthDay,
            Attributes = { player.Attributes },
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

    [NetEvent(MessageId.ChangeHeadPhotoRequest)]
    public RpcResult OnChangeHeadPhotoRequest(ChangeHeadPhotoRequest request, ModelManager modelManager)
    {
        modelManager.Player.SetAttribute(PlayerAttrKey.HeadPhoto, request.HeadPhotoId);

        return Response(MessageId.ChangeHeadPhotoResponse, new ChangeHeadPhotoResponse
        {
            HeadPhotoId = request.HeadPhotoId
        });
    }

    [NetEvent(MessageId.BirthdayInitRequest)]
    public RpcResult OnBirthdayInitRequest(BirthdayInitRequest request, ModelManager modelManager)
    {
        modelManager.Player.BirthDay = request.Birthday;

        return Response(MessageId.BirthdayInitResponse, new BirthdayInitResponse());
    }

    [NetEvent(MessageId.ModifySignatureRequest)]
    public RpcResult OnModifySignatureRequest(ModifySignatureRequest request, ModelManager modelManager)
    {
        modelManager.Player.SetAttribute(PlayerAttrKey.Sign, request.Signature);

        return Response(MessageId.ModifySignatureResponse, new ModifySignatureResponse
        {
            Signature = request.Signature
        });
    }

    [NetEvent(MessageId.ModifyNameRequest)]
    public RpcResult OnModifyNameRequest(ModifyNameRequest request, ModelManager modelManager)
    {
        modelManager.Player.SetAttribute(PlayerAttrKey.Name, request.Name);

        return Response(MessageId.ModifyNameResponse, new ModifyNameResponse
        {
            Name = request.Name
        });
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
