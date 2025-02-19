using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a sale transaction in the system.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// Gets or sets the unique sale number.
    /// Must not be null or empty.
    /// </summary>
    public string Number { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the date and time when the sale was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time of the sale.
    /// </summary>
    public DateTime SaleDate { get; set; }
    
    /// <summary>
    /// Gets or sets the customer ID associated with this sale.
    /// </summary>
    public Guid CustomerId { get; set; }
    
    /// <summary>
    /// Gets or sets the customer who made the purchase.
    /// </summary>
    public virtual User Customer { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the branch ID where the sale was made.
    /// </summary>
    public Guid BranchId { get; set; }
    
    /// <summary>
    /// Gets or sets the branch where the sale was made.
    /// </summary>
    public virtual Branch Branch { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the subtotal amount before discounts.
    /// </summary>
    public decimal Subtotal { get; set; }
    
    /// <summary>
    /// Gets or sets the total discount amount.
    /// </summary>
    public decimal DiscountAmount { get; set; }
    
    /// <summary>
    /// Gets or sets the total amount after discounts.
    /// </summary>
    public decimal TotalAmount { get; set; }
    
    /// <summary>
    /// Gets or sets whether the sale has been cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time when the sale was cancelled.
    /// </summary>
    public DateTime? CancelledAt { get; set; }
    
    /// <summary>
    /// Gets or sets the collection of items in this sale.
    /// </summary>
    public virtual ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();

    /// <summary>
    /// Initializes a new instance of the Sale class.
    /// </summary>
    public Sale()
    {
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Calculates the total amounts for the sale.
    /// </summary>
    public void CalculateTotals()
    {
        Subtotal = 0;
        DiscountAmount = 0;
        TotalAmount = 0;

        var activeItems = Items.Where(i => !i.IsCancelled);
        foreach (var item in activeItems)
        {
            item.CalculateDiscount();
            Subtotal += item.Subtotal;
            DiscountAmount += item.DiscountAmount;
            TotalAmount += item.Total;
        }
    }
    
    /// <summary>
    /// Cancels the sale and records the cancellation time.
    /// </summary>
    public void Cancel()
    {
        if (!IsCancelled)
        {
            IsCancelled = true;
            CancelledAt = DateTime.UtcNow;

            foreach (var item in Items)
            {
                item.Cancel();
            }
        }
    }

    /// <summary>
    /// Cancels a specific item in the sale.
    /// </summary>
    /// <param name="itemId">The ID of the item to cancel.</param>
    public void CancelItem(Guid itemId)
    {
        var item = Items.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
        {
            throw new InvalidOperationException("Item not found");
        }
        
        item.Cancel();
        CalculateTotals();
    }

    /// <summary>
    /// Performs validation of the sale entity using the SaleValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing validation results.
    /// </returns>
    public ValidationResultDetail Validate()
    {
        var validator = new SaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
} 