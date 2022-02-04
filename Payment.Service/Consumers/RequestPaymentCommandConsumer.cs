using Contracts.Commands;
using Contracts.Events;
using MassTransit;

namespace Payment.Service.Consumers
{
    public class RequestPaymentCommandConsumer : IConsumer<IRequestPaymentCommand>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public RequestPaymentCommandConsumer(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<IRequestPaymentCommand> context)
        {
            var message = context.Message;
            var paymentResult = true;

            if (paymentResult)
            {
                await _publishEndpoint.Publish<IPaymentConfirmedEvent>(new
                {
                    CorrelationId = message.CorrelationId
                });
            }
            else
            {
                await _publishEndpoint.Publish<IPaymentRejectedEvent>(new
                {
                    CorrelationId = message.CorrelationId,
                    Items = message.Items,
                    Reason = "Payment rejected!"
                });
            }
        }
    }
}