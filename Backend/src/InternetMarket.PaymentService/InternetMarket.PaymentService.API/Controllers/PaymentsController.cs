using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
using InternetMarket.PaymentService.Application.PaymentMethods.Get;
using InternetMarket.PaymentService.Application.Transactions.Create;
using InternetMarket.PaymentService.Application.Transactions.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace InternetMarket.PaymentService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("methods")]
        public async Task<IActionResult> GetPaymentMethodsAsync()
        {
            var results = await _mediator.Send(new GetPaymentMethodsQuery());
            return Ok(results);
        }
        [HttpPost]
        [Route("pay/{orderId}")]
        public async Task<IActionResult> PayAsync([FromRoute] Guid orderId)
        {
            var link = await _mediator.Send(new CreateTransactionCommand(orderId));
            return Ok(link);
        }

        [HttpPost]
        [Route("webhook")]
        public async Task<IActionResult> ProcessWebhookAsync()
        {
            using var reader = new StreamReader(Request.Body);
            var json = await reader.ReadToEndAsync();

            await _mediator.Send(new UpdateTransactionStatusCommand(json));

            return Ok();
        }
    }
}