using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleExemploDominioRico : BaseEntity
    {
        public string Number { get; private set; } = string.Empty;
        public DateTime Date { get; private set; } = DateTime.UtcNow;
        public CustomerInfo Customer { get; private set; } = new();
        public BranchInfo Branch { get; private set; } = new();
        public bool IsCancelled { get; private set; }

        private readonly List<SaleItemExemploDominioRico> _items = new();
        public IReadOnlyCollection<SaleItemExemploDominioRico> Items => _items.AsReadOnly();

        public decimal TotalAmount => _items.Where(i => !i.IsCancelled).Sum(i => i.Total);

        //EF Constructor 
        private SaleExemploDominioRico()
        { }

        public SaleExemploDominioRico(CustomerInfo customer, BranchInfo branch)
        {
            Number = GenerateUniqueNumber();
            Customer = customer;
            Branch = branch;
        }

        public void AddItem(ProductInfo product, int quantity, decimal unitPrice)
        {
            var item = new SaleItemExemploDominioRico(Id, product, quantity, unitPrice);
            _items.Add(item);
        }

        public void Cancel()
        {
            _items.ForEach(i => i.Cancel());    
            IsCancelled = true;
        }

        public void CancelItem(Guid itemId)
        {
            var item = _items.FirstOrDefault(i => i.Id == itemId)
                       ?? throw new DomainException("Item not found");

            item.Cancel();
        }

        private string GenerateUniqueNumber()
        {
            var datePart = DateTime.UtcNow.ToString("yyyyMMdd");
            var randomPart = Guid.NewGuid().ToString("N")[..6].ToUpper();
            return $"{datePart}{randomPart}";
        }
    }
}
