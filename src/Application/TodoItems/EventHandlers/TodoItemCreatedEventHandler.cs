using LightsOn.Domain.Events;
using LightsOn.Domain.Events.TodoItem;
using Microsoft.Extensions.Logging;

namespace LightsOn.Application.TodoItems.EventHandlers;

public class TodoItemCreatedEventHandler : INotificationHandler<TodoItemCreatedEvent>
{
    private readonly ILogger<TodoItemCreatedEventHandler> _logger;

    public TodoItemCreatedEventHandler(ILogger<TodoItemCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TodoItemCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("LightsOn Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
