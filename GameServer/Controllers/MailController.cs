using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Systems.Event;
using Google.Protobuf.Collections;
using Protocol;
using System.Net.Mail;

namespace GameServer.Controllers;
internal class MailController : Controller
{
    public MailController(PlayerSession session) : base(session)
    {
        //    MailAddNotify = 6102,
        //MailDeleteNotify = 6101,
        //MailDeleteRequest = 6107,
        //MailDeleteResponse = 6108,
        //MailGetAttachmentRequest = 6105,
        //MailGetAttachmentResponse = 6106,
        //MailInfosNotify = 6100,
        //MailReadRequest = 6103,
        //MailReadResponse = 6104,
        // MailController.
    }

    [GameEvent(GameEventType.EnterGame)]
    public async Task OnEnterGame()
    {
        MailAddNotify notify = new()
        {

            NewMail = new PbMailInfo
            {
                Id = "Reverse Room",
                ReceivedTime = 0,
                ReadTime = 0,
                State = 1,
                Level = 1,
                Title = "Welcome To Reverse Room !",
                Content = "Welcome to Reverse Room, the best place to play and have fun !",
                Sender = "WW Server",
                ValidTime = Int32.MaxValue,
                ReadValidTime = Int32.MaxValue,
                Attachments = {new  PbMailAttachment
                {
                    //Id = 1,
                    //Count = 5
                } },
                //ConfigId = 1402,

            },
            Reason = (int)MailAddReason.Gm

        };

    await Session.Push(MessageId.MailAddNotify, notify);
}

[NetEvent(MessageId.MailReadRequest)]
public RpcResult OnMailReadRequest(MailReadRequest request)
{
    // id_ = other.id_;

    return Response(MessageId.MailReadResponse, new MailReadResponse
    {

        Id = request.Id,
        State = 1,
        ErrorCode = 0

    });
}

[NetEvent(MessageId.MailDeleteRequest)]
public RpcResult OnMailDeleteRequest(/*MailDeleteRequest request*/) => Response(MessageId.MailDeleteResponse, new MailDeleteResponse());


[NetEvent(MessageId.MailGetAttachmentRequest)]
public RpcResult OnMailGetAttachmentRequest(MailGetAttachmentRequest request)
{
    int count = request.MailIds.Count;
    //  request.MailIds[];
    if (count != 0)
    {
        MapField<string, int> mailmap = [];
        for (int i = 0; i < count; i++)
        {
            mailmap.Add(request.MailIds[i], i);
        }
        return Response(MessageId.MailGetAttachmentResponse, new MailGetAttachmentResponse
        {
            //    successIdMap_ = other.successIdMap_.Clone();
            //errorCode_ = other.errorCode_;
            ErrorCode = 0,
            SuccessIdMap = { mailmap }
        });

    }
    else
    {
        return Response(MessageId.MailGetAttachmentResponse, new MailGetAttachmentResponse
        {
            //    successIdMap_ = other.successIdMap_.Clone();
            //errorCode_ = other.errorCode_;
            ErrorCode = (int)ErrorCode.ErrNoMailCanGet,
            SuccessIdMap = { }
        });
    }

}


}
