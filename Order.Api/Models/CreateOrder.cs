using Contracts.Commands;
using Contracts.Messages;

namespace Order.Api.Models
{
    public class CreateOrder : ICreateOrderCommand
    {
        public int UserId { get; set; }

        public IEnumerable<OrderItemMessage> Items { get; set; } = new List<OrderItemMessage>();
    }
}