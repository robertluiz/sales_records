using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSaleFeature;

/// <summary>
/// Validator for DeleteSaleRequest
/// </summary>
public class DeleteSaleRequestValidator : AbstractValidator<DeleteSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of DeleteSaleRequestValidator
    /// </summary>
    public DeleteSaleRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("O ID da venda é obrigatório");
    }
} 