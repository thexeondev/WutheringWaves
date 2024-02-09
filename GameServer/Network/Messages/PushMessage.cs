using System.Buffers.Binary;
using Protocol;

namespace GameServer.Network.Messages;
internal class PushMessage : BaseMessage
{
    public override MessageType Type => MessageType.Push;
    public override int HeaderSize => 11;
    public MessageId MessageId { get; set; }

    public override void Encode(Memory<byte> buffer)
    {
        base.Encode(buffer);

        Span<byte> span = buffer.Span;
        BinaryPrimitives.WriteUInt16LittleEndian(span[5..], (ushort)MessageId);
        BinaryPrimitives.WriteUInt32LittleEndian(span[7..], 0);
    }

    public override void Decode(ReadOnlyMemory<byte> buffer)
    {
        base.Decode(buffer);

        ReadOnlySpan<byte> span = buffer.Span;
        MessageId = (MessageId)BinaryPrimitives.ReadUInt16LittleEndian(span[5..]);
        _ = BinaryPrimitives.ReadUInt32LittleEndian(span[7..]);
    }
}
