using Contracts.Commands;
using Contracts.Messages;

namespace Saga.Service.Commands
{
    public class ReserveStockCommand : IReserveStockCommand
    {
        public ReserveStockCommand(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        public Guid CorrelationId { get; private set; }

        public IEnumerable<OrderItemMessage> Items { get; set; } = new List<OrderItemMessage>();
    }
}