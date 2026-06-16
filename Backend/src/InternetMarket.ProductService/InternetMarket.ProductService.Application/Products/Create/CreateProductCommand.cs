using MediatR;

namespace InternetMarket.ProductService.Application.Products.Create
{
    public record CreateProductCommand(
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
        string? ImageUrl = null) : IRequest<ProductDto>;
}
