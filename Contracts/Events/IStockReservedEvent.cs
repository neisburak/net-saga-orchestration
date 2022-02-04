using Contracts.Messages;

namespace Contracts.Events
{
    public interface IStockReservedEvent
    {
        Guid CorrelationId { get; }
        IEnumerable<OrderItemMessage> Items { get; }
    }
}