using Automatonymous;
using Contracts.Events;
using Saga.Service.Commands;
using Shared;

namespace Saga.Service
{
    public class OrderStateMachine : MassTransitStateMachine<OrderStateInstance>
    {
        public State OrderSubmitted { get; set; }
        public State StockReserved { get; set; }
        public State StockNotReserved { get; set; }
        public State PaymentConfirmed { get; set; }
        public State PaymentRejected { get; set; }

        public OrderStateMachine()
        {
            InstanceState(instance => instance.CurrentState);

            Event(() => OrderSubmittedEvent, instance => instance
                .CorrelateBy<int>(state => state.OrderId, context => context.Message.OrderId)
                .SelectId(s => Guid.NewGuid()));

            Event(() => StockReservedEvent, instance => instance
                  .CorrelateById(selector => selector.Message.CorrelationId));

            Event(() => StockNotReservedEvent, instance => instance
                 .CorrelateById(selector => selector.Message.CorrelationId));

            Event(() => PaymentConfirmedEvent, instance => instance
                 .CorrelateById(selector => selector.Message.CorrelationId));

            Event(() => PaymentRejectedEvent, instance => instance
                 .CorrelateById(selector => selector.Message.CorrelationId));

            Initially(When(OrderSubmittedEvent)
                .Then(context =>
                {
                    context.Instance.UserId = context.Data.UserId;
                    context.Instance.OrderId = context.Data.OrderId;
                    context.Instance.TotalPrice = context.Data.Items.Sum(s => s.Count * s.Price);
                    context.Instance.CreatedOn = DateTime.Now;
                })
                .TransitionTo(OrderSubmitted)
                .Send(new Uri($"queue:{RabbitMQConstants.ReserveStockCommandQueue}"), context => new ReserveStockCommand(context.Instance.CorrelationId)
                {
                    Items = context.Data.Items
                }));

            During(OrderSubmitted,
                When(StockReservedEvent)
                .TransitionTo(StockReserved)
                .Send(new Uri($"queue:{RabbitMQConstants.RequestPaymentCommandQueue}"), context => new RequestPaymentCommand(context.Instance.CorrelationId)
                {
                    Items = context.Data.Items
                }),
                When(StockNotReservedEvent)
                .TransitionTo(StockNotReserved)
                .Send(new Uri($"queue:{RabbitMQConstants.OrderFailedCommandQueue}"), context => new OrderFailedCommand
                {
                    OrderId = context.Instance.OrderId,
                    Reason = context.Data.Reason
                }));

            During(StockReserved,
                When(PaymentConfirmedEvent)
                .TransitionTo(PaymentConfirmed)
                .Send(new Uri($"queue:{RabbitMQConstants.OrderCompletedCommandQueue}"), context => new OrderCompletedCommand
                {
                    OrderId = context.Instance.OrderId
                })
                .Finalize(),
                When(PaymentRejectedEvent)
                .TransitionTo(PaymentRejected)
                .Send(new Uri($"queue:{RabbitMQConstants.OrderFailedCommandQueue}"), context => new OrderFailedCommand
                {
                    OrderId = context.Instance.OrderId,
                    Reason = context.Data.Reason
                })
                .Send(new Uri($"queue:{RabbitMQConstants.CompensateStockCommandQueue}"), context => new CompensateStockCommand
                {
                    Items = context.Data.Items
                }));

            SetCompletedWhenFinalized();
        }

        public Event<IOrderSubmittedEvent> OrderSubmittedEvent { get; set; }
        public Event<IStockReservedEvent> StockReservedEvent { get; set; }
        public Event<IStockNotReservedEvent> StockNotReservedEvent { get; set; }
        public Event<IPaymentConfirmedEvent> PaymentConfirmedEvent { get; set; }
        public Event<IPaymentRejectedEvent> PaymentRejectedEvent { get; set; }
    }
}