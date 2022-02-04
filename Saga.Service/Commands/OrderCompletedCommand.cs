using Contracts.Commands;

namespace Saga.Service.Commands
{
    public class OrderCompletedCommand : IOrderCompletedCommand
    {
        public int OrderId { get; set; }
    }
}