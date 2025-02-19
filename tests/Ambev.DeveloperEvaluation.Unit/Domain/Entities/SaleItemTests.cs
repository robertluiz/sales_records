using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleItemTests
{
    [Theory(DisplayName = "Deve aplicar 10% de desconto para quantidades entre 4 e 9 itens")]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(9)]
    public void CalculateDiscount_ShouldApply10PercentDiscount_WhenQuantityBetween4And9(int quantity)
    {
        // Arrange
        var item = new SaleItem
        {
            Quantity = quantity,
            UnitPrice = 100,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        item.CalculateDiscount();

        // Assert
        item.DiscountPercentage.Should().Be(10);
        item.DiscountAmount.Should().Be(quantity * 100 * 0.1m);
        item.Subtotal.Should().Be(quantity * 100);
        item.Total.Should().Be(quantity * 100 * 0.9m);
    }

    [Theory(DisplayName = "Deve aplicar 20% de desconto para quantidades entre 10 e 20 itens")]
    [InlineData(10)]
    [InlineData(15)]
    [InlineData(20)]
    public void CalculateDiscount_ShouldApply20PercentDiscount_WhenQuantityBetween10And20(int quantity)
    {
        // Arrange
        var item = new SaleItem
        {
            Quantity = quantity,
            UnitPrice = 100,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        item.CalculateDiscount();

        // Assert
        item.DiscountPercentage.Should().Be(20);
        item.DiscountAmount.Should().Be(quantity * 100 * 0.2m);
        item.Subtotal.Should().Be(quantity * 100);
        item.Total.Should().Be(quantity * 100 * 0.8m);
    }

    [Theory(DisplayName = "Não deve aplicar desconto para quantidades menores que 4 itens")]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void CalculateDiscount_ShouldNotApplyDiscount_WhenQuantityLessThan4(int quantity)
    {
        // Arrange
        var item = new SaleItem
        {
            Quantity = quantity,
            UnitPrice = 100,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        item.CalculateDiscount();

        // Assert
        item.DiscountPercentage.Should().Be(0);
        item.DiscountAmount.Should().Be(0);
        item.Subtotal.Should().Be(quantity * 100);
        item.Total.Should().Be(quantity * 100);
    }

    [Theory(DisplayName = "Deve lançar exceção para quantidades maiores que 20 itens")]
    [InlineData(21)]
    [InlineData(30)]
    [InlineData(100)]
    public void CalculateDiscount_ShouldThrowException_WhenQuantityGreaterThan20(int quantity)
    {
        // Arrange
        var item = new SaleItem
        {
            Quantity = quantity,
            UnitPrice = 100,
            CreatedAt = DateTime.UtcNow
        };

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => item.CalculateDiscount());
    }

    [Fact(DisplayName = "Deve lançar exceção ao tentar aplicar desconto em quantidade menor que 4")]
    public void CalculateDiscount_ShouldThrowException_WhenTryingToApplyDiscountForLessThan4Items()
    {
        // Arrange
        var item = new SaleItem
        {
            Quantity = 2,
            UnitPrice = 100,
            DiscountPercentage = 10, // Tentativa inválida de aplicar desconto
            CreatedAt = DateTime.UtcNow
        };

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => item.CalculateDiscount());
    }
} 