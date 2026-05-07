using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.ProductService.Application.Products.Update.Reserve
{
    public record ReserveProductCommand(Dictionary<Guid, int> ItemsToReserve) : IRequest;
}