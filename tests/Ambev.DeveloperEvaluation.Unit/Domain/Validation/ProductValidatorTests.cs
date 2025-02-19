using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the ProductValidator class.
/// Tests cover validation of all product properties including code,
/// title, name, description, category, image, price, and ratings.
/// </summary>
public class ProductValidatorTests
{
    private readonly ProductValidator _validator;

    public ProductValidatorTests()
    {
        _validator = new ProductValidator();
    }

    /// <summary>
    /// Tests that validation passes when all product properties are valid.
    /// </summary>
    [Fact(DisplayName = "Valid product should pass validation")]
    public void Given_ValidProduct_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails when product code is empty.
    /// </summary>
    [Theory(DisplayName = "Invalid code should fail validation")]
    [InlineData("")]
    [InlineData(" ")]
    public void Given_InvalidCode_When_Validated_Then_ShouldHaveError(string code)
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Code = code;

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Code)
            .WithErrorMessage("'Code' must not be empty.");
    }

    /// <summary>
    /// Tests that validation fails when product title is empty.
    /// </summary>
    [Theory(DisplayName = "Invalid title should fail validation")]
    [InlineData("")]
    [InlineData(" ")]
    public void Given_InvalidTitle_When_Validated_Then_ShouldHaveError(string title)
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Title = title;

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Title)
            .WithErrorMessage("'Title' must not be empty.");
    }

    /// <summary>
    /// Tests that validation fails when product name is empty.
    /// </summary>
    [Theory(DisplayName = "Invalid name should fail validation")]
    [InlineData("")]
    [InlineData(" ")]
    public void Given_InvalidName_When_Validated_Then_ShouldHaveError(string name)
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Name = name;

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("'Name' must not be empty.");
    }

    /// <summary>
    /// Tests that validation fails when product description is too long.
    /// </summary>
    [Fact(DisplayName = "Long description should fail validation")]
    public void Given_LongDescription_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Description = ProductTestData.GenerateLongDescription();

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description)
            .WithErrorMessage("The length of 'Description' must be 500 characters or fewer. You entered 501 characters.");
    }

    /// <summary>
    /// Tests that validation fails when product category is empty.
    /// </summary>
    [Theory(DisplayName = "Invalid category should fail validation")]
    [InlineData("")]
    [InlineData(" ")]
    public void Given_InvalidCategory_When_Validated_Then_ShouldHaveError(string category)
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Category = category;

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Category)
            .WithErrorMessage("'Category' must not be empty.");
    }

    /// <summary>
    /// Tests that validation fails when image URL is too long.
    /// </summary>
    [Fact(DisplayName = "Long image URL should fail validation")]
    public void Given_LongImageUrl_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Image = ProductTestData.GenerateLongImageUrl();

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Image)
            .WithErrorMessage("The length of 'Image' must be 500 characters or fewer. You entered 501 characters.");
    }

    /// <summary>
    /// Tests that validation fails when product price is invalid.
    /// </summary>
    [Theory(DisplayName = "Invalid price should fail validation")]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Given_InvalidPrice_When_Validated_Then_ShouldHaveError(decimal price)
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Price = price;

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Price)
            .WithErrorMessage("'Price' must be greater than '0'.");
    }

    /// <summary>
    /// Tests that validation fails when product rating is invalid.
    /// </summary>
    [Theory(DisplayName = "Invalid rating should fail validation")]
    [InlineData(-1)]
    [InlineData(5.1)]
    [InlineData(6)]
    public void Given_InvalidRating_When_Validated_Then_ShouldHaveError(decimal rating)
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Rating.Rate = rating;

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Rating.Rate)
            .WithErrorMessage($"'Rating Rate' must be between 0 and 5. You entered {rating}.");
    }

    /// <summary>
    /// Tests that validation fails when rating count is negative.
    /// </summary>
    [Theory(DisplayName = "Negative rating count should fail validation")]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Given_NegativeRatingCount_When_Validated_Then_ShouldHaveError(int count)
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Rating.Count = count;

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Rating.Count)
            .WithErrorMessage("'Rating Count' must be greater than or equal to '0'.");
    }

    /// <summary>
    /// Tests that validation fails when update date is before creation date.
    /// </summary>
    [Fact(DisplayName = "Update date before creation date should fail validation")]
    public void Given_UpdatedAtBeforeCreatedAt_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.CreatedAt = DateTime.UtcNow;
        product.UpdatedAt = product.CreatedAt.AddDays(-1);

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UpdatedAt)
            .WithErrorMessage("Update date must be after creation date.");
    }
} 