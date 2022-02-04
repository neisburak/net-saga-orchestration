namespace Contracts.Commands
{
    public interface IOrderFailedCommand
    {
        int OrderId { get; }
        string Reason { get; }
    }
}