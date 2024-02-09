using GameServer.Network.Kcp;
using GameServer.Network.Messages;
using KcpSharp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GameServer.Network;
internal class SessionManager
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger _logger;

    public SessionManager(IServiceScopeFactory scopeFactory, ILogger<SessionManager> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public async Task RunSessionAsync(KcpConversation kcpConv)
    {
        using IServiceScope scope = _scopeFactory.CreateScope();
        PlayerSession session = scope.ServiceProvider.GetRequiredService<PlayerSession>();
        KcpConnection connection = new(kcpConv);
        session.Listener = connection;

        try
        {
            while (connection.Active)
            {
                BaseMessage? message = await connection.ReceiveMessageAsync();
                if (message == null) break;

                await session.HandleMessageAsync(message);
            }
        }
        catch (Exception exception)
        {
            _logger.LogError("Exception occurred in session processing: {trace}", exception);
        }
    }
}
