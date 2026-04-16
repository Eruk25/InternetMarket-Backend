using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.ProductService.Application.Products.Get
{
    public record GetAllProductsQuery() : IRequest<IEnumerable<ProductDto>>;
}