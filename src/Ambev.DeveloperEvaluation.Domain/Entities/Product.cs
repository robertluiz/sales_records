using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Models;
using Ambev.DeveloperEvaluation.Domain.Validation;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a product in the system.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Gets or sets the product ID
    /// </summary>
    public new int Id { get; set; }
    
    /// <summary>
    /// Gets or sets the product code.
    /// Must not be null or empty.
    /// </summary>
    public string Code { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the product title.
    /// Must not be null or empty.
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the product name.
    /// Must not be null or empty.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the product description.
    /// Optional field providing additional details about the product.
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Gets or sets the product category.
    /// </summary>
    public string Category { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the product image URL.
    /// </summary>
    public string? Image { get; set; }
    
    /// <summary>
    /// Gets or sets the product price.
    /// Must be greater than zero.
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// Gets or sets the product rating information
    /// </summary>
    public ProductRating Rating { get; set; } = new();
    
    /// <summary>
    /// Gets or sets whether the product is active.
    /// Inactive products cannot be sold.
    /// </summary>
    public bool IsActive { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time when the product was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time of the last update to the product.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time when the product was deleted.
    /// Null if the product has not been deleted.
    /// </summary>
    public DateTime? DeletedAt { get; set; }
    
    /// <summary>
    /// Gets or sets the collection of sale items containing this product.
    /// </summary>
    public virtual ICollection<SaleItem> SaleItems { get; set; }

    /// <summary>
    /// Initializes a new instance of the Product class.
    /// </summary>
    public Product()
    {
        CreatedAt = DateTime.UtcNow;
        SaleItems = new HashSet<SaleItem>();
        IsActive = true;
        Rating = new ProductRating();
    }

    /// <summary>
    /// Activates the product.
    /// </summary>
    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Deactivates the product.
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the product price.
    /// </summary>
    /// <param name="newPrice">The new price to set.</param>
    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice <= 0)
            throw new ArgumentException("Price must be greater than zero", nameof(newPrice));

        Price = newPrice;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Validates the product
    /// </summary>
    /// <returns>The validation result</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new ProductValidator();
        var validationResult = validator.Validate(this);

        return new ValidationResultDetail
        {
            IsValid = validationResult.IsValid,
            Errors = validationResult.Errors.Select(e => (ValidationErrorDetail)e).ToList()
        };
    }
} 