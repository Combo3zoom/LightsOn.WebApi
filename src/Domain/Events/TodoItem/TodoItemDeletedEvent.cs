namespace LightsOn.Domain.Events.TodoItem;

public class TodoItemDeletedEvent : BaseEvent
{
    public TodoItemDeletedEvent(Entities.TodoItem item)
    {
        Item = item;
    }

    public Entities.TodoItem Item { get; }
}
