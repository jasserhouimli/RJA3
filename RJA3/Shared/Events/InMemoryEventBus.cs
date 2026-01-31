
namespace RJA3.Shared.Events;

public class InMemoryEventBus : IEventBus
{
    private readonly Dictionary<Type, List<Type>> _handlers = new();
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<InMemoryEventBus> _logger;

    public InMemoryEventBus(IServiceProvider serviceProvider, ILogger<InMemoryEventBus> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public void Subscribe<TEvent, THandler>()
        where TEvent : IEvent
        where THandler : IEventHandler<TEvent>
    {
        var eventType = typeof(TEvent);
        var handlerType = typeof(THandler);

        if (!_handlers.ContainsKey(eventType))
        {
            _handlers[eventType] = new List<Type>();
        }

        if (!_handlers[eventType].Contains(handlerType))
        {
            _handlers[eventType].Add(handlerType);
            _logger.LogInformation("Subscribed handler {HandlerType} for event {EventType}", handlerType.Name, eventType.Name);
        }
    }

    public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
    {
        var eventType = typeof(TEvent);
        _logger.LogInformation("Publishing event {EventType}", eventType.Name);

        if (_handlers.ContainsKey(eventType))
        {
            _logger.LogInformation("Found {HandlerCount} handlers for event {EventType}", _handlers[eventType].Count, eventType.Name);
            using var scope = _serviceProvider.CreateScope();
            foreach (var handlerType in _handlers[eventType])
            {
                _logger.LogInformation("Resolving handler {HandlerType}", handlerType.Name);
                var handler = scope.ServiceProvider.GetService(handlerType) as IEventHandler<TEvent>;
                if (handler != null)
                {
                    _logger.LogInformation("Executing handler {HandlerType}", handlerType.Name);
                    await handler.HandleAsync(@event);
                }
                else
                {
                    _logger.LogWarning("Failed to resolve handler {HandlerType}", handlerType.Name);
                }
            }
        }
        else
        {
            _logger.LogWarning("No handlers found for event {EventType}", eventType.Name);
        }
    }
}
