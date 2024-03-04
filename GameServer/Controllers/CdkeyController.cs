using Core.Config;
using GameServer.Controllers.Attributes;
using GameServer.Extensions.Logic;
using GameServer.Models;
using GameServer.Network;
using GameServer.Systems.Event;
using GameServer.Systems.Notify;
using Protocol;

namespace GameServer.Controllers;
internal class CdkeyController : Controller
{
    private readonly ModelManager _modelManager;
    private readonly ConfigManager _configManager;


    public CdkeyController(PlayerSession session, ConfigManager configManager, ModelManager modelManager) : base(session)
    {
        _modelManager = modelManager;
        _configManager = configManager;

    }



    [NetEvent(MessageId.CdKeyVerifyRequest)]
    public RpcResult OnCdKeyVerifyRequest(CdKeyVerifyRequest request)
    {
        if (request.CdKey != "")
        {
            //do something
            return Response(MessageId.CdKeyVerifyResponse, new CdKeyVerifyResponse
            {
                ErrorCode = (int)ErrorCode.Success
            });
        }
        else
            return Response(MessageId.CdKeyVerifyResponse, new CdKeyVerifyResponse
            {
                ErrorCode = (int)ErrorCode.ErrCdKeyNotFound
            });

    }



}

