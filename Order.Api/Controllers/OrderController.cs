using Contracts.Commands;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Order.Api.Models;
using Shared;

namespace Order.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly ISendEndpoint _sendEndpoint;

    public OrderController(ISendEndpointProvider sendEndpointProvider)
    {
        _sendEndpoint = sendEndpointProvider.GetSendEndpoint(new($"queue:{RabbitMQConstants.CreateOrderCommandQueue}")).Result;
    }

    [HttpPost]
    public async Task Post(CreateOrder createOrder)
    {
        await _sendEndpoint.Send<ICreateOrderCommand>(new
        {
            UserId = createOrder.UserId,
            Items = createOrder.Items
        });
    }
}
