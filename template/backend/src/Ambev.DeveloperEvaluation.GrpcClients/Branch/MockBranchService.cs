using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.GrpcClients.Branch
{
    public class MockBranchService : IBranchService
    {
        private readonly List<BranchInfo> _mockBranches = new()
        {
            new BranchInfo { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Filial 1" },
            new BranchInfo { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Filial 2" },
            new BranchInfo { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Filial 3" },
        };

        public Task<BranchInfo?> GetBranchByIdAsync(Guid branchId, CancellationToken cancellationToken)
        {
            var branch = _mockBranches.FirstOrDefault(b => b.Id == branchId);
            return Task.FromResult(branch);
        }
    }

}
