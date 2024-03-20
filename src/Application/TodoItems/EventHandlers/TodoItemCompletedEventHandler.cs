using LightsOn.Domain.Events;
using LightsOn.Domain.Events.TodoItem;
using Microsoft.Extensions.Logging;

namespace LightsOn.Application.TodoItems.EventHandlers;

public class TodoItemCompletedEventHandler : INotificationHandler<TodoItemCompletedEvent>
{
    private readonly ILogger<TodoItemCompletedEventHandler> _logger;

    public TodoItemCompletedEventHandler(ILogger<TodoItemCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TodoItemCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("LightsOn Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
