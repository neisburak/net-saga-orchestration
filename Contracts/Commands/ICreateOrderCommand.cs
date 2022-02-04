using Contracts.Messages;

namespace Contracts.Commands
{
    public interface ICreateOrderCommand
    {
        int UserId { get; }
        IEnumerable<OrderItemMessage> Items { get; }
    }
}