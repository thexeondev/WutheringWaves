using Google.Protobuf;
using Protocol;

namespace GameServer.Models.Chat;
internal class ChatRoom
{
    private readonly List<ChatContentProto> _messages;
    private int _msgIdCounter;

    public int TargetUid { get; }
    
    public ChatRoom(int targetUid)
    {
        TargetUid = targetUid;
        _messages = [];
    }
    
    public IEnumerable<ChatContentProto> ChatHistory => _messages;

    public void AddMessage(int senderId, int contentType, string content)
    {
        _messages.Add(new ChatContentProto
        {
            SenderUid = senderId,
            ChatContentType = contentType,
            Content = content,
            MsgId = NextMessageId().ToString()
        });
    }

    private int NextMessageId() => Interlocked.Increment(ref _msgIdCounter);
}
