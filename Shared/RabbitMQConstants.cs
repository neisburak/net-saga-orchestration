namespace Shared
{
    public static class RabbitMQConstants
    {
        public const string Uri = "amqp://localhost";

        public const string SagaQueue = "order.saga";
        public const string CreateOrderCommandQueue = "order.create";

        public const string ReserveStockCommandQueue = "stock.order.received";
        public const string RequestPaymentCommandQueue = "payment.request";
        public const string OrderCompletedCommandQueue = "order.completed";
        public const string OrderFailedCommandQueue = "order.failed";
        public const string CompensateStockCommandQueue = "stock.compensate";
    }
}