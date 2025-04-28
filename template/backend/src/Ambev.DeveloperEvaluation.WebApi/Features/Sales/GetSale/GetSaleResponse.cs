using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    /// <summary>
    /// API response model for GetSale operation
    /// </summary>
    public class GetSaleResponse
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

        /// <summary>
        /// The items of the sale
        /// </summary> 
        public IEnumerable<GetSaleItemResponse> Items { get; set; } = [];

        /// <summary>
        /// The total value of the sale
        /// </summary> 
        public decimal TotalAmount { get; set; }
    }

    /// <summary>
    /// API SaleItem response model for GetSale operation
    /// </summary>
    public class GetSaleItemResponse
    {
        /// <summary>
        /// The unique identifier of the saleItem
        /// </summary> 
        public Guid Id { get; set; }

        /// <summary>
        /// The unique identifier of the sale
        /// </summary> 
        public Guid SaleId { get; set; }

        /// <summary>
        /// The product of the saleItem
        /// </summary> 
        public ProductInfo Product { get; set; } = new();

        /// <summary>
        /// The quantity product of the saleItem
        /// </summary> 
        public int Quantity { get; set; }

        /// <summary>
        /// The discount percentage of the saleItem
        /// </summary> 
        public decimal DiscountPerecentage { get; set; }

        /// <summary>
        /// The cancelation status of the saleItem
        /// </summary> 
        public bool IsCancelled { get; set; }

        /// <summary>
        /// The total value of the saleItem with discounts
        /// </summary> 
        public decimal Total { get; set; }
    }
}
