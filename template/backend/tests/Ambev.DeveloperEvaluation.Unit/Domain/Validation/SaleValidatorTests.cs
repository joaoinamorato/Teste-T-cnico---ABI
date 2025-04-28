using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation
{
    public class SaleValidatorTests
    {
        private readonly SaleValidator _validator;

        public SaleValidatorTests()
        {
            _validator = new SaleValidator();
        }

        [Fact(DisplayName = "Valid sale should pass all validation rules")]
        public void Given_ValidUser_When_Validated_Then_ShouldNotHaveErrors()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
            sale.Items.Any(i => i.DiscountPerecentage == 0m).Should().BeTrue();
            sale.Items.Any(i => i.DiscountPerecentage == 0.10m).Should().BeTrue();
            sale.Items.Any(i => i.DiscountPerecentage == 0.20m).Should().BeTrue();
        }

        [Fact(DisplayName = "Invalid branch should fail validation")]
        public void Given_InvalidBranch_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.Branch.Name = string.Empty;

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Branch);
        }

        [Fact(DisplayName = "Invalid customer should fail validation")]
        public void Given_InvalidCustomer_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.Customer.Name = string.Empty;

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Branch);
        }

        [Fact(DisplayName = "Invalid product items fail validation")]
        public void Given_InvalidProductItems_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.Items.First().Product.Name = string.Empty; 

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Items);
        }

        [Fact(DisplayName = "Invalid quantity items should fail validation")]
        public void Given_InvalidQuantityItems_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.Items.First().Quantity = 0;

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Items);
        }
    }
}
