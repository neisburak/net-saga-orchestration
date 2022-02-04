using Contracts.Commands;
using MassTransit;

namespace Order.Service.Consumers
{
    public class OrderFailedCommandConsumer : IConsumer<IOrderFailedCommand>
    {
        public Task Consume(ConsumeContext<IOrderFailedCommand> context)
        {
            var message = context.Message;

            // Order failed, update order status to Rejected...

            return Task.CompletedTask;
        }
    }
}