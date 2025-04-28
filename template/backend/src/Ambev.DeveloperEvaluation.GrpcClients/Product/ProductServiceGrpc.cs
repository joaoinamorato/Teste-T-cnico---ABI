using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Grpc.Protos;
using Grpc.Core;
using System.Globalization;

namespace Ambev.DeveloperEvaluation.GrpcClients.Product
{
    public class ProductServiceGrpc : IProductService
    {
        private readonly ProductGrpc.ProductGrpcClient _client;

        public ProductServiceGrpc(ProductGrpc.ProductGrpcClient client)
        {
            _client = client;
        }

        public async Task<ProductInfo?> GetProductByIdAsync(Guid customerId, CancellationToken cancellationToken)
        {
            var request = new GetProductRequest { Id = customerId.ToString() };

            try
            {
                var response = await _client.GetProductAsync(request, cancellationToken: cancellationToken);
                return new ProductInfo
                {
                    Id = Guid.Parse(response.Id),
                    Name = response.Name,
                    UnitPrice = decimal.Parse(response.Price, CultureInfo.InvariantCulture)
                };
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
            {
                return null;
            }
        }
    }
}
