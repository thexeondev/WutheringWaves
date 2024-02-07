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
        KcpSession session = scope.ServiceProvider.GetRequiredService<KcpSession>();

        try
        {
            session.SetConv(kcpConv);
            await session.RunAsync();
        }
        catch (Exception exception)
        {
            _logger.LogError("Exception occurred in session processing: {trace}", exception);
        }
    }
}
