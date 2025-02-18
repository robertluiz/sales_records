using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSaleFeature;

/// <summary>
/// Validator for UpdateSaleRequest
/// </summary>
public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleRequestValidator"/> class
    /// </summary>
    public UpdateSaleRequestValidator()
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
            .SetValidator(new UpdateSaleItemRequestValidator());
    }
}

/// <summary>
/// Validator for UpdateSaleItemRequest
/// </summary>
public class UpdateSaleItemRequestValidator : AbstractValidator<UpdateSaleItemRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleItemRequestValidator"/> class
    /// </summary>
    public UpdateSaleItemRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("O ID do produto é obrigatório");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("A quantidade deve ser maior que zero");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0)
            .WithMessage("O preço unitário deve ser maior que zero");
    }
} 