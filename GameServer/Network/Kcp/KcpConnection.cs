using GameServer.Extensions;
using System.Buffers;
using KcpSharp;
using GameServer.Network.Messages;

namespace GameServer.Network.Kcp;
internal class KcpConnection : IConnection
{
    private readonly byte[] _recvBuffer;
    private readonly KcpConversation _conv;
    private uint _upStreamSeqNo;
    private uint _downStreamSeqNo;

    public KcpConnection(KcpConversation conv)
    {
        _conv = conv;
        _recvBuffer = GC.AllocateUninitializedArray<byte>(8192);
    }

    public bool Active => !_conv.TransportClosed;

    public async ValueTask<BaseMessage?> ReceiveMessageAsync()
    {
        KcpConversationReceiveResult result = await _conv.ReceiveAsync(_recvBuffer.AsMemory(), CancellationToken.None);
        if (result.TransportClosed) return null;

        ReadOnlyMemory<byte> buffer = _recvBuffer.AsMemory(0, result.BytesReceived);
        BaseMessage message = MessageManager.DecodeMessage(buffer.Slice(BaseMessage.LengthFieldSize, buffer.Span.ReadInt24LittleEndian()));

        if (message.SeqNo < _downStreamSeqNo) return null;

        _downStreamSeqNo = message.SeqNo;
        return message;
    }

    public Task OnServerMessageAvailable(BaseMessage message)
    {
        message.SeqNo = NextUpStreamSeqNo();
        return SendAsyncImpl(message);
    }

    private async Task SendAsyncImpl(BaseMessage message)
    {
        int networkSize = message.NetworkSize;

        using IMemoryOwner<byte> memoryOwner = MemoryPool<byte>.Shared.Rent(networkSize);
        Memory<byte> memory = memoryOwner.Memory;

        memory.Span.WriteInt24LittleEndian(networkSize - BaseMessage.LengthFieldSize);

        MessageManager.EncodeMessage(memory[BaseMessage.LengthFieldSize..], message);

        if (_conv == null) throw new InvalidOperationException("Trying to send message when conv is null");
        await _conv.SendAsync(memoryOwner.Memory[..networkSize]);
    }

    private uint NextUpStreamSeqNo()
    {
        return Interlocked.Increment(ref _upStreamSeqNo);
    }
}
