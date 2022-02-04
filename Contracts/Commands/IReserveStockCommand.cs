using Contracts.Messages;

namespace Contracts.Commands
{
    public interface IReserveStockCommand
    {
        Guid CorrelationId { get; }
        IEnumerable<OrderItemMessage> Items { get; }
    }
}