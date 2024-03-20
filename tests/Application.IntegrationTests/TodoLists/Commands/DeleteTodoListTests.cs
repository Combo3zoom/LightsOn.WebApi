using LightsOn.Application.TodoLists.Commands.CreateTodoList;
using LightsOn.Application.TodoLists.Commands.DeleteTodoList;
using LightsOn.Domain.Entities;

namespace LightsOn.Application.IntegrationTests.TodoLists.Commands;


[Collection("Tests")]
public class DeleteTodoListTests : BaseTestFixture, IClassFixture<Testing>
{
    private readonly Testing _testing;

    public DeleteTodoListTests(Testing testing)
    {
        _testing = testing;
    }
    
    [Fact]
    public async Task ShouldRequireValidTodoListId()
    {
        var command = new DeleteTodoListCommand(99);
        await FluentActions.Invoking(() => _testing.SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task ShouldDeleteTodoList()
    {
        var listId = await _testing.SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        await _testing.SendAsync(new DeleteTodoListCommand(listId));

        var list = await _testing.FindAsync<TodoList>(listId);

        list.Should().BeNull();
    }
}
