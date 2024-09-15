﻿using GameServer.Controllers.Attributes;
using GameServer.Models;
using GameServer.Network;
using GameServer.Settings;
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
            Attributes = { player.Attributes },
            RoleShowList =
            {
                new RoleShowEntry
                {
                    Level = 90,
                    RoleId = 1501 // Rover
                }
            },
            CurCardId = 80060000,
            CardUnlockList =
        {
            new CardShowEntry { CardId = 80060000, IsRead = true },
            new CardShowEntry { CardId = 80060001, IsRead = true },
            new CardShowEntry { CardId = 80060002, IsRead = true },
            new CardShowEntry { CardId = 80060003, IsRead = true },
            new CardShowEntry { CardId = 80060004, IsRead = true },
            new CardShowEntry { CardId = 80060005, IsRead = true },
            new CardShowEntry { CardId = 80060006, IsRead = true },
            new CardShowEntry { CardId = 80060007, IsRead = true },
            new CardShowEntry { CardId = 80060008, IsRead = true },
            new CardShowEntry { CardId = 80060009, IsRead = true },
            new CardShowEntry { CardId = 80060010, IsRead = true },
            new CardShowEntry { CardId = 80060011, IsRead = true },
            new CardShowEntry { CardId = 80060012, IsRead = true },
            new CardShowEntry { CardId = 80060013, IsRead = true },
            new CardShowEntry { CardId = 80060014, IsRead = true },
            new CardShowEntry { CardId = 80060015, IsRead = true },
            new CardShowEntry { CardId = 80060016, IsRead = true },
            new CardShowEntry { CardId = 80069001, IsRead = true }
        }
        };

        await Session.Push(MessageId.BasicInfoNotify, basicInfo);
    }

    [NetEvent(MessageId.ChangeHeadPhotoRequest)]
    public RpcResult OnChangeHeadPhotoRequest(ChangeHeadPhotoRequest request, ModelManager modelManager)
    {
        modelManager.Player.SetAttribute(PlayerAttrKey.HeadPhoto, request.HeadPhotoId);
        DBManager.UpdateDB("StartingValues.HeadPhoto", request.HeadPhotoId);
        return Response(MessageId.ChangeHeadPhotoResponse, new ChangeHeadPhotoResponse
        {
            HeadPhotoId = request.HeadPhotoId
        });
    }
    [NetEvent(MessageId.ChangeCardRequest)]
    public async Task<RpcResult> OnChangeCardRequest(ChangeCardRequest request, ModelManager modelManager)
    {
        modelManager.Player.SetAttribute(PlayerAttrKey.HeadFrame, request.CardId);
        DBManager.UpdateDB("StartingValues.HeadFrame", request.CardId);
        await Session.Push(MessageId.UnlockHeadFrameNotify, new UnlockHeadFrameNotify
        {
            HeadFrameId = request.CardId
        });
        return Response(MessageId.ChangeCardResponse, new ChangeCardResponse
        {
          ErrorCode = 0,
        });
    }

    [NetEvent(MessageId.BirthdayInitRequest)]
    public RpcResult OnBirthdayInitRequest(BirthdayInitRequest request, ModelManager modelManager)
    {
        modelManager.Player.Birthday = request.Birthday;
        DBManager.UpdateDB("StartingValues.Birthday", request.Birthday);

        return Response(MessageId.BirthdayInitResponse, new BirthdayInitResponse());
    }

    [NetEvent(MessageId.ModifySignatureRequest)]
    public RpcResult OnModifySignatureRequest(ModifySignatureRequest request, ModelManager modelManager)
    {
        modelManager.Player.SetAttribute(PlayerAttrKey.Sign, request.Signature);
        DBManager.UpdateDB("StartingValues.Signature", request.Signature);
        return Response(MessageId.ModifySignatureResponse, new ModifySignatureResponse
        {
            Signature = request.Signature
        });
    }

    [NetEvent(MessageId.ModifyNameRequest)]
    public RpcResult OnModifyNameRequest(ModifyNameRequest request, ModelManager modelManager)
    {
        modelManager.Player.SetAttribute(PlayerAttrKey.Name, request.Name);
        DBManager.UpdateDB("StartingValues.Name", request.Name);
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
