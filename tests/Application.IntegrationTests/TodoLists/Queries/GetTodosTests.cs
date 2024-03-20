using LightsOn.Application.TodoLists.Queries.GetTodos;
using LightsOn.Domain.Entities;
using LightsOn.Domain.ValueObjects;

namespace LightsOn.Application.IntegrationTests.TodoLists.Queries;

[Collection("Tests")]
public class GetTodosTests : BaseTestFixture, IClassFixture<Testing>
{
    private readonly Testing _testing;

    public GetTodosTests(Testing testing)
    {
        _testing = testing;
    }
    
    [Fact]
    public async Task ShouldReturnPriorityLevels()
    {
        await _testing.RunAsDefaultUserAsync();

        var query = new GetTodosQuery();

        var result = await _testing.SendAsync(query);

        result.PriorityLevels.Should().NotBeEmpty();
    }

    [Fact]
    public async Task ShouldReturnAllListsAndItems()
    {
        await _testing.RunAsDefaultUserAsync();

        await _testing.AddAsync(new TodoList
        {
            Title = "Shopping",
            Colour = Colour.Blue,
            Items =
                    {
                        new TodoItem { Title = "Apples", Done = true },
                        new TodoItem { Title = "Milk", Done = true },
                        new TodoItem { Title = "Bread", Done = true },
                        new TodoItem { Title = "Toilet paper" },
                        new TodoItem { Title = "Pasta" },
                        new TodoItem { Title = "Tissues" },
                        new TodoItem { Title = "Tuna" }
                    }
        });

        var query = new GetTodosQuery();

        var result = await _testing.SendAsync(query);

        result.Lists.Should().HaveCount(1);
        result.Lists.First().Items.Should().HaveCount(7);
    }

    [Fact]
    public async Task ShouldDenyAnonymousUser()
    {
        var query = new GetTodosQuery();

        var action = () => _testing.SendAsync(query);
        
        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
