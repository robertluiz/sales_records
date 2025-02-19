using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

public class SaleItemValidatorTests
{
    private readonly SaleItemValidator _validator;

    public SaleItemValidatorTests()
    {
        _validator = new SaleItemValidator();
    }

    [Fact(DisplayName = "Item de venda válido deve passar na validação")]
    public void Given_ValidSaleItem_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            ProductId = 1,
            Quantity = 10,
            UnitPrice = 50.00m,
            CreatedAt = DateTime.UtcNow.AddHours(-1)
        };

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory(DisplayName = "ProductId inválido deve falhar na validação")]
    [InlineData(0)]
    [InlineData(-1)]
    public void Given_InvalidProductId_When_Validated_Then_ShouldHaveError(int productId)
    {
        // Arrange
        var saleItem = new SaleItem 
        { 
            ProductId = productId,
            Quantity = 1,
            UnitPrice = 10.00m,
            CreatedAt = DateTime.UtcNow.AddHours(-1)
        };

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ProductId);
    }

    [Theory(DisplayName = "Quantidade inválida deve falhar na validação")]
    [InlineData(0)]
    [InlineData(-1)]
    public void Given_InvalidQuantity_When_Validated_Then_ShouldHaveError(int quantity)
    {
        // Arrange
        var saleItem = new SaleItem 
        { 
            ProductId = 1,
            Quantity = quantity,
            UnitPrice = 10.00m,
            CreatedAt = DateTime.UtcNow.AddHours(-1)
        };

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Quantity);
    }

    [Theory(DisplayName = "Preço unitário inválido deve falhar na validação")]
    [InlineData(0)]
    [InlineData(-1)]
    public void Given_InvalidUnitPrice_When_Validated_Then_ShouldHaveError(decimal unitPrice)
    {
        // Arrange
        var saleItem = new SaleItem 
        { 
            ProductId = 1,
            Quantity = 1,
            UnitPrice = unitPrice,
            CreatedAt = DateTime.UtcNow.AddHours(-1)
        };

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UnitPrice);
    }

    [Fact(DisplayName = "Data de cancelamento anterior à criação deve falhar na validação")]
    public void Given_CancelledDateBeforeCreation_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            ProductId = 1,
            Quantity = 1,
            UnitPrice = 10.00m,
            CreatedAt = DateTime.UtcNow,
            CancelledAt = DateTime.UtcNow.AddHours(-1),
            IsCancelled = true
        };

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CancelledAt);
    }

    [Fact(DisplayName = "Item cancelado sem data de cancelamento deve falhar na validação")]
    public void Given_CancelledWithoutDate_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            ProductId = 1,
            Quantity = 1,
            UnitPrice = 10.00m,
            CreatedAt = DateTime.UtcNow.AddHours(-1),
            IsCancelled = true,
            CancelledAt = null
        };

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CancelledAt);
    }
} 