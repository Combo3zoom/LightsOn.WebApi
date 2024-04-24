using LightsOn.Application.Common.Interfaces;
using LightsOn.Domain.Events.Customer;
using Microsoft.Extensions.Logging;

namespace LightsOn.Application.Customer.EventHandlers;

public class CustomerCreatedEventHandler : INotificationHandler<CustomerCreatedEvent>
{
    private readonly ILogger<CustomerCreatedEventHandler> _logger;
    private readonly ITelegramBot _telegramBot;

    public CustomerCreatedEventHandler(ILogger<CustomerCreatedEventHandler> logger, ITelegramBot telegramBot)
    {
        _logger = logger;
        _telegramBot = telegramBot;
    } 

    public async Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Customer created event: {DomainEvent}", notification.GetType().Name);
        
        await _telegramBot.SendMessageToAllowedUsers($"Customer name {notification.Customer.Name}\n" +
                                                     $"Phone number {notification.Customer.PhoneNumber}\n"+
                                                    $"Description problem {notification.Customer.DescribeProblem}");
        
        _logger.LogInformation("Send telegram bot message event");
    }
}