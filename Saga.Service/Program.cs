using MassTransit;
using Saga.Service;
using Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransit(configure =>
{
    configure.AddSagaStateMachine<OrderStateMachine, OrderStateInstance>()
        .InMemoryRepository();

    configure.AddBus(factory => Bus.Factory.CreateUsingRabbitMq(config =>
    {
        config.Host(RabbitMQConstants.Uri);
        config.ReceiveEndpoint(RabbitMQConstants.SagaQueue, e =>
        {
            e.ConfigureSaga<OrderStateInstance>(factory);
        });
    }));
});
builder.Services.AddMassTransitHostedService();

var app = builder.Build();

app.Run();
