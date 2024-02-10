using System.Buffers;
using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using GameServer.Settings;
using KcpSharp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GameServer.Network.Kcp;
internal class KcpGateway
{
    private const int KcpSynPacketSize = 1;
    private const int KcpAckPacketSize = 5;

    private const byte NetFlagSyn = 0xED;
    private const byte NetFlagAck = 0xEE;

    private readonly ILogger _logger;
    private readonly IOptions<GatewaySettings> _options;
    private readonly SessionManager _sessionManager;

    private IKcpTransport<IKcpMultiplexConnection>? _kcpTransport;
    private int _convCounter;

    public KcpGateway(ILogger<KcpGateway> logger, IOptions<GatewaySettings> options, SessionManager sessionManager)
    {
        _logger = logger;
        _options = options;
        _sessionManager = sessionManager;
    }

    public void Start()
    {
        IPEndPoint endPoint = _options.Value.EndPoint;
        _kcpTransport = KcpSocketTransport.CreateMultiplexConnection(new(endPoint), 1400);
        _kcpTransport.SetHandshakeHandler(KcpSynPacketSize, HandleKcpSynPacket);
        _kcpTransport.Start();

        _logger.LogInformation("Listening for incoming connections at {endPoint}", endPoint);
    }

    private async ValueTask HandleKcpSynPacket(UdpReceiveResult recvResult)
    {
        if (recvResult.Buffer[0] != NetFlagSyn) return;

        _logger.LogInformation("Received SYN from {remoteEndPoint}", recvResult.RemoteEndPoint);

        int conv = Interlocked.Increment(ref _convCounter);
        _ = _sessionManager.RunSessionAsync(_kcpTransport!.Connection.CreateConversation(conv, recvResult.RemoteEndPoint));

        await SendAckAsync(conv, recvResult.RemoteEndPoint);
    }

    private async ValueTask SendAckAsync(int conv, IPEndPoint remoteEndPoint)
    {
        _logger.LogInformation("Sending ACK to {remoteEndPoint}, convID: {conv}", remoteEndPoint, conv);

        byte[] buffer = ArrayPool<byte>.Shared.Rent(KcpAckPacketSize);
        try
        {
            buffer[0] = NetFlagAck;
            BinaryPrimitives.WriteInt32LittleEndian(buffer.AsSpan(1), conv);

            await _kcpTransport!.SendPacketAsync(buffer.AsMemory(0, KcpAckPacketSize), remoteEndPoint, CancellationToken.None);
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }
    }
}
