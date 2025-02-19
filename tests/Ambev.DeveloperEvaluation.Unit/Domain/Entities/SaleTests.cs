using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleTests
{
    [Fact(DisplayName = "Deve calcular totais corretamente")]
    public void CalculateTotals_ShouldCalculateCorrectly()
    {
        // Arrange
        var sale = new Sale
        {
            Items = new List<SaleItem>
            {
                new()
                {
                    Quantity = 5,
                    UnitPrice = 100,
                    CreatedAt = DateTime.UtcNow.AddHours(-1)
                },
                new()
                {
                    Quantity = 3,
                    UnitPrice = 50,
                    CreatedAt = DateTime.UtcNow.AddHours(-1)
                }
            }
        };

        // Act
        sale.CalculateTotals();

        // Assert
        sale.Subtotal.Should().Be(650);
        sale.DiscountAmount.Should().Be(50);
        sale.TotalAmount.Should().Be(600);
    }

    [Fact(DisplayName = "Deve ignorar itens cancelados no cálculo dos totais")]
    public void CalculateTotals_ShouldIgnoreCancelledItems()
    {
        // Arrange
        var sale = new Sale
        {
            Items = new List<SaleItem>
            {
                new()
                {
                    Quantity = 2,
                    UnitPrice = 100,
                    DiscountPercentage = 10,
                    Subtotal = 200,
                    DiscountAmount = 20,
                    Total = 180,
                    IsCancelled = true
                },
                new()
                {
                    Quantity = 3,
                    UnitPrice = 50,
                    DiscountPercentage = 0,
                    Subtotal = 150,
                    DiscountAmount = 0,
                    Total = 150
                }
            }
        };

        // Act
        sale.CalculateTotals();

        // Assert
        sale.Subtotal.Should().Be(150);
        sale.DiscountAmount.Should().Be(0);
        sale.TotalAmount.Should().Be(150);
    }

    [Fact(DisplayName = "Deve cancelar a venda corretamente")]
    public void Cancel_ShouldCancelSaleAndItems()
    {
        // Arrange
        var sale = new Sale
        {
            CreatedAt = DateTime.UtcNow.AddHours(-1),
            Items = new List<SaleItem>
            {
                new() { CreatedAt = DateTime.UtcNow.AddHours(-1) },
                new() { CreatedAt = DateTime.UtcNow.AddHours(-1) }
            }
        };

        // Act
        sale.Cancel();

        // Assert
        sale.IsCancelled.Should().BeTrue();
        sale.CancelledAt.Should().NotBeNull();
        sale.CancelledAt.Should().BeAfter(sale.CreatedAt);

        foreach (var item in sale.Items)
        {
            item.IsCancelled.Should().BeTrue();
            item.CancelledAt.Should().NotBeNull();
            item.CancelledAt.Should().BeAfter(item.CreatedAt);
        }
    }

    [Fact(DisplayName = "Deve cancelar um item específico corretamente")]
    public void CancelItem_ShouldCancelSpecificItem()
    {
        // Arrange
        var itemId = Guid.NewGuid();
        var sale = new Sale
        {
            CreatedAt = DateTime.UtcNow.AddHours(-1),
            Items = new List<SaleItem>
            {
                new() 
                { 
                    Id = itemId,
                    CreatedAt = DateTime.UtcNow.AddHours(-1)
                },
                new() 
                { 
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow.AddHours(-1)
                }
            }
        };

        // Act
        sale.CancelItem(itemId);

        // Assert
        var cancelledItem = sale.Items.First(i => i.Id == itemId);
        cancelledItem.IsCancelled.Should().BeTrue();
        cancelledItem.CancelledAt.Should().NotBeNull();
        cancelledItem.CancelledAt.Should().BeAfter(cancelledItem.CreatedAt);

        var otherItem = sale.Items.First(i => i.Id != itemId);
        otherItem.IsCancelled.Should().BeFalse();
        otherItem.CancelledAt.Should().BeNull();
    }

    [Fact(DisplayName = "Deve lançar exceção ao tentar cancelar item inexistente")]
    public void CancelItem_ShouldThrowException_WhenItemNotFound()
    {
        // Arrange
        var sale = new Sale
        {
            Items = new List<SaleItem>
            {
                new() { Id = Guid.NewGuid() }
            }
        };

        // Act & Assert
        var action = () => sale.CancelItem(Guid.NewGuid());
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("Item not found");
    }

    [Fact(DisplayName = "Deve validar a venda corretamente")]
    public void Validate_ShouldReturnValidationResult()
    {
        // Arrange
        var sale = new Sale
        {
            Number = "SALE-001",
            BranchId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            SaleDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            Items = new List<SaleItem>
            {
                new()
                {
                    ProductId = 1,
                    Quantity = 1,
                    UnitPrice = 100,
                    CreatedAt = DateTime.UtcNow
                }
            }
        };

        // Act
        var result = sale.Validate();

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }
} 