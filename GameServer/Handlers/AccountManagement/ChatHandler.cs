using GameServer.Handlers.Attributes;
using GameServer.Network;
using Google.Protobuf.WellKnownTypes;
using Protocol;

namespace GameServer.Handlers;
internal class ChatHandler : MessageHandlerBase
{
    public ChatHandler(KcpSession session) : base(session)
    {
    }

    /*[MessageHandler(MessageId.PrivateChatHistoryRequest)]
    public async Task OnPrivateChatHistoryRequest(ReadOnlyMemory<byte> data)
    {
        PrivateChatHistoryRequest request = PrivateChatHistoryRequest.Parser.ParseFrom(data.Span);

        Console.WriteLine(request);

        await Session.Rpc.ReturnAsync(MessageId.PrivateChatHistoryResponse, new PrivateChatHistoryResponse
        {
            Data =
            {
                TargetUid = request.TargetUid,
                HistoryIsEnd = false,
                TotalNums = 1,
                Chats =
                {
                    new ChatContentProto() {
                        ChatContentType = ((int)ChatContentType.Text),
                        Content = "Welcome to Pepega Land",
                        MsgId = "00001",
                        OfflineMsg = false,
                        SenderUid = 9999,
                        UtcTime = DateTimeOffset.Now.ToUnixTimeSeconds()
                    }
                }
            },
            ErrorCode = ((int)ErrorCode.Success)
        });
    }*/

    [MessageHandler(MessageId.PrivateChatDataRequest)]
    public async Task OnPrivateChatDataRequest(ReadOnlyMemory<byte> data)
    {
        PrivateChatDataRequest request = PrivateChatDataRequest.Parser.ParseFrom(data.Span);
        
        Console.WriteLine(request);

        await Session.Rpc.ReturnAsync(MessageId.PrivateChatDataResponse, new PrivateChatDataResponse
        {
            ErrorCode = ((int)ErrorCode.Success)
        });
    }

    [MessageHandler(MessageId.PrivateChatRequest)]
    public async Task OnPrivateChatRequest(ReadOnlyMemory<byte> data)
    {
        PrivateChatRequest request = PrivateChatRequest.Parser.ParseFrom(data.Span);

        Console.WriteLine(request);

        if (request.ChatContentType == (int)ChatContentType.Text)
        {
            var content = request.Content;
            if (content.StartsWith("tp"))
            {
                var args = content.Split(" ");
                if (args.Length >= 4 &&
                    int.TryParse(args[1], out int posX) &&
                    int.TryParse(args[2], out int posY) &&
                    int.TryParse(args[3], out int posZ)) {
                    ReplyMessage("Teleported!");
                    PushTeleport(0, posX, posY, posZ);
                } else
                {
                    ReplyMessage("Missing arguments!");
                }
            } else
            {
                ReplyMessage("Unknown command!");
            }
        }

        await Session.PushMessage(MessageId.PushDataCompleteNotify, new PushDataCompleteNotify());

        await Session.Rpc.ReturnAsync(MessageId.PrivateChatResponse, new PrivateChatResponse
        {
            TargetUid = request.TargetUid,
            ErrorCode = ((int)ErrorCode.Success),
            MsgId = "MSG" + new Random().Next(1, 99999) + "ORIG",
            FilterMsg = ""
        });
    }

    private async void ReplyMessage(string message)
    {
        await Session.PushMessage(MessageId.PrivateMessageNotify, new PrivateMessageNotify()
        {
            ChatContent = new ChatContentProto()
            {
                ChatContentType = ((int)ChatContentType.Text),
                Content = message,
                MsgId = "MSG" + new Random().Next(1, 99999) + "REPL",
                OfflineMsg = false,
                SenderUid = 9999,
                UtcTime = DateTimeOffset.Now.ToUnixTimeSeconds()
            }
        });
    }

    private async void PushTeleport(int mapId, int posX, int posY, int posZ)
    {
        await Session.PushMessage(MessageId.TeleportNotify, new TeleportNotify()
        {
            MapId = mapId,
            PosX = posX,
            PosY = posY,
            PosZ = posZ,
            Reason = (int)TeleportReason.Transfer,
        });
    }
}
