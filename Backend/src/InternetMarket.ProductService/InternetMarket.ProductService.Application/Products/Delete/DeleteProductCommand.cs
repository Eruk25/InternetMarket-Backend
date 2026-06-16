using MediatR;

namespace InternetMarket.ProductService.Application.Products.Delete
{
    public record DeleteProductCommand(Guid Id) : IRequest<Unit>;
}
