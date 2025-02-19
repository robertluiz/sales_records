using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator for the Sale entity
/// </summary>
public class SaleValidator : AbstractValidator<Sale>
{
    /// <summary>
    /// Initializes a new instance of the validator with defined rules
    /// </summary>
    public SaleValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty()
            .WithMessage("Sale number is required");

        RuleFor(x => x.BranchId)
            .NotEmpty()
            .WithMessage("Branch ID is required");

        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("Customer ID is required");

        RuleFor(x => x.SaleDate)
            .NotEmpty()
            .WithMessage("Sale date is required")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Sale date cannot be in the future");

        RuleFor(x => x.Items)
            .NotEmpty()
            .WithMessage("At least one item is required");

        RuleForEach(x => x.Items)
            .SetValidator(new SaleItemValidator());

        RuleFor(x => x.CreatedAt)
            .NotEmpty();

        RuleFor(x => x.CancelledAt)
            .Must((sale, cancelledAt) => !cancelledAt.HasValue || (cancelledAt.Value > sale.CreatedAt))
            .When(sale => sale.IsCancelled)
            .WithMessage("Cancellation date must be after creation date");
    }
} 