using GameServer.Models.Chat;
using GameServer.Settings;

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
    private readonly ServerBot.ServerFriend bot = ServerBot.GetServerBot()!;
    public ChatRoom GetBotChatRoom()
    {
        if (!_rooms.TryGetValue(bot.BotConfig.PlayerId, out ChatRoom? chatRoom))
        {
            chatRoom = new ChatRoom(bot.BotConfig.PlayerId);
            _rooms[bot.BotConfig.PlayerId] = chatRoom;
        }

        return chatRoom;
    }

    public IEnumerable<ChatRoom> AllChatRooms => _rooms.Values;
}
