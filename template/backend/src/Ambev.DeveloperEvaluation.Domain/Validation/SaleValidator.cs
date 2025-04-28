using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(sale => sale.Branch)
            .NotNull().WithMessage("Branch cannot be null")
            .ChildRules(branch =>
            {
                branch.RuleFor(b => b.Id)
                    .NotEmpty().WithMessage("Branch Id cannot be empty");

                branch.RuleFor(b => b.Name)
                    .NotEmpty().WithMessage("Branch Name cannot be empty");
            });

        RuleFor(sale => sale.Customer)
            .NotNull().WithMessage("Customer cannot be null")
            .ChildRules(customer =>
            {
                customer.RuleFor(b => b.Id)
                    .NotEmpty().WithMessage("Customer Id cannot be empty");

                customer.RuleFor(b => b.Name)
                    .NotEmpty().WithMessage("Customer Name cannot be empty");
            });

        RuleFor(sale => sale.Items)
            .NotNull().WithMessage("Items cannot be null")
            .NotEmpty().WithMessage("Items cannot be empty")
            .ForEach(item =>
            {
                item.NotNull().WithMessage("Item cannot be null");

                item.ChildRules(saleItem =>
                {
                    saleItem.RuleFor(i => i.Product)
                        .NotNull().WithMessage("Product cannot be null")
                        .ChildRules(product =>
                        {
                            product.RuleFor(b => b.Id)
                                .NotEmpty().WithMessage("Product Id cannot be empty");

                            product.RuleFor(b => b.Name)
                                .NotEmpty().WithMessage("Product Name cannot be empty");
                        });

                    saleItem.RuleFor(i => i.Quantity)
                        .GreaterThan(0).WithMessage("Quantity must be greater than 0")
                        .LessThanOrEqualTo(20).WithMessage("Quantity cannot be greater than 20");
                });
            });
    }
}
