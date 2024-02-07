namespace GameServer.Network;
internal enum MessageType : byte
{
    Request = 1,
    Response,
    Exception,
    Push
}
