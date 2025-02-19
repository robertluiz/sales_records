using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSaleFeature;

/// <summary>
/// Validator for the update sale request
/// </summary>
public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the validator with defined rules
    /// </summary>
    public UpdateSaleRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required");

        When(x => x.Observation != null, () =>
        {
            RuleFor(x => x.Observation)
                .MaximumLength(500)
                .WithMessage("Observation cannot be longer than 500 characters");
        });

        When(x => x.Items != null, () =>
        {
            RuleFor(x => x.Items)
                .NotEmpty()
                .WithMessage("Items list cannot be empty when provided")
                .ForEach(item =>
                {
                    item.SetValidator(new UpdateSaleItemRequestValidator());
                });
        });
    }
}

/// <summary>
/// Validator for the update sale item request
/// </summary>
public class UpdateSaleItemRequestValidator : AbstractValidator<UpdateSaleItemRequest>
{
    /// <summary>
    /// Initializes a new instance of the validator with defined rules
    /// </summary>
    public UpdateSaleItemRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Item ID is required");

        When(x => x.Observation != null, () =>
        {
            RuleFor(x => x.Observation)
                .MaximumLength(200)
                .WithMessage("Observation cannot be longer than 200 characters");
        });
    }
} 