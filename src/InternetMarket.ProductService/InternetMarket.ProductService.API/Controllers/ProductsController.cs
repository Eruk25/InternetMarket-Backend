using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ProductService.API.DTOs.Requests;
using InternetMarket.ProductService.Application.Products;
using InternetMarket.ProductService.Application.Products.Get;
using InternetMarket.ProductService.Application.Products.Get.GetById;
using InternetMarket.ProductService.Application.Products.Get.GetByIds;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IEnumerable<ProductDto>> GetByIdsAsync([FromBody] GetByIdsRequest request)
        {
            var products = await _mediator.Send(new GetProductsByIdsQuery(request.Ids));
            return products;
        }

    }
}