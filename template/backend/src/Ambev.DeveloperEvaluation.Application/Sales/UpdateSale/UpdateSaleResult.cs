using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Response model for UpdateSale operation
    /// </summary>
    public class UpdateSaleResult
    {
        /// <summary>
        /// The unique identifier of the sale
        /// </summary> 
        public Guid Id { get; set; }

        /// <summary>
        /// The unique number of the sale
        /// </summary> 
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// The date of the sale
        /// </summary> 
        public DateTime Date { get; set; }

        /// <summary>
        /// The Customer of the sale
        /// </summary> 
        public CustomerInfo Customer { get; set; } = new();

        /// <summary>
        /// The Branch of the sale
        /// </summary> 
        public BranchInfo Branch { get; set; } = new();

        /// <summary>
        /// The cancelation status of the sale
        /// </summary> 
        public bool IsCancelled { get; set; }
    }
}
