using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.ProductService.API.DTOs.Requests
{
    public record GetByIdsRequest(IEnumerable<Guid> Ids);
}