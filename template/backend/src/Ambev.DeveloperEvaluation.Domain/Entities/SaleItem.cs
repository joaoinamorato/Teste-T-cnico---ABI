using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        public Guid SaleId { get; set; }
        public Sale? Sale { get; set; }
        public ProductInfo Product { get; set; } = new();
        public int Quantity { get; set; }
        public decimal DiscountPerecentage { get; set; }
        public bool IsCancelled { get; set; }
        public decimal Total => CalculateTotal();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public SaleItem()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public void CalculateDiscountPercentage()
        {
            if (new TwentyPercentDiscountSpecification().IsSatisfiedBy(Quantity))
            {
                DiscountPerecentage = 0.20m;
                return;
            }

            if (new TenPercentDiscountSpecification().IsSatisfiedBy(Quantity))
            {
                DiscountPerecentage = 0.10m;
                return;
            }

            DiscountPerecentage = 0.0m;
        }

        private decimal CalculateTotal()
        {
            var gross = Quantity * Product.UnitPrice;
            return gross - (gross * DiscountPerecentage);
        }
    }
}
