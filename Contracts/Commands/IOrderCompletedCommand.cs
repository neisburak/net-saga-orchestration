namespace Contracts.Commands
{
    public interface IOrderCompletedCommand
    {
        int OrderId { get; }
    }
}