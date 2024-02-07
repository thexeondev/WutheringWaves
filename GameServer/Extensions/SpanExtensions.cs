namespace GameServer.Extensions;
internal static class SpanExtensions
{
    public static void WriteInt24LittleEndian(this Span<byte> span, int value)
    {
        span[0] = (byte)value;
        span[1] = (byte)(value >> 8);
        span[2] = (byte)(value >> 16);
    }

    public static int ReadInt24LittleEndian(this ReadOnlySpan<byte> span)
    {
        return span[0] | span[1] << 8 | span[2] << 16;
    }
}
