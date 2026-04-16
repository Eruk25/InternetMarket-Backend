using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.ProductService.Application.Products.Get.GetById
{
    public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;
}