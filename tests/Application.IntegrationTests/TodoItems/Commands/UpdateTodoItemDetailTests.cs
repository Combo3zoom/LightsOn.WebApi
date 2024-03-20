using LightsOn.Application.TodoItems.Commands.CreateTodoItem;
using LightsOn.Application.TodoItems.Commands.UpdateTodoItem;
using LightsOn.Application.TodoItems.Commands.UpdateTodoItemDetail;
using LightsOn.Application.TodoLists.Commands.CreateTodoList;
using LightsOn.Domain.Entities;
using LightsOn.Domain.Enums;

namespace LightsOn.Application.IntegrationTests.TodoItems.Commands;


[Collection("Tests")]
public class UpdateTodoItemDetailTests : BaseTestFixture, IClassFixture<Testing>
{
    private readonly Testing _testing;

    public UpdateTodoItemDetailTests(Testing testing)
    {
        _testing = testing;
    }
    
    [Fact]
    public async Task ShouldRequireValidTodoItemId()
    {
        var command = new UpdateTodoItemCommand { Id = 99, Title = "New Title" };
        await FluentActions.Invoking(() => _testing.SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task ShouldUpdateTodoItem()
    {
        var userId = await _testing.RunAsDefaultUserAsync();

        var listId = await _testing.SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        var itemId = await _testing.SendAsync(new CreateTodoItemCommand
        {
            ListId = listId,
            Title = "New Item"
        });

        var command = new UpdateTodoItemDetailCommand
        {
            Id = itemId,
            ListId = listId,
            Note = "This is the note.",
            Priority = PriorityLevel.High
        };

        await _testing.SendAsync(command);

        var item = await _testing.FindAsync<TodoItem>(itemId);

        item.Should().NotBeNull();
        item!.ListId.Should().Be(command.ListId);
        item.Note.Should().Be(command.Note);
        item.Priority.Should().Be(command.Priority);
        item.LastModifiedBy.Should().NotBeNull();
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().NotBeNull();
        item.LastModified.Should().BeExactly(_testing._mockDataTimeOffset.Object.Now);
    }
}
