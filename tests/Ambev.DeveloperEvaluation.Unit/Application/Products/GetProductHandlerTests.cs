using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

public class GetProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly GetProductHandler _handler;

    public GetProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetProductHandler(_productRepository, _mapper);
    }

    [Fact(DisplayName = "Should return product when product exists")]
    public async Task Handle_ExistingProduct_ShouldReturnProduct()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        var query = new GetProductQuery { Id = product.Id };
        var expectedResult = new GetProductResult
        {
            Id = product.Id,
            Code = product.Code,
            Title = product.Title,
            Name = product.Name,
            Description = product.Description,
            Category = product.Category,
            Image = product.Image,
            Price = product.Price,
            Rating = product.Rating,
            IsActive = product.IsActive,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };

        _productRepository.GetByIdAsync(product.Id, Arg.Any<CancellationToken>())
            .Returns(product);
        _mapper.Map<GetProductResult>(Arg.Any<Product>()).Returns(expectedResult);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact(DisplayName = "Should throw exception when product not found")]
    public async Task Handle_ProductNotFound_ShouldThrowException()
    {
        // Arrange
        var query = new GetProductQuery { Id = 1 };
        _productRepository.GetByIdAsync(query.Id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Product?>(null));

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(query, CancellationToken.None));
    }
} 