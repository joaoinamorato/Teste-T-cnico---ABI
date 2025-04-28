namespace Ambev.DeveloperEvaluation.Domain.Specifications
{
    public class NoDiscountSpecification : ISpecification<int>
    {
        public bool IsSatisfiedBy(int quantity) => quantity < 4;
    }

    public class TenPercentDiscountSpecification : ISpecification<int>
    {
        public bool IsSatisfiedBy(int quantity) => quantity >= 4 && quantity < 10;
    }

    public class TwentyPercentDiscountSpecification : ISpecification<int>
    {
        public bool IsSatisfiedBy(int quantity) => quantity >= 10 && quantity <= 20;
    }

    public class OverLimitSpecification : ISpecification<int>
    {
        public bool IsSatisfiedBy(int quantity) => quantity > 20;
    }

}
