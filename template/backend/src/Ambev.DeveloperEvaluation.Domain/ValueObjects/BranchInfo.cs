namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    /// <summary>
    /// The Branch value object
    /// </summary> 
    public class BranchInfo
    {
        /// <summary>
        /// The unique identifier of the Branch
        /// </summary> 
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the Branch
        /// </summary> 
        public string Name { get; set; } = string.Empty;
    }
}
