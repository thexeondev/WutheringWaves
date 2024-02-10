using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;
using GameServer.Controllers.Attributes;
using GameServer.Controllers.Manager;
using GameServer.Models;
using Microsoft.Extensions.Logging;

namespace GameServer.Systems.Event;
internal class EventSystem
{
    private static readonly ImmutableDictionary<GameEventType, Func<ModelManager, Task>> s_modelManagerEventHandlers = RegisterModelManagerEvents();

    private readonly ModelManager _modelManager;
    private readonly ControllerManager _controllerManager;

    private readonly ILogger _logger;

    public EventSystem(ModelManager modelManager, ControllerManager controllerManager, ILogger<EventSystem> logger)
    {
        _modelManager = modelManager;
        _controllerManager = controllerManager;

        _logger = logger;
    }

    public async Task Emit(GameEventType eventType)
    {
        if (s_modelManagerEventHandlers.TryGetValue(eventType, out var handler))
            await handler(_modelManager);

        await _controllerManager.OnEvent(eventType);

        _logger.LogInformation("Event {type} emitted", eventType);
    }

    private static ImmutableDictionary<GameEventType, Func<ModelManager, Task>> RegisterModelManagerEvents()
    {
        var builder = ImmutableDictionary.CreateBuilder<GameEventType, Func<ModelManager, Task>>();

        foreach (MethodInfo method in typeof(ModelManager).GetMethods())
        {
            GameEventAttribute? attribute = method.GetCustomAttribute<GameEventAttribute>();
            if (attribute == null) continue;

            ParameterExpression modelManagerParam = Expression.Parameter(typeof(ModelManager));
            Expression exp = Expression.Call(modelManagerParam, method);
            if (method.ReturnType == typeof(void))
                exp = Expression.Block(exp, Expression.Constant(Task.CompletedTask));

            Expression<Func<ModelManager, Task>> lambda = Expression.Lambda<Func<ModelManager, Task>>(exp, modelManagerParam);

            builder.Add(attribute.Type, lambda.Compile());
        }

        return builder.ToImmutable();
    }
}
