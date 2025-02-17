using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(sale => sale.Number)
            .NotEmpty()
            .MaxLength(50)
            .WithMessage("Sale number is required and cannot be longer than 50 characters.");

        RuleFor(sale => sale.CustomerId)
            .NotEmpty()
            .WithMessage("Customer is required.");

        RuleFor(sale => sale.BranchId)
            .NotEmpty()
            .WithMessage("Branch is required.");

        RuleFor(sale => sale.TotalAmount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Total amount cannot be negative.");

        RuleFor(sale => sale.CreatedAt)
            .NotEmpty()
            .WithMessage("Creation date is required.");

        RuleFor(sale => sale.CancelledAt)
            .Must((sale, cancelledAt) => !cancelledAt.HasValue || (cancelledAt.Value > sale.CreatedAt))
            .When(sale => sale.IsCancelled)
            .WithMessage("Cancellation date must be after creation date.");

        RuleFor(sale => sale.Items)
            .NotEmpty()
            .WithMessage("Sale must have at least one item.");

        RuleForEach(sale => sale.Items)
            .SetValidator(new SaleItemValidator());
    }
} 