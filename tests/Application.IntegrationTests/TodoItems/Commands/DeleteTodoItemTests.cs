using LightsOn.Application.TodoItems.Commands.CreateTodoItem;
using LightsOn.Application.TodoItems.Commands.DeleteTodoItem;
using LightsOn.Application.TodoLists.Commands.CreateTodoList;
using LightsOn.Domain.Entities;

namespace LightsOn.Application.IntegrationTests.TodoItems.Commands;


[Collection("Tests")]
public class DeleteTodoItemTests : BaseTestFixture, IClassFixture<Testing>
{
    private readonly Testing _testing;

    public DeleteTodoItemTests(Testing testing)
    {
        _testing = testing;
    }
    
    [Fact]
    public async Task ShouldRequireValidTodoItemId()
    {
        var command = new DeleteTodoItemCommand(99);

        await FluentActions.Invoking(() =>
            _testing.SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task ShouldDeleteTodoItem()
    {
        var listId = await _testing.SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        var itemId = await _testing.SendAsync(new CreateTodoItemCommand
        {
            ListId = listId,
            Title = "New Item"
        });

        await _testing.SendAsync(new DeleteTodoItemCommand(itemId));

        var item = await _testing.FindAsync<TodoItem>(itemId);

        item.Should().BeNull();
    }
}
