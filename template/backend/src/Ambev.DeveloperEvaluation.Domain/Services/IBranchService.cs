using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public interface IBranchService
    {
        Task<BranchInfo?> GetBranchByIdAsync(Guid branchId, CancellationToken cancellationToken);
    }
}
