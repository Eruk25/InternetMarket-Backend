using System;
using System.Collections.Generic;
using System.Linq;

namespace InternetMarket.ProductService.Application.Abstractions.Caching
{
    public static class ProductCacheKeys
    {
        public const string GetAll = "Product:GetAll";

        public static string GetById(Guid id) => $"Product:GetById:{id}";

        public static string GetByIds(IEnumerable<Guid> ids) =>
            $"Product:GetByIds:{string.Join(",", ids.OrderBy(id => id))}";

        public static string GetPaged(int page, int pageSize) =>
            $"Product:GetPaged:{page}:{pageSize}";
    }
}
