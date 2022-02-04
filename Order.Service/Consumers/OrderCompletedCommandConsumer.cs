using Contracts.Commands;
using MassTransit;

namespace Order.Service.Consumers
{
    public class OrderCompletedCommandConsumer : IConsumer<IOrderCompletedCommand>
    {
        public Task Consume(ConsumeContext<IOrderCompletedCommand> context)
        {
            var message = context.Message;

            // Order completed, update order status to Confirmed...

            return Task.CompletedTask;
        }
    }
}