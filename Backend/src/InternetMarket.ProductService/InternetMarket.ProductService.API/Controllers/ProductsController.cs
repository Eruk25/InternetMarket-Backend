using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ProductService.API.DTOs.Requests;
using InternetMarket.ProductService.Application.Products;
using InternetMarket.ProductService.Application.Products.Get;
using InternetMarket.ProductService.Application.Products.Get.GetById;
using InternetMarket.ProductService.Application.Products.Get.GetByIds;
using InternetMarket.ProductService.Application.Products.Update.CancelReservation;
using InternetMarket.ProductService.Application.Products.Update.ConfirmShipment;
using InternetMarket.ProductService.Application.Products.Update.Reserve;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace InternetMarket.ProductService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllAsync()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());
            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ProductDto>> GetByIdAsync(Guid id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(product);
        }

        [HttpPost]
        [Route("by-ids")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetByIdsAsync([FromBody] GetByIdsRequest request)
        {
            var products = await _mediator.Send(new GetProductsByIdsQuery(request.Ids));
            return Ok(products);
        }

        [HttpPost]
        [Route("reserve")]
        public async Task<ActionResult> ReserveAsync([FromBody] ReserveRequest request)
        {
            await _mediator.Send(new ReserveProductCommand(request.ItemsToReserve));
            return NoContent();
        }

        [HttpPost]
        [Route("cancel-reservation")]
        public async Task<ActionResult> CancelReservationAsync([FromBody] ReserveRequest request)
        {
            await _mediator.Send(new CancelReservationCommand(request.ItemsToReserve));
            return NoContent();
        }

        [HttpPost]
        [Route("confirm-reservation")]
        public async Task<ActionResult> ConfirmShipmentAsync([FromBody] ReserveRequest request)
        {
            await _mediator.Send(new ConfirmShipmentCommand(request.ItemsToReserve));
            return NoContent();
        }
    }
}