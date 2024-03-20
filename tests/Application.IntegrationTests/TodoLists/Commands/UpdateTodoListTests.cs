using LightsOn.Application.Common.Exceptions;
using LightsOn.Application.TodoLists.Commands.CreateTodoList;
using LightsOn.Application.TodoLists.Commands.UpdateTodoList;
using LightsOn.Domain.Entities;

namespace LightsOn.Application.IntegrationTests.TodoLists.Commands;

[Collection("Tests")]
public class UpdateTodoListTests : BaseTestFixture, IClassFixture<Testing>
{
    private readonly Testing _testing;

    public UpdateTodoListTests(Testing testing)
    {
        _testing = testing;
    }
    
    [Fact]
    public async Task ShouldRequireValidTodoListId()
    {
        var command = new UpdateTodoListCommand { Id = 99, Title = "New Title" };
        await FluentActions.Invoking(() => _testing.SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task ShouldRequireUniqueTitle()
    {
        var listId = await _testing.SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        await _testing.SendAsync(new CreateTodoListCommand
        {
            Title = "Other List"
        });

        var command = new UpdateTodoListCommand
        {
            Id = listId,
            Title = "Other List"
        };

        (await FluentActions.Invoking(() =>
                    _testing.SendAsync(command))
                .Should().ThrowAsync<ValidationException>().Where(ex => ex.Errors.ContainsKey("Title")))
                .And.Errors["Title"].Should().Contain("'Title' must be unique.");
    }

    [Fact]
    public async Task ShouldUpdateTodoList()
    {
        var userId = await _testing.RunAsDefaultUserAsync();

        var listId = await _testing.SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        var command = new UpdateTodoListCommand
        {
            Id = listId,
            Title = "Updated List Title"
        };

        await _testing.SendAsync(command);

        var list = await _testing.FindAsync<TodoList>(listId);

        list.Should().NotBeNull();
        list!.Title.Should().Be(command.Title);
        list.LastModifiedBy.Should().NotBeNull();
        list.LastModifiedBy.Should().Be(userId);
        list.LastModified.Should().NotBeNull();
        list.LastModified.Should().BeExactly(_testing._mockDataTimeOffset.Object.Now);
    }
}
