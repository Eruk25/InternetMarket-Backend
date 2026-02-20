using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.ProductService.Application.Products.Get.GetByIds
{
    public record GetProductsByIdsQuery(IEnumerable<Guid> Ids) : IRequest<IEnumerable<ProductDto>>;
}