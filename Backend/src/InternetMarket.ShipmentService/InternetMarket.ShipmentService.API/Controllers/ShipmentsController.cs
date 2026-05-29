using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ShipmentService.API.DTOs;
using InternetMarket.ShipmentService.Application.Abstractions.Clients;
using InternetMarket.ShipmentService.Application.DTOs;
using InternetMarket.ShipmentService.Application.Shipments.Calculate;
using InternetMarket.ShipmentService.Application.Shipments.Create;
using InternetMarket.ShipmentService.Application.Shipments.Get;
using InternetMarket.ShipmentService.Application.Shipments.Get.GetDeliveryPoints;
using InternetMarket.ShipmentService.Application.Shipments.Update.UpdateStatus;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InternetMarket.ShipmentService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShipmentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _environment;

        public ShipmentsController(IMediator mediator, IWebHostEnvironment environment)
        {
            _mediator = mediator;
            _environment = environment;
        }

        [HttpPost]
        [Route("create-order/{orderId}")]
        public async Task<IActionResult> CreateOrderDeliveryAsync([FromBody] CreateOrderDeliveryRequest request, [FromRoute] Guid orderId)
        {
            var result = await _mediator.Send(new CreateShipmentCommand(
                request.PaymentMethod,
                request.DeliveryType,
                request.ToCityCode,
                request.DeliveryPointId,
                request.City,
                request.Address,
                request.FullName,
                request.NumberPhone,
                orderId,
                request.OrderItems.Select(oi =>
                new OrderItemDto(
                    oi.ProductId,
                    oi.ProductName,
                    oi.Quantity,
                    oi.UnitPrice,
                    oi.Weight,
                    oi.Length,
                    oi.Width,
                    oi.Height,
                    oi.IsLargeSizeProduct)),
                request.TotalPrice));
            return Ok(result);
        }

        [HttpPost]
        [Route("test/orders/{orderId}/complete")]
        public async Task<IActionResult> SimulateOrderDeliveryAsync([FromRoute] Guid orderId)
        {
            if (_environment.IsDevelopment())
                return NotFound();

            await _mediator.Send(new ReceivedStatusCommand(orderId));
            return NoContent();
        }

        [HttpGet]
        [Route("deliverypoints")]
        public async Task<IActionResult> GetDeliveryPointsAsync([FromQuery] int cityCode)
        {
            var result = await _mediator.Send(new GetDeliveryPointsQuery(cityCode));
            return Ok(result);
        }

        [HttpPost]
        [Route("calculator")]
        public async Task<IActionResult> CalculateDeliveryAsync([FromBody] CalculateDeliveryRequest request)
        {
            var result = await _mediator.Send(new CalculateDeliveryPriceCommand(
                request.DeliveryType,
                request.ToCityCode,
                request.OrderItems.Select(oi => new OrderItemDto(
                    oi.ProductId,
                    oi.ProductName,
                    oi.Quantity,
                    oi.UnitPrice,
                    oi.Weight,
                    oi.Length,
                    oi.Width,
                    oi.Height,
                    oi.IsLargeSizeProduct))));
            return Ok(result);
        }
        [HttpGet]
        [Route("cities")]
        public async Task<IActionResult> GetAvailableCitiesAsync([FromQuery] string name)
        {
            var results = await _mediator.Send(new GetCitiesQuery(name));

            return Ok(results);
        }
    }
}