namespace Contracts.Events
{
    public interface IStockNotReservedEvent
    {
        Guid CorrelationId { get; }
        string Reason { get; }
    }
}