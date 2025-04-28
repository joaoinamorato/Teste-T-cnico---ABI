namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Represents a request to create a new sale in the system.
    /// </summary>
    public class CreateSaleRequest
    {
        /// <summary>
        /// Gets or sets the BranchId. Must be valid branch unique identifier.
        /// </summary>
        public Guid BranchId { get; set; }

        /// <summary>
        /// Gets or sets the CustomerId. Must be valid customer unique identifier.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the Items. Must be greatest of 0.
        /// </summary>
        public IEnumerable<CreateSaleItemRequest> Items { get; set; } = [];
    }

    /// <summary>
    /// Represents a request to create a new sale in the system.
    /// </summary>
    public class CreateSaleItemRequest
    {
        /// <summary>
        /// Gets or sets the ProductId. Must be valid product unique identifier.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the Quantity. Not be greatest of 0.
        /// </summary>
        public int Quantity { get; set; }

    }
}
