using Contracts.Commands;
using Contracts.Messages;

namespace Saga.Service.Commands
{
    public class RequestPaymentCommand : IRequestPaymentCommand
    {
        public RequestPaymentCommand(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        public Guid CorrelationId { get; private set; }

        public decimal TotalPrice { get; set; }
        public IEnumerable<OrderItemMessage> Items { get; set; } = new List<OrderItemMessage>();
    }
}