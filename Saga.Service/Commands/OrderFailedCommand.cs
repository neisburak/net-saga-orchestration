using Contracts.Commands;

namespace Saga.Service.Commands
{
    public class OrderFailedCommand : IOrderFailedCommand
    {
        public int OrderId { get; set; }

        public string Reason { get; set; }
    }
}