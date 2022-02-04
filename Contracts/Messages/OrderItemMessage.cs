namespace Contracts.Messages
{
    public class OrderItemMessage
    {
        public int ProductId { get; }
        public int Count { get; }
        public decimal Price { get; }
    }
}