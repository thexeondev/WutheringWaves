using System.Buffers;
using GameServer.Extensions;
using GameServer.Handlers;
using GameServer.Network.Messages;
using GameServer.Network.Packets;
using GameServer.Network.Rpc;
using Google.Protobuf;
using KcpSharp;
using Microsoft.Extensions.Logging;
using Protocol;

namespace GameServer.Network;
internal class KcpSession
{
    private readonly ILogger _logger;
    private readonly MessageManager _messageManager;
    private readonly byte[] _recvBuffer;

    private KcpConversation? _conv;
    private uint _upStreamSeqNo;
    private uint _downStreamSeqNo;

    public RpcManager Rpc { get; }

    public KcpSession(ILogger<KcpSession> logger, MessageManager messageManager, RpcManager rpcManager)
    {
        _logger = logger;
        _messageManager = messageManager;
        Rpc = rpcManager;
        _recvBuffer = GC.AllocateUninitializedArray<byte>(8192);
    }

    public async Task RunAsync()
    {
        while (_conv != null)
        {
            KcpConversationReceiveResult result = await _conv.ReceiveAsync(_recvBuffer.AsMemory(), CancellationToken.None);
            if (result.TransportClosed) return;

            ReadOnlyMemory<byte> buffer = _recvBuffer.AsMemory(0, result.BytesReceived);
            await HandleMessageAsync(MessageManager.DecodeMessage(buffer.Slice(BaseMessage.LengthFieldSize, buffer.Span.ReadInt24LittleEndian())));
        }
    }

    private async Task HandleMessageAsync(BaseMessage message)
    {
        if (_downStreamSeqNo >= message.SeqNo) return;
        _downStreamSeqNo = message.SeqNo;

        switch (message)
        {
            case RequestMessage request:
                await Rpc.HandleRpcRequest(request);
                break;
            case PushMessage push:
                if (!await _messageManager.ProcessMessage(push.MessageId, push.Payload))
                    _logger.LogWarning("Push message ({id}) was not handled", push.MessageId);

                break;
        }
    }

    public Task PushMessage<TProtoBuf>(MessageId id, TProtoBuf data) where TProtoBuf : IMessage<TProtoBuf>
    {
        return Send(new PushMessage
        {
            MessageId = id,
            Payload = data.ToByteArray()
        });
    }

    public Task Send(BaseMessage message)
    {
        message.SeqNo = NextUpStreamSeqNo();
        return SendAsyncImpl(message);
    }

    public void SetConv(KcpConversation conv)
    {
        if (_conv != null) throw new InvalidOperationException("Conv was already set");

        _conv = conv;
    }

    private uint NextUpStreamSeqNo()
    {
        return Interlocked.Increment(ref _upStreamSeqNo);
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
}
