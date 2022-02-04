using Contracts.Commands;
using Contracts.Events;
using MassTransit;
using Shared;

namespace Order.Service.Consumers
{
    public class CreateOrderCommandConsumer : IConsumer<ICreateOrderCommand>
    {
        private readonly ISendEndpoint _sendEndpoint;

        public CreateOrderCommandConsumer(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpoint = sendEndpointProvider.GetSendEndpoint(new($"queue:{RabbitMQConstants.SagaQueue}")).Result;
        }

        public async Task Consume(ConsumeContext<ICreateOrderCommand> context)
        {
            var message = context.Message;

            // Creating order with Pending status...

            await _sendEndpoint.Send<IOrderSubmittedEvent>(new
            {
                OrderId = new Random().Next(),
                UserId = message.UserId,
                Items = message.Items
            });
        }
    }
}