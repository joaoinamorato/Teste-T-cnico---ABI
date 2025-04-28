namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleCanceledEvent
    {
        public Guid SaleId { get; }

        public SaleCanceledEvent(Guid saleId)
        {
            SaleId = saleId;
        }
    }
}
