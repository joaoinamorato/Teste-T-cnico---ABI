using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class SaleTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// The generated users will have valid:
    /// - BranchId (valid unique identifier of branch)
    /// - CustomerId (valid unique identifier of customer)
    /// - Items (valid list of unique products ids)
    /// </summary>
    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(u => u.Branch, f => new BranchInfo { Id = Guid.NewGuid(), Name = f.Company.CompanyName() })
        .RuleFor(u => u.Customer, f => new CustomerInfo { Id = Guid.NewGuid(), Name = f.Internet.UserName() });

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static Sale GenerateValidSale()
    {
        var sale =  SaleFaker.Generate();
        sale.Items.Add(GenerateValidNoDiscountSaleItem(sale.Id));
        sale.Items.Add(GenerateValidTwentyPercentDiscountSaleItem(sale.Id));
        sale.Items.Add(GenerateValidTenPercentDiscountSaleItem(sale.Id));

        return sale;
    }

    public static SaleItem GenerateValidNoDiscountSaleItem(Guid saleId)
    {
        var faker = new Faker();
        var saleItem = new SaleItem
        {
            SaleId = saleId,
            Product = new ProductInfo
            {
                Id = Guid.NewGuid(),
                Name = faker.Commerce.ProductName(),
                UnitPrice = new Random().Next(100)
            },
            Quantity = new Random().Next(1,3)
        };

        saleItem.CalculateDiscountPercentage();
        return saleItem;
    }

    public static SaleItem GenerateValidTwentyPercentDiscountSaleItem(Guid saleId)
    {
        var faker = new Faker();
        var saleItem = new SaleItem
        {
            SaleId = saleId,
            Product = new ProductInfo
            {
                Id = Guid.NewGuid(),
                Name = faker.Commerce.ProductName(),
                UnitPrice = new Random().Next(100)
            },
            Quantity = new Random().Next(10, 20)
        };

        saleItem.CalculateDiscountPercentage();
        return saleItem;
    }

    public static SaleItem GenerateValidTenPercentDiscountSaleItem(Guid saleId)
    {
        var faker = new Faker();
        var saleItem = new SaleItem
        {
            SaleId = saleId,
            Product = new ProductInfo
            {
                Id = Guid.NewGuid(),
                Name = faker.Commerce.ProductName(),
                UnitPrice = new Random().Next(100)
            },
            Quantity = new Random().Next(4, 9)
        };

        saleItem.CalculateDiscountPercentage();
        return saleItem;
    }
}
