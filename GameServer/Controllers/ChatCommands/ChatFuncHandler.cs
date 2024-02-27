﻿using GameServer.Controllers.Attributes;
using GameServer.Models;
using GameServer.Models.Chat;
using GameServer.Network;
using GameServer.Settings;
namespace GameServer.Controllers.ChatCommands;

[ChatCommandCategory("func")]
internal class ChatFuncCommandHandler
{
    private readonly ChatRoom _helperRoom;



    public ChatFuncCommandHandler(ModelManager modelManager)
    {
        _helperRoom = modelManager.Chat.GetBotChatRoom();
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
            _helperRoom.AddCommandReply(0, $"UnlimitedEnergy is now on");
        }
        else if (args[0] == "off")
        {
            DBManager.UpdateDB("Features.UnlimitedEnergy", false);
            _helperRoom.AddCommandReply(0, $"func UnlimitedEnergy is now off");
        }
        else
        {
            _helperRoom.AddCommandReply(0, $"Usage: /func unlimitedenergy [on/off]");
        }
    }

    [ChatCommand("multiplehits")]
    [ChatCommandDesc("/func multiplehits [on/off] [times]- Attack triggers multiple effects toggle")]
    //CombatSendPackResponse好像不是这个
    public void MultipleHitsToggleCommand(string[] args)
    {
        if (args.Length < 1|| args.Length>2)
        {
            _helperRoom.AddCommandReply(0, $"Usage: /multiplehits [on/off] [times]");
            return;
        }
        if (args[0] == "on" && int.TryParse(args[1], out int times) )
        {
            if(times>0)
            {
                DBManager.UpdateDB("Features.MultipleHits", true); 
                DBManager.UpdateDB("Features.MultipleHitsTimes", times); 
                _helperRoom.AddCommandReply(0, $"MultipleHits is now on with "+$"{times}" +" times");
            }
            else
            {
                _helperRoom.AddCommandReply(0, $"Usage: /multiplehits [on/off] [times]");
            }
        }
        else if (args[0] == "off")
        {
            DBManager.UpdateDB("Features.MultipleHits", false); 
            _helperRoom.AddCommandReply(0, $"MultipleHits is now off");
        }
        else
        {
            _helperRoom.AddCommandReply(0, $"Usage: /func multiplehits [on/off] [times]");
        }
    }


}
