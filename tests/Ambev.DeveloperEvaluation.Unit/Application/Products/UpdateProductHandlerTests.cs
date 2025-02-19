using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Models;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

public class UpdateProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly UpdateProductHandler _handler;

    public UpdateProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new UpdateProductHandler(_productRepository, _mapper);
    }

    [Fact(DisplayName = "Should update product successfully when command is valid")]
    public async Task Handle_ValidCommand_ShouldUpdateProduct()
    {
        // Arrange
        var existingProduct = ProductTestData.GenerateValidProduct();
        var command = ProductTestData.GenerateValidUpdateCommand(existingProduct.Id);
        var expectedResult = new UpdateProductResult
        {
            Id = existingProduct.Id,
            Code = command.Code,
            Title = command.Title,
            Name = command.Name,
            Description = command.Description,
            Category = command.Category,
            Image = command.Image,
            Price = command.Price,
            Rating = new ProductRating { Rate = command.Rating, Count = command.RatingCount },
            RatingCount = command.RatingCount,
            IsActive = command.IsActive
        };

        _productRepository.GetByIdAsync(existingProduct.Id, Arg.Any<CancellationToken>())
            .Returns(existingProduct);
        _mapper.Map<UpdateProductResult>(Arg.Any<Product>()).Returns(expectedResult);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedResult);

        await _productRepository.Received(1).UpdateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
        await _productRepository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Should throw validation exception when command is invalid")]
    public async Task Handle_InvalidCommand_ShouldThrowValidationException()
    {
        // Arrange
        var command = new UpdateProductCommand(); // Empty command

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));

        await _productRepository.DidNotReceive().UpdateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
        await _productRepository.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Should throw exception when product not found")]
    public async Task Handle_ProductNotFound_ShouldThrowException()
    {
        // Arrange
        var command = ProductTestData.GenerateValidUpdateCommand(1);
        _productRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Product?>(null));

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(command, CancellationToken.None));

        await _productRepository.DidNotReceive().UpdateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
        await _productRepository.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Should throw validation exception when product validation fails")]
    public async Task Handle_InvalidProduct_ShouldThrowValidationException()
    {
        // Arrange
        var existingProduct = ProductTestData.GenerateValidProduct();
        var command = ProductTestData.GenerateValidUpdateCommand(existingProduct.Id);
        command.Price = -1; // Invalid price

        _productRepository.GetByIdAsync(existingProduct.Id, Arg.Any<CancellationToken>())
            .Returns(existingProduct);

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));

        await _productRepository.DidNotReceive().UpdateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
        await _productRepository.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }
} 