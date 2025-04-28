using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItemExemploDominioRico : BaseEntity
    {
        public Guid SaleId { get; private set; }
        public SaleExemploDominioRico? Sale { get; private set; }
        public ProductInfo Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal DiscountPerecentage { get; private set; }
        public decimal Total => CalculateTotal();
        public bool IsCancelled { get; private set; }

        public SaleItemExemploDominioRico(Guid saleId, ProductInfo product, int quantity, decimal unitPrice)
        {
            if (new OverLimitSpecification().IsSatisfiedBy(quantity))
                throw new InvalidOperationException("Cannot sell more than 20 identical items");
            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than zero.");

            SaleId = saleId;
            Product = product;
            Quantity = quantity;
            UnitPrice = unitPrice;
            DiscountPerecentage = CalculateDiscountPercentage(quantity);
        }

        public void Cancel()
        {
            if (!IsCancelled)
                IsCancelled = true;
        }

        private decimal CalculateDiscountPercentage(int quantity)
        {
            if (new TwentyPercentDiscountSpecification().IsSatisfiedBy(quantity))
                return 0.20m;

            if (new TenPercentDiscountSpecification().IsSatisfiedBy(quantity))
                return 0.10m;

            return 0.0m;
        }

        private decimal CalculateTotal()
        {
            var gross = Quantity * UnitPrice;
            return gross - (gross * DiscountPerecentage);
        }
    }
}
