using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public interface IProductService
    {
        Task<ProductInfo?> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken);
    }
}
