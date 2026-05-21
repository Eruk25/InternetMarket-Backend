using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ShipmentService.API.DTOs;
using InternetMarket.ShipmentService.Application.Abstractions.Clients;
using InternetMarket.ShipmentService.Application.Shipments.Calculate;
using InternetMarket.ShipmentService.Application.Shipments.Get;
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
        [Route("create-order")]
        public async Task<IActionResult> CreateOrderDeliveryAsync()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("test/orders/{orderId}/complete")]
        public async Task<IActionResult> SimulateOrderDelivery([FromRoute] Guid orderId)
        {
            if (_environment.IsDevelopment())
                return NotFound();

            return Ok();
        }

        [HttpPost]
        [Route("calculator")]
        public async Task<IActionResult> CalculateDeliveryAsync([FromBody] CalculateDeliveryRequest request)
        {
            var result = await _mediator.Send(new CalculateDeliveryPriceCommand(request.ToCityCode, request.TypeOfDelivery));
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