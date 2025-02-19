using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(product => product.Code)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(product => product.Title)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(product => product.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(product => product.Description)
            .MaximumLength(500)
            .When(product => !string.IsNullOrEmpty(product.Description));

        RuleFor(product => product.Category)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(product => product.Image)
            .MaximumLength(500)
            .When(product => !string.IsNullOrEmpty(product.Image));

        RuleFor(product => product.Price)
            .GreaterThan(0);

        When(product => product.Rating != null, () =>
        {
            RuleFor(product => product.Rating.Rate)
                .InclusiveBetween(0, 5);

            RuleFor(product => product.Rating.Count)
                .GreaterThanOrEqualTo(0);
        });

        RuleFor(product => product.CreatedAt)
            .NotEmpty();

        RuleFor(product => product.UpdatedAt)
            .Must((product, updatedAt) => !updatedAt.HasValue || (updatedAt.Value > product.CreatedAt))
            .When(product => product.UpdatedAt.HasValue)
            .WithMessage("Update date must be after creation date.");
    }
} 