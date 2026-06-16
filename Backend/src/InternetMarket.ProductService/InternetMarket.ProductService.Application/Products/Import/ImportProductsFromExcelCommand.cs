using MediatR;

namespace InternetMarket.ProductService.Application.Products.Import
{
    public record ImportProductsFromExcelCommand(IEnumerable<ExcelProductRow> Products) : IRequest<int>;
}
