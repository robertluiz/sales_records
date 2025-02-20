using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSaleFeature;

/// <summary>
/// Validator for CreateSaleRequest
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleRequestValidator"/> class
    /// </summary>
    public CreateSaleRequestValidator()
    {
        RuleFor(x => x.BranchId)
            .NotEmpty()
            .WithMessage("O ID da filial é obrigatório");

        RuleFor(x => x.SaleDate)
            .NotEmpty()
            .WithMessage("A data da venda é obrigatória")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("A data da venda não pode ser futura");

        RuleFor(x => x.Items)
            .NotEmpty()
            .WithMessage("A venda deve conter pelo menos um item");

        RuleForEach(x => x.Items)
            .SetValidator(new SaleItemRequestValidator());
    }
}

/// <summary>
/// Validator for SaleItemRequest
/// </summary>
public class SaleItemRequestValidator : AbstractValidator<SaleItemRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SaleItemRequestValidator"/> class
    /// </summary>
    public SaleItemRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("O ID do produto é obrigatório");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("A quantidade deve ser maior que zero");

    }
} 