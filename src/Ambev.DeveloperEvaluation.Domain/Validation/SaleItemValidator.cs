using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator for the SaleItem entity
/// </summary>
public class SaleItemValidator : AbstractValidator<SaleItem>
{
    /// <summary>
    /// Initializes a new instance of the validator with defined rules
    /// </summary>
    public SaleItemValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0)
            .WithMessage("Product ID must be greater than zero");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero")
            .LessThanOrEqualTo(20)
            .WithMessage("Maximum quantity per item is 20");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0)
            .WithMessage("Unit price must be greater than zero");

        // Regra de validação para desconto baseado na quantidade
        RuleFor(x => x)
            .Custom((item, context) =>
            {
                decimal discountPercentage = 0;

                if (item.Quantity >= 10 && item.Quantity <= 20)
                {
                    discountPercentage = 20;
                }
                else if (item.Quantity >= 4)
                {
                    discountPercentage = 10;
                }
                else if (item.DiscountPercentage > 0)
                {
                    context.AddFailure("Discount is not allowed for quantities below 4 items");
                }

                if (item.DiscountPercentage > discountPercentage)
                {
                    context.AddFailure($"Maximum discount allowed for quantity {item.Quantity} is {discountPercentage}%");
                }
            });

        RuleFor(x => x.CreatedAt)
            .NotEmpty();

        RuleFor(x => x.CancelledAt)
            .Must((item, cancelledAt) => !item.IsCancelled || cancelledAt.HasValue)
            .When(item => item.IsCancelled)
            .WithMessage("Cancellation date is required when item is cancelled");

        RuleFor(x => x.CancelledAt)
            .Must((item, cancelledAt) => !cancelledAt.HasValue || (cancelledAt.Value > item.CreatedAt))
            .When(item => item.CancelledAt.HasValue)
            .WithMessage("Cancellation date must be after creation date");
    }
} 