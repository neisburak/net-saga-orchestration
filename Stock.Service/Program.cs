using MassTransit;
using Shared;
using Stock.Service.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransit(configure =>
{
    configure.AddConsumer<PaymentRejectedEventConsumer>();
    configure.AddConsumer<ReserveStockCommandConsumer>();

    configure.UsingRabbitMq((context, config) =>
    {
        config.Host(RabbitMQConstants.Uri);

        config.ReceiveEndpoint(RabbitMQConstants.CompensateStockCommandQueue, e =>
       {
           e.Consumer<PaymentRejectedEventConsumer>(context);
       });

        config.ReceiveEndpoint(RabbitMQConstants.ReserveStockCommandQueue, e =>
       {
           e.Consumer<ReserveStockCommandConsumer>(context);
       });
    });
});
builder.Services.AddMassTransitHostedService();

var app = builder.Build();

app.Run();
