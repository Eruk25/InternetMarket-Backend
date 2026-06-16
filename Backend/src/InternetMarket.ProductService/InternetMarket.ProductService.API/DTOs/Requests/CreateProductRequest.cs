namespace InternetMarket.ProductService.API.DTOs.Requests
{
    public record CreateProductRequest(
        string ProductName,
        string Description,
        decimal Price,
        int Quantity,
        int Weight,
        int Length,
        int Width,
        int Height,
        Guid CategoryId,
        Guid ProviderId,
        string? ImageUrl = null);
}
