using Contracts.Messages;

namespace Contracts.Events
{
    public interface IPaymentRejectedEvent
    {
        Guid CorrelationId { get; }
        IEnumerable<OrderItemMessage> Items { get; }
        string Reason { get; }
    }
}