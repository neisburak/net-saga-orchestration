using Contracts.Events;
using MassTransit;

namespace Stock.Service.Consumers
{
    public class PaymentRejectedEventConsumer : IConsumer<IPaymentRejectedEvent>
    {
        public Task Consume(ConsumeContext<IPaymentRejectedEvent> context)
        {
            var message = context.Message;

            // Payment rejected, rollback stock.

            return Task.CompletedTask;
        }
    }
}