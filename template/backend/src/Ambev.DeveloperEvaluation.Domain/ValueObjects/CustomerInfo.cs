namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    /// <summary>
    /// The Customer value object
    /// </summary> 
    public class CustomerInfo
    {
        /// <summary>
        /// The unique identifier of the customer
        /// </summary> 
        public Guid Id { get; set; }

        /// <summary>
        /// The Name of the customer
        /// </summary> 
        public string Name { get; set; } = string.Empty;
    }
}
