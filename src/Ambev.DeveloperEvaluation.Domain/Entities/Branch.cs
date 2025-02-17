using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a branch (store/location) in the system.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Branch : BaseEntity
{
    /// <summary>
    /// Gets or sets the unique branch code.
    /// Must not be null or empty.
    /// </summary>
    public string Code { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the branch name.
    /// Must not be null or empty.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the branch address.
    /// Optional field providing location details.
    /// </summary>
    public string? Address { get; set; }
    
    /// <summary>
    /// Gets or sets whether the branch is active.
    /// Inactive branches cannot process sales.
    /// </summary>
    public bool IsActive { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time when the branch was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time of the last update to the branch.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
    
    /// <summary>
    /// Gets or sets the collection of sales made at this branch.
    /// </summary>
    public virtual ICollection<Sale> Sales { get; set; }

    /// <summary>
    /// Initializes a new instance of the Branch class.
    /// </summary>
    public Branch()
    {
        CreatedAt = DateTime.UtcNow;
        Sales = new List<Sale>();
        IsActive = true;
    }

    /// <summary>
    /// Activates the branch.
    /// </summary>
    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Deactivates the branch.
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the branch address.
    /// </summary>
    /// <param name="newAddress">The new address to set.</param>
    public void UpdateAddress(string newAddress)
    {
        Address = newAddress;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Performs validation of the branch using the BranchValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing validation results.
    /// </returns>
    public ValidationResultDetail Validate()
    {
        var validator = new BranchValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
} 