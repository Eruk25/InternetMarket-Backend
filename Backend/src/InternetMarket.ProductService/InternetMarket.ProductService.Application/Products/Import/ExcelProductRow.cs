namespace InternetMarket.ProductService.Application.Products.Import
{
    public record ExcelProductRow(
        string ProductName,
        string Description,
        decimal Price,
        int Quantity,
        int Weight,
        int Length,
        int Width,
        int Height,
        string CategoryName,
        string ProviderName,
        string? ImageUrl = null);
}
