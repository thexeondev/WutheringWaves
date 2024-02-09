using System.Buffers.Binary;

namespace GameServer.Network.Messages;
internal abstract class BaseMessage
{
    public const int LengthFieldSize = 3;

    public abstract MessageType Type { get; }
    public abstract int HeaderSize { get; }

    public uint SeqNo { get; set; }
    public ReadOnlyMemory<byte> Payload { get; set; }

    public virtual void Encode(Memory<byte> buffer)
    {
        BinaryPrimitives.WriteUInt32LittleEndian(buffer.Span[1..], SeqNo);
        Payload.CopyTo(buffer[HeaderSize..]);
    }

    public virtual void Decode(ReadOnlyMemory<byte> buffer)
    {
        SeqNo = BinaryPrimitives.ReadUInt32LittleEndian(buffer.Span[1..]);
        Payload = buffer[HeaderSize..];
    }

    public int NetworkSize => HeaderSize + Payload.Length + LengthFieldSize;
}
