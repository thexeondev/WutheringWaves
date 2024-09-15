﻿using GameServer.Controllers.Attributes;
using GameServer.Models;
using GameServer.Models.Chat;
using GameServer.Network;
using GameServer.Settings;
using GameServer.Systems.Entity;
using GameServer.Systems.Event;
using Protocol;
namespace GameServer.Controllers.ChatCommands;

[ChatCommandCategory("func")]
internal class ChatFuncCommandHandler
{
    private readonly ChatRoom _helperRoom;
    private readonly PlayerSession _session;
    private readonly CreatureController _creatureController;



    public ChatFuncCommandHandler(ModelManager modelManager, PlayerSession session, CreatureController creatureController)
    {
        _helperRoom = modelManager.Chat.GetBotChatRoom();
        _session = session;
        _creatureController = creatureController;
    }

    [ChatCommand("mapmarktp")]
    [ChatCommandDesc("/func mapmarktp [on/off]- TeleportByMapMark toggle")]
    public void MapMarkTPtoggleCommand(string[] args)
    {
        if (args.Length < 1)
        {
            _helperRoom.AddCommandReply(0, $"Usage: /func mapmarktp [on/off]");
            return;
        }

        if (args[0] == "on")
        {
            DBManager.UpdateDB("Features.TeleportByMapMark", true); 
            _helperRoom.AddCommandReply(0, $"Usage: /MapMarkTP is now on");
        }
        else if (args[0] == "off")
        {
            DBManager.UpdateDB("Features.TeleportByMapMark", false); 
            _helperRoom.AddCommandReply(0, $"Usage: /func MapMarkTP is now off");
        }
        else
        {
            _helperRoom.AddCommandReply(0, $"Usage: /func mapmarktp [on/off]");
        }
    }

    [ChatCommand("unlimitedenergy")]
    [ChatCommandDesc("/func unlimitedenergy [on/off]- UnlimitedEnergy toggle")]
    public void UnlimitedEnergytoggleCommand(string[] args)
    {
        if (args.Length < 1)
        {
            _helperRoom.AddCommandReply(0, $"Usage: /func unlimitedenergy [on/off]");
            return;
        }

        if (args[0] == "on")
        {
            DBManager.UpdateDB("Features.UnlimitedEnergy", true);
            //_creatureController.CreateTeamPlayerEntities();
            _helperRoom.AddCommandReply(0, $"UnlimitedEnergy is now on");
        }
        else if (args[0] == "off")
        {
            DBManager.UpdateDB("Features.UnlimitedEnergy", false);
           // _creatureController.CreateTeamPlayerEntities();
            _helperRoom.AddCommandReply(0, $"func UnlimitedEnergy is now off");
        }
        else
        {
            _helperRoom.AddCommandReply(0, $"Usage: /func unlimitedenergy [on/off]");
        }
    }

    [ChatCommand("unlimitedcd")]
    [ChatCommandDesc("/func unlimitedcd [on/off]- UnlimitedCD toggle")]
    public void UnlimitedCDtoggleCommand(string[] args)
    {
        if (args.Length < 1)
        {
            _helperRoom.AddCommandReply(0, $"Usage: /func unlimitedcd [on/off]");
            return;
        }

        if (args[0] == "on")
        {
            DBManager.UpdateDB("Features.UnlimitedCD", true);
            
            _helperRoom.AddCommandReply(0, $"UnlimitedCD is now on");
        }
        else if (args[0] == "off")
        {
            DBManager.UpdateDB("Features.UnlimitedCD", false);
            //_creatureController.CreateTeamPlayerEntities();
            _helperRoom.AddCommandReply(0, $"func UnlimitedCD is now off");
        }
        else
        {
            _helperRoom.AddCommandReply(0, $"Usage: /func unlimitedcd [on/off]");
        }
    }

}
