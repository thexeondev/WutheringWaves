using System.Buffers.Binary;
using Protocol;

namespace GameServer.Network.Messages;
internal class ResponseMessage : BaseMessage
{
    public override MessageType Type => MessageType.Response;
    public override int HeaderSize => 13;

    public ushort RpcID { get; set; }
    public MessageId MessageId { get; set; }

    public override void Encode(Memory<byte> buffer)
    {
        base.Encode(buffer);

        Span<byte> span = buffer.Span;
        BinaryPrimitives.WriteUInt16LittleEndian(span[5..], RpcID);
        BinaryPrimitives.WriteUInt16LittleEndian(span[7..], (ushort)MessageId);
        BinaryPrimitives.WriteUInt32LittleEndian(span[9..], 0);
    }

    public override void Decode(ReadOnlyMemory<byte> buffer)
    {
        base.Decode(buffer);

        ReadOnlySpan<byte> span = buffer.Span;
        RpcID = BinaryPrimitives.ReadUInt16LittleEndian(span[5..]);
        MessageId = (MessageId)BinaryPrimitives.ReadUInt16LittleEndian(span[7..]);
        _ = BinaryPrimitives.ReadUInt32LittleEndian(span[9..]);
    }
}
