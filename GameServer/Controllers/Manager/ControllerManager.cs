using GameServer.Controllers.Factory;
using GameServer.Systems.Event;
using Microsoft.Extensions.DependencyInjection;

namespace GameServer.Controllers.Manager;
internal class ControllerManager
{
    private readonly IServiceProvider _serviceProvider;
    private readonly EventHandlerFactory _eventHandlerFactory;

    public ControllerManager(IServiceProvider serviceProvider, EventHandlerFactory handlerFactory)
    {
        _serviceProvider = serviceProvider;
        _eventHandlerFactory = handlerFactory;
    }

    public async Task OnEvent(GameEventType eventType)
    {
        IEnumerable<GameEventHandler> handlers = _eventHandlerFactory.GetEventHandlers(eventType);

        foreach (GameEventHandler handler in handlers)
        {
            await handler(_serviceProvider);
        }
    }

    public TController Get<TController>() where TController : Controller
    {
        return _serviceProvider.GetRequiredService<TController>();
    }
}
