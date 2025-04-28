namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    /// <summary>
    /// Represents a request to update a sale in the system.
    /// </summary>
    public class UpdateSaleRequest
    {
        /// <summary>
        /// Sets the BranchId. Must be valid branch unique identifier.
        /// </summary>
        public Guid BranchId { get; set; }

        /// <summary>
        /// Sets the CustomerId. Must be valid customer unique identifier.
        /// </summary>
        public Guid CustomerId { get; set; }
    }
}
