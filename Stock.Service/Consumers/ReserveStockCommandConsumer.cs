using Contracts.Commands;
using Contracts.Events;
using MassTransit;

namespace Stock.Service.Consumers
{
    public class ReserveStockCommandConsumer : IConsumer<IReserveStockCommand>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public ReserveStockCommandConsumer(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<IReserveStockCommand> context)
        {
            var stockResult = true;
            var message = context.Message;

            if (stockResult)
            {
                await _publishEndpoint.Publish<IStockReservedEvent>(new
                {
                    CorrelationId = message.CorrelationId,
                    Items = message.Items
                });
            }
            else
            {
                await _publishEndpoint.Publish<IStockNotReservedEvent>(new
                {
                    CorrelationId = message.CorrelationId,
                    Reason = "Insufficent stock!"
                });
            }
        }
    }
}