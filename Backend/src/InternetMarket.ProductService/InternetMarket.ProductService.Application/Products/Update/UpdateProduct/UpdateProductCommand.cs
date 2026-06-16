using MediatR;

namespace InternetMarket.ProductService.Application.Products.Update
{
    public record UpdateProductCommand(
        Guid Id,
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
