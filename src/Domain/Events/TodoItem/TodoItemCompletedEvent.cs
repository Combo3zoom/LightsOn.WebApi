namespace LightsOn.Domain.Events.TodoItem;

public class TodoItemCompletedEvent : BaseEvent
{
    public TodoItemCompletedEvent(Entities.TodoItem item)
    {
        Item = item;
    }

    public Entities.TodoItem Item { get; }
}
