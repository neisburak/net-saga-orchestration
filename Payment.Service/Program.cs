using MassTransit;
using Payment.Service.Consumers;
using Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransit(configure =>
{
    configure.AddConsumer<RequestPaymentCommandConsumer>();

    configure.UsingRabbitMq((context, config) =>
    {
        config.Host(RabbitMQConstants.Uri);

        config.ReceiveEndpoint(RabbitMQConstants.RequestPaymentCommandQueue, e =>
        {
            e.Consumer<RequestPaymentCommandConsumer>(context);
        });
    });
});
builder.Services.AddMassTransitHostedService();

var app = builder.Build();

app.Run();
