using MassTransit;
using Order.Service.Consumers;
using Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransit(configure =>
{
    configure.AddConsumer<CreateOrderCommandConsumer>();
    configure.AddConsumer<OrderCompletedCommandConsumer>();
    configure.AddConsumer<OrderFailedCommandConsumer>();

    configure.UsingRabbitMq((context, config) =>
    {
        config.Host(RabbitMQConstants.Uri);

        config.ReceiveEndpoint(RabbitMQConstants.CreateOrderCommandQueue, e =>
        {
            e.Consumer<CreateOrderCommandConsumer>(context);
        });

        config.ReceiveEndpoint(RabbitMQConstants.OrderCompletedCommandQueue, e =>
        {
            e.Consumer<OrderCompletedCommandConsumer>(context);
        });

        config.ReceiveEndpoint(RabbitMQConstants.OrderFailedCommandQueue, e =>
        {
            e.Consumer<OrderFailedCommandConsumer>(context);
        });
    });
});
builder.Services.AddMassTransitHostedService();


var app = builder.Build();
app.Run();
