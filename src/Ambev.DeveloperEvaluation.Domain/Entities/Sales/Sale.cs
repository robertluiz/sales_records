using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Sales;

public class Sale
{
    public Sale()
    {
        Items = new List<SaleItem>();
        CreatedAt = DateTime.UtcNow;
    }

    [Key]
    public Guid Id { get; set; }
    
    public string Number { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
    
    public Guid CustomerId { get; set; }
    
    public virtual User Customer { get; set; } = null!;
    
    public Guid BranchId { get; set; }
    
    public virtual Branch Branch { get; set; } = null!;
    
    public decimal TotalAmount { get; set; }
    
    public bool IsCancelled { get; set; }
    
    public DateTime? CancelledAt { get; set; }
    
    public virtual ICollection<SaleItem> Items { get; set; }
    
    public void CalculateTotalAmount()
    {
        TotalAmount = Items.Sum(item => item.TotalAmount);
    }
    
    public void Cancel()
    {
        if (!IsCancelled)
        {
            IsCancelled = true;
            CancelledAt = DateTime.UtcNow;
        }
    }
} 