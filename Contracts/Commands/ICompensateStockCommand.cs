using Contracts.Messages;

namespace Contracts.Commands
{
    public interface ICompensateStockCommand
    {
        IEnumerable<OrderItemMessage> Items { get; }
    }
}