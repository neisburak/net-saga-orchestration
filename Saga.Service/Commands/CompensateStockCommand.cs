using Contracts.Commands;
using Contracts.Messages;

namespace Saga.Service.Commands
{
    public class CompensateStockCommand : ICompensateStockCommand
    {
        public IEnumerable<OrderItemMessage> Items { get; set; } = new List<OrderItemMessage>();
    }
}