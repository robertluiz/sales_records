using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

public class SaleValidatorTests
{
    private readonly SaleValidator _validator;

    public SaleValidatorTests()
    {
        _validator = new SaleValidator();
    }

    [Fact(DisplayName = "Valid sale must pass the validation")]
    public void Given_ValidSale_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var sale = new Sale
        {
            Number = "SALE-20240101",
            BranchId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            SaleDate = DateTime.UtcNow.AddHours(-1),
            Items = new List<SaleItem> 
            { 
                new() 
                {
                    ProductId = 1,
                    Quantity = 1,
                    UnitPrice = 10.00m,
                    CreatedAt = DateTime.UtcNow.AddHours(-2)
                } 
            },
            CreatedAt = DateTime.UtcNow.AddHours(-2)
        };

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory(DisplayName = "Invalid sale number should fail validation")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Given_InvalidNumber_When_Validated_Then_ShouldHaveError(string number)
    {
        // Arrange
        var sale = new Sale { Number = number };

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Number);
    }

    [Fact(DisplayName = "Empty BranchId should fail validation")]
    public void Given_EmptyBranchId_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var sale = new Sale { BranchId = Guid.Empty };

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.BranchId);
    }

    [Fact(DisplayName = "Empty CustomerId should fail validation")]
    public void Given_EmptyCustomerId_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var sale = new Sale { CustomerId = Guid.Empty };

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CustomerId);
    }

    [Fact(DisplayName = "Sale date in the future should fail validation")]
    public void Given_FutureSaleDate_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var sale = new Sale { SaleDate = DateTime.UtcNow.AddDays(1) };

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.SaleDate);
    }

    [Fact(DisplayName = "Sale without items should fail validation")]
    public void Given_NoItems_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var sale = new Sale { Items = new List<SaleItem>() };

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Items);
    }

    [Fact(DisplayName = "Cancellation date before creation date should fail validation")]
    public void Given_CancelledDateBeforeCreation_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var sale = new Sale
        {
            CreatedAt = DateTime.UtcNow,
            CancelledAt = DateTime.UtcNow.AddHours(-1),
            IsCancelled = true,
            Items = new List<SaleItem> 
            { 
                new() 
                {
                    ProductId = 1,
                    Quantity = 1,
                    UnitPrice = 10.00m,
                    CreatedAt = DateTime.UtcNow.AddHours(-2)
                } 
            }
        };

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CancelledAt);
    }
} 