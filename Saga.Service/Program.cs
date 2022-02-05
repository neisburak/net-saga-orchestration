using MassTransit;
using Microsoft.EntityFrameworkCore;
using Saga.Service;
using Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransit(configure =>
{
    configure.AddSagaStateMachine<OrderStateMachine, OrderStateInstance>()
        .EntityFrameworkRepository(config =>
        {
            config.AddDbContext<DbContext, OrderStateDbContext>((p, b) =>
            {
                b.UseSqlServer(builder.Configuration.GetConnectionString("OrderStateDb"));
            });
        });
    //.InMemoryRepository();

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
