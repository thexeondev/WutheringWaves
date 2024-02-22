using System.Text;
using System.Text.RegularExpressions;
using GameServer.Controllers.Attributes;
using GameServer.Controllers.ChatCommands;
using GameServer.Models;
using GameServer.Models.Chat;
using GameServer.Network;
using GameServer.Network.Messages;
using Protocol;

namespace GameServer.Controllers;
internal partial class ChatController : Controller
{
    private readonly ModelManager _modelManager;

    public ChatController(PlayerSession session, ModelManager modelManager) : base(session)
    {
        _modelManager = modelManager;
    }

    [NetEvent(MessageId.PrivateChatDataRequest)]
    public async Task<RpcResult> OnPrivateChatDataRequest()
    {
        if (!_modelManager.Chat.AllChatRooms.Any())
        {
            ChatRoom chatRoom = _modelManager.Chat.GetChatRoom(1338); // Reversed Helper
            chatRoom.AddMessage(1338, (int)ChatContentType.Text, BuildWelcomeMessage());
        }

        await PushPrivateChatHistory();
        return Response(MessageId.PrivateChatDataResponse, new PrivateChatDataResponse()); // Response doesn't contain any useful info, everything is in notifies.
    }

    [NetEvent(MessageId.PrivateChatRequest)]
    public async Task<RpcResult> OnPrivateChatRequest(PrivateChatRequest request, ChatCommandManager chatCommandManager)
    {
        ChatRoom chatRoom = _modelManager.Chat.GetChatRoom(1338);

        chatRoom.AddMessage(_modelManager.Player.Id, request.ChatContentType, request.Content);
        if (!request.Content.StartsWith('/'))
        {
            chatRoom.AddMessage(1338, 0, "huh?");
        }
        else
        {
            string content = MultipleSpacesRegex().Replace(request.Content, " ");
            string[] split = content[1..].Split(' ');
            if (split.Length >= 2)
            {
                await chatCommandManager.InvokeCommandAsync(split[0], split[1], split[2..]);
            }
        }

        await PushPrivateChatHistory();
        return Response(MessageId.PrivateChatResponse, new PrivateChatResponse());
    }

    [NetEvent(MessageId.PrivateChatOperateRequest)]
    public RpcResult OnPrivateChatOperateRequest() => Response(MessageId.PrivateChatOperateResponse, new PrivateChatOperateResponse());

    private async Task PushPrivateChatHistory()
    {
        await Session.Push(MessageId.PrivateChatHistoryNotify, new PrivateChatHistoryNotify
        {
            AllChats =
            {
                _modelManager.Chat.AllChatRooms
                .Select(chatRoom => new PrivateChatHistoryContentProto
                {
                    TargetUid = chatRoom.TargetUid,
                    Chats = { chatRoom.ChatHistory }
                })
            }
        });
    }

    private static string BuildWelcomeMessage()
    {
        StringBuilder builder = new();
        builder.AppendLine("Welcome to ReversedRooms WutheringWaves private server!\nTo get support, join:\ndiscord.gg/reversedrooms");
        builder.AppendLine("List of all commands:");

        foreach (string description in ChatCommandManager.CommandDescriptions)
        {
            builder.AppendLine(description);
        }

        return builder.ToString();
    }

    [GeneratedRegex(@"\s+")]
    private static partial Regex MultipleSpacesRegex();
}
