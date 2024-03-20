using LightsOn.Application.Common.Exceptions;
using LightsOn.Application.TodoLists.Commands.CreateTodoList;
using LightsOn.Domain.Entities;

namespace LightsOn.Application.IntegrationTests.TodoLists.Commands;


[Collection("Tests")]
public class CreateTodoListTests : BaseTestFixture, IClassFixture<Testing>
{
    private readonly Testing _testing;

    public CreateTodoListTests(Testing testing)
    {
        _testing = testing;
    }
    
    [Fact]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateTodoListCommand();
        await FluentActions.Invoking(() => _testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task ShouldRequireUniqueTitle()
    {
        await _testing.SendAsync(new CreateTodoListCommand
        {
            Title = "Shopping"
        });

        var command = new CreateTodoListCommand
        {
            Title = "Shopping"
        };

        await FluentActions.Invoking(() =>
            _testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task ShouldCreateTodoList()
    {
        var userId = await _testing.RunAsDefaultUserAsync();

        var command = new CreateTodoListCommand
        {
            Title = "Tasks"
        };

        var id = await _testing.SendAsync(command);

        var list = await _testing.FindAsync<TodoList>(id);

        list.Should().NotBeNull();
        list!.Title.Should().Be(command.Title);
        list.CreatedBy.Should().Be(userId);
        // list.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        // list.Created.Should().BeExactly(_testing._mockDataTimeOffset.Object.Now);
    }
}
