namespace LightsOn.Domain.Events.TodoItem;

public class TodoItemCreatedEvent : BaseEvent
{
    public TodoItemCreatedEvent(Entities.TodoItem item)
    {
        Item = item;
    }

    public Entities.TodoItem Item { get; }
}
