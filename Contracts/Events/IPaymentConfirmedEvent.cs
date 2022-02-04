namespace Contracts.Events
{
    public interface IPaymentConfirmedEvent
    {
        Guid CorrelationId { get; }
    }
}