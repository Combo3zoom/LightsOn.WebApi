using LightsOn.Application.Common.Exceptions;
using LightsOn.Application.Common.Security;
using LightsOn.Application.TodoLists.Commands.CreateTodoList;
using LightsOn.Application.TodoLists.Commands.PurgeTodoLists;
using LightsOn.Domain.Entities;

namespace LightsOn.Application.IntegrationTests.TodoLists.Commands;

using static Testing;


[Collection("Tests")]
public class PurgeTodoListsTests : BaseTestFixture, IClassFixture<Testing>
{
    private readonly Testing _testing;

    public PurgeTodoListsTests(Testing testing)
    {
        _testing = testing;
    }
    
    [Fact]
    public async Task ShouldDenyAnonymousUser()
    {
        var command = new PurgeTodoListsCommand();

        command.GetType().Should().BeDecoratedWith<AuthorizeAttribute>();

        var action = () => _testing.SendAsync(command);

        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Fact]
    public async Task ShouldDenyNonAdministrator()
    {
        await _testing.RunAsDefaultUserAsync();

        var command = new PurgeTodoListsCommand();

        var action = () => _testing.SendAsync(command);

        await action.Should().ThrowAsync<ForbiddenAccessException>();
    }

    [Fact]
    public async Task ShouldAllowAdministrator()
    {
        await _testing.RunAsAdministratorAsync();

        var command = new PurgeTodoListsCommand();

        var action = () => _testing.SendAsync(command);

        await action.Should().NotThrowAsync<ForbiddenAccessException>();
    }

    [Fact]
    public async Task ShouldDeleteAllLists()
    {
        await _testing.RunAsAdministratorAsync();

        await _testing.SendAsync(new CreateTodoListCommand
        {
            Title = "New List #1"
        });

        await _testing.SendAsync(new CreateTodoListCommand
        {
            Title = "New List #2"
        });

        await _testing.SendAsync(new CreateTodoListCommand
        {
            Title = "New List #3"
        });

        await _testing.SendAsync(new PurgeTodoListsCommand());

        var count = await _testing.CountAsync<TodoList>();

        count.Should().Be(0);
    }
}
