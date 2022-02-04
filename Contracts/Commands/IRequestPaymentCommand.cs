using Contracts.Messages;

namespace Contracts.Commands
{
    public interface IRequestPaymentCommand
    {
        Guid CorrelationId { get; }
        decimal TotalPrice { get; }
        IEnumerable<OrderItemMessage> Items { get; }
    }
}