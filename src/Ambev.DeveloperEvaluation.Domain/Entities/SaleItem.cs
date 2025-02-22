using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents an item within a sale transaction.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class SaleItem : BaseEntity
{
    /// <summary>
    /// Gets or sets the sale ID this item belongs to.
    /// </summary>
    public Guid SaleId { get; set; }
    
    /// <summary>
    /// Gets or sets the sale this item belongs to.
    /// </summary>
    public virtual Sale Sale { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the product ID for this item.
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// Gets or sets the product associated with this item.
    /// </summary>
    public virtual Product Product { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the quantity of the product.
    /// Must be between 1 and 20 units.
    /// </summary>
    public int Quantity { get; set; }
    
    /// <summary>
    /// Gets or sets the unit price of the product at the time of sale.
    /// </summary>
    public decimal UnitPrice { get; set; }
    
    /// <summary>
    /// Gets or sets the discount percentage (0-100)
    /// </summary>
    public decimal DiscountPercentage { get; set; }
    
    /// <summary>
    /// Gets or sets the discount amount
    /// </summary>
    public decimal DiscountAmount { get; set; }
    
    /// <summary>
    /// Gets or sets the subtotal before discount (quantity * unit price)
    /// </summary>
    public decimal Subtotal { get; set; }
    
    /// <summary>
    /// Gets or sets the total after discount
    /// </summary>
    public decimal Total { get; set; }
    
    /// <summary>
    /// Gets or sets whether this item has been cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time when this item was cancelled.
    /// </summary>
    public DateTime? CancelledAt { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time when this item was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Initializes a new instance of the SaleItem class.
    /// </summary>
    public SaleItem()
    {
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Calculates the discount based on quantity
    /// </summary>
    public void CalculateDiscount()
    {
        if (Quantity > 20)
        {
            throw new InvalidOperationException("It's not possible to sell above 20 identical items");
        }

        Subtotal = Quantity * UnitPrice;

        if (Quantity >= 10 && Quantity <= 20)
        {
            DiscountPercentage = 20;
        }
        else if (Quantity >= 4)
        {
            DiscountPercentage = 10;
        }
        else if (DiscountPercentage > 0)
        {
            throw new InvalidOperationException("Discount is not allowed for quantities below 4 items");
        }

        DiscountAmount = Subtotal * (DiscountPercentage / 100);
        Total = Subtotal - DiscountAmount;
    }

    /// <summary>
    /// Cancels this item and records the cancellation time.
    /// </summary>
    public void Cancel()
    {
        if (!IsCancelled)
        {
            IsCancelled = true;
            CancelledAt = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// Validates the business rules for this item.
    /// </summary>
    /// <returns>True if the item is valid, false otherwise.</returns>
    public bool Validate()
    {
        if (Quantity > 20)
            return false;

        if (Quantity < 4 && DiscountPercentage > 0)
            return false;

        return true;
    }

    /// <summary>
    /// Performs validation of the sale item using the SaleItemValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing validation results.
    /// </returns>
    public ValidationResultDetail ValidateDetails()
    {
        var validator = new SaleItemValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
} 