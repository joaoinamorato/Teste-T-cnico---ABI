namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class ProductInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
    }
}
