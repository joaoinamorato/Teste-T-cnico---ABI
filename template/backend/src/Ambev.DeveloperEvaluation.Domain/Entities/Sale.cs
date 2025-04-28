using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public string Number { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public CustomerInfo Customer { get; set; } = new();
        public BranchInfo Branch { get; set; } = new();
        public bool IsCancelled { get; set; }
        public ICollection<SaleItem> Items { get; set; } = [];
        public decimal TotalAmount => Items.Where(i => !i.IsCancelled).Sum(i => i.Total);
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Sale()
        {
            Number = GenerateUniqueNumber();
            Date = DateTime.UtcNow;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            foreach (var item in Items)
            {
                item.IsCancelled = true;
            }

            UpdatedAt = DateTime.UtcNow;
            IsCancelled = true;
        }

        private string GenerateUniqueNumber()
        {
            var datePart = DateTime.UtcNow.ToString("yyyyMMdd");
            var randomPart = Guid.NewGuid().ToString("N")[..6].ToUpper();
            return $"{datePart}{randomPart}";
        }
    }
}
