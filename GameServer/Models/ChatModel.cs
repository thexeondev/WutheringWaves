using GameServer.Models.Chat;

namespace GameServer.Models;
internal class ChatModel
{
    private readonly Dictionary<int, ChatRoom> _rooms;

    public ChatModel()
    {
        _rooms = [];
    }

    /// <summary>
    /// Gets chat room for specified player id.
    /// Creates new one if it doesn't exist.
    /// </summary>
    public ChatRoom GetChatRoom(int id)
    {
        if (!_rooms.TryGetValue(id, out ChatRoom? chatRoom))
        {
            chatRoom = new ChatRoom(id);
            _rooms[id] = chatRoom;
        }

        return chatRoom;
    }

    public IEnumerable<ChatRoom> AllChatRooms => _rooms.Values;
}
