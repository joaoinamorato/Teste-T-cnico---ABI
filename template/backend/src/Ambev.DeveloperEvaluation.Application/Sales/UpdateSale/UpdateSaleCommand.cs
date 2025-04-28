using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public record UpdateSaleCommand(
        Guid Id,
        Guid BranchId,
        Guid CustomerId) : IRequest<UpdateSaleResult>
    {
        public ValidationResultDetail Validate()
        {
            var validator = new UpdateSaleValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
