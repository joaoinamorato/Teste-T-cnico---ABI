using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Grpc.Protos;
using Grpc.Core;

namespace Ambev.DeveloperEvaluation.GrpcClients.Branch
{
    public class BranchServiceGrpc : IBranchService
    {
        private readonly BranchGrpc.BranchGrpcClient _client;

        public BranchServiceGrpc(BranchGrpc.BranchGrpcClient client)
        {
            _client = client;
        }

        public async Task<BranchInfo?> GetBranchByIdAsync(Guid branchId, CancellationToken cancellationToken)
        {
            var request = new GetBranchRequest { Id = branchId.ToString() };

            try
            {
                var response = await _client.GetBranchAsync(request, cancellationToken: cancellationToken);
                return new BranchInfo
                {
                    Id = Guid.Parse(response.Id),
                    Name = response.Name
                };
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
            {
                return null;
            }
        }
    }
}
