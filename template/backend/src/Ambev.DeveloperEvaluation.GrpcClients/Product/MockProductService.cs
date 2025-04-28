using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.GrpcClients.Product
{
    public class MockProductService : IProductService
    {
        private readonly List<ProductInfo> _mockProducts = new()
    {
        new ProductInfo { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Produto 1", UnitPrice = 30m },
        new ProductInfo { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Produto 2", UnitPrice = 50.99m },
        new ProductInfo { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Produto 3", UnitPrice = 100m },
    };

        public Task<ProductInfo?> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken)
        {
            var branch = _mockProducts.FirstOrDefault(b => b.Id == productId);
            return Task.FromResult(branch);
        }
    }

}
