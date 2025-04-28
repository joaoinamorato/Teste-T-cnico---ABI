using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public interface ICustomerService
    {
        Task<CustomerInfo?> GetCustomerByIdAsync(Guid customerId, CancellationToken cancellationToken);
    }
}
