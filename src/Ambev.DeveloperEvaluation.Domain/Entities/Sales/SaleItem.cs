using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Sales;

public class SaleItem
{
    public SaleItem()
    {
        CreatedAt = DateTime.UtcNow;
    }

    [Key]
    public Guid Id { get; set; }
    
    public Guid SaleId { get; set; }
    
    public virtual Sale Sale { get; set; } = null!;
    
    public Guid ProductId { get; set; }
    
    public virtual Product Product { get; set; } = null!;
    
    public int Quantity { get; set; }
    
    public decimal UnitPrice { get; set; }
    
    public decimal DiscountPercentage { get; set; }
    
    public decimal TotalAmount { get; set; }
    
    public bool IsCancelled { get; set; }
    
    public DateTime? CancelledAt { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public void CalculateDiscount()
    {
        if (Quantity < 4)
        {
            DiscountPercentage = 0;
        }
        else if (Quantity >= 4 && Quantity < 10)
        {
            DiscountPercentage = 10;
        }
        else if (Quantity >= 10 && Quantity <= 20)
        {
            DiscountPercentage = 20;
        }
    }

    public void CalculateTotalAmount()
    {
        decimal subtotal = Quantity * UnitPrice;
        decimal discount = subtotal * (DiscountPercentage / 100);
        TotalAmount = subtotal - discount;
    }

    public void Cancel()
    {
        if (!IsCancelled)
        {
            IsCancelled = true;
            CancelledAt = DateTime.UtcNow;
        }
    }

    public bool Validate()
    {
        if (Quantity > 20)
            return false;

        if (Quantity < 4 && DiscountPercentage > 0)
            return false;

        return true;
    }
} 