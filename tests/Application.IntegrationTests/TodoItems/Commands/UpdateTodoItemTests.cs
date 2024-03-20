using LightsOn.Application.TodoItems.Commands.CreateTodoItem;
using LightsOn.Application.TodoItems.Commands.UpdateTodoItem;
using LightsOn.Application.TodoLists.Commands.CreateTodoList;
using LightsOn.Domain.Entities;

namespace LightsOn.Application.IntegrationTests.TodoItems.Commands;


[Collection("Tests")]
public class UpdateTodoItemTests : BaseTestFixture, IClassFixture<Testing>
{
    private readonly Testing _testing;

    public UpdateTodoItemTests(Testing testing)
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
        var time = _testing._mockDataTimeOffset.Object.Now;
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

        var command = new UpdateTodoItemCommand
        {
            Id = itemId,
            Title = "Updated Item Title"
        };

        await _testing.SendAsync(command);

        var item = await _testing.FindAsync<TodoItem>(itemId);

        item.Should().NotBeNull();
        item!.Title.Should().Be(command.Title);
        item.LastModifiedBy.Should().NotBeNull();
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().NotBeNull();
        item.LastModified.Should().BeExactly(time);
    }
}
