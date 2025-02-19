using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Validator for the create product command
/// </summary>
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the validator with defined rules
    /// </summary>
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Product code is required")
            .MaximumLength(50)
            .WithMessage("Product code cannot be longer than 50 characters");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Product title is required")
            .MaximumLength(100)
            .WithMessage("Product title cannot be longer than 100 characters");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Product name is required")
            .MaximumLength(100)
            .WithMessage("Product name cannot be longer than 100 characters");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .When(x => !string.IsNullOrEmpty(x.Description))
            .WithMessage("Product description cannot be longer than 500 characters");

        RuleFor(x => x.Category)
            .NotEmpty()
            .WithMessage("Product category is required")
            .MaximumLength(50)
            .WithMessage("Product category cannot be longer than 50 characters");

        RuleFor(x => x.Image)
            .MaximumLength(500)
            .When(x => !string.IsNullOrEmpty(x.Image))
            .WithMessage("Product image URL cannot be longer than 500 characters");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Product price must be greater than zero");

        RuleFor(x => x.Rating.Rate)
            .InclusiveBetween(0, 5)
            .WithMessage("Product rating must be between 0 and 5");

        RuleFor(x => x.Rating.Count)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Product rating count must be greater than or equal to zero");
    }
} 