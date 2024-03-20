using FluentValidation;
using LightsOn.Application.Client.Commands.DeleteClient;
using LightsOn.Application.TodoLists.Commands.DeleteTodoList;

namespace LightsOn.Application.IntegrationTests.Client.Command.DeleteClient;
using static LightsOn.Application.IntegrationTests.Testing;
public partial class DeleteClientCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomClientTestCaseSource))]
    public async Task ShouldRequireValidClientId(Domain.Entities.Client randomClient)
    {
        var nonExistedClient = new DeleteCommandClient(randomClient.Id);
        
        await FluentActions.Invoking(() 
            => _testing.SendAsync(nonExistedClient)).Should().ThrowAsync<NotFoundException>();
    }
}