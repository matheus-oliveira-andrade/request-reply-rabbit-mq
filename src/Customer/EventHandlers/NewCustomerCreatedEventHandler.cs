using System.Formats.Asn1;
using Core;
using Core.Events;
using Customer.Services;
using EasyNetQ;
using FluentValidation.Results;

namespace Customer.EventHandlers;

public class NewCustomerCreatedEventHandler : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private IBus _bus;

    public NewCustomerCreatedEventHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _bus = RabbitHutch.CreateBus("host=localhost:5672");

        await _bus.Rpc.RespondAsync<UserCreatedEvent, ResponseMessage>(request => new ResponseMessage(CreateNewCustomer(request)));
    }

    private ValidationResult CreateNewCustomer(UserCreatedEvent @event)
    {
        using var scope = _serviceProvider.CreateScope();
        var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<NewCustomerCreatedEventHandler>>();

        logger.LogInformation("Received event {EventName}", nameof(UserCreatedEvent));
        
        customerService.Create(new Domain.Customer(@event.Name, @event.Email, @event.Document));

        logger.LogInformation("New customer created [{Document}]", @event.Document);
        
        return new ValidationResult();
    }
}