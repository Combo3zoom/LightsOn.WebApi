using LightsOn.Application.Common.Exceptions;
using LightsOn.Application.TodoItems.Commands.CreateTodoItem;
using LightsOn.Application.TodoLists.Commands.CreateTodoList;
using LightsOn.Domain.Entities;

namespace LightsOn.Application.IntegrationTests.TodoItems.Commands;

[Collection("Tests")]
public class CreateTodoItemTests : BaseTestFixture, IClassFixture<Testing>
{
    private readonly Testing _testing;

    public CreateTodoItemTests(Testing testing)
    {
        _testing = testing;
    }
    [Fact]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateTodoItemCommand();

        await FluentActions.Invoking(() =>
            _testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task ShouldCreateTodoItem()
    {
        var userId = await _testing.RunAsDefaultUserAsync();

        var listId = await _testing.SendAsync(new CreateTodoListCommand
                   {
                       Title = "New List"
                   });

        var command = new CreateTodoItemCommand
        {
            ListId = listId,
            Title = "Tasks"
        };

        var itemId = await _testing.SendAsync(command);

        var item = await _testing.FindAsync<TodoItem>(itemId);

        item.Should().NotBeNull();
        item!.ListId.Should().Be(command.ListId);
        item.Title.Should().Be(command.Title);
        item.CreatedBy.Should().Be(userId);
        item.Created.Should().BeExactly(_testing._mockDataTimeOffset.Object.Now);
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().BeExactly(_testing._mockDataTimeOffset.Object.Now);
    }
}
