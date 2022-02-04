using Contracts.Messages;

namespace Contracts.Events
{
    public interface IOrderSubmittedEvent
    {
        int OrderId { get; }
        int UserId { get; }
        IEnumerable<OrderItemMessage> Items { get; }
    }
}