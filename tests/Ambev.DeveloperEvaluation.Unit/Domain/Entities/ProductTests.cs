using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Product entity class.
/// Tests cover status changes and validation scenarios.
/// </summary>
public class ProductTests
{
    /// <summary>
    /// Tests that when an inactive product is activated, its status changes to active.
    /// </summary>
    [Fact(DisplayName = "Product status should change to active when activated")]
    public void Given_InactiveProduct_When_Activated_Then_StatusShouldBeActive()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.IsActive = false;

        // Act
        product.Activate();

        // Assert
        product.IsActive.Should().BeTrue();
        product.UpdatedAt.Should().NotBeNull();
    }

    /// <summary>
    /// Tests that when an active product is deactivated, its status changes to inactive.
    /// </summary>
    [Fact(DisplayName = "Product status should change to inactive when deactivated")]
    public void Given_ActiveProduct_When_Deactivated_Then_StatusShouldBeInactive()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.IsActive = true;

        // Act
        product.Deactivate();

        // Assert
        product.IsActive.Should().BeFalse();
        product.UpdatedAt.Should().NotBeNull();
    }

    /// <summary>
    /// Tests that validation passes when all product properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid product data")]
    public void Given_ValidProductData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();

        // Act
        var result = product.Validate();

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    /// <summary>
    /// Tests that validation fails when product properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid product data")]
    public void Given_InvalidProductData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = new Product
        {
            Code = "", // Invalid: empty
            Title = "", // Invalid: empty
            Name = "", // Invalid: empty
            Description = ProductTestData.GenerateLongDescription(), // Invalid: too long
            Category = "", // Invalid: empty
            Image = ProductTestData.GenerateLongImageUrl(), // Invalid: too long
            Price = 0, // Invalid: must be greater than zero
            Rating = new() { Rate = 6, Count = -1 } // Invalid: rate > 5 and count < 0
        };

        // Act
        var result = product.Validate();

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
    }

    /// <summary>
    /// Tests that price update works correctly for valid values.
    /// </summary>
    [Fact(DisplayName = "Price update should work for valid values")]
    public void Given_ValidPrice_When_UpdatePrice_Then_ShouldUpdateSuccessfully()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        var newPrice = 150.99m;

        // Act
        product.UpdatePrice(newPrice);

        // Assert
        product.Price.Should().Be(newPrice);
        product.UpdatedAt.Should().NotBeNull();
    }

    /// <summary>
    /// Tests that price update fails for invalid values.
    /// </summary>
    [Theory(DisplayName = "Price update should fail for invalid values")]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Given_InvalidPrice_When_UpdatePrice_Then_ShouldThrowException(decimal invalidPrice)
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();

        // Act & Assert
        var act = () => product.UpdatePrice(invalidPrice);
        act.Should().Throw<ArgumentException>()
           .WithMessage("Price must be greater than zero*");
    }

    /// <summary>
    /// Tests that validation fails when product code is too long.
    /// </summary>
    [Fact(DisplayName = "Validation should fail when code is too long")]
    public void Given_LongCode_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Code = ProductTestData.GenerateLongCode();

        // Act
        var result = product.Validate();

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.Error == "MaximumLengthValidator");
    }

    /// <summary>
    /// Tests that validation fails when product title is too long.
    /// </summary>
    [Fact(DisplayName = "Validation should fail when title is too long")]
    public void Given_LongTitle_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Title = ProductTestData.GenerateLongTitle();

        // Act
        var result = product.Validate();

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.Error == "MaximumLengthValidator");
    }
} 