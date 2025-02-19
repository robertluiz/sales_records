using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the <see cref="CreateSaleHandler"/> class.
/// </summary>
public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly CreateSaleCommandValidator _validator;
    private readonly CreateSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public CreateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _validator = Substitute.For<CreateSaleCommandValidator>();

        _handler = new CreateSaleHandler(
            _saleRepository,
            _productRepository,
            _mapper,
            _validator);
    }

    /// <summary>
    /// Tests that a valid sale creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var product = new Product { Id = command.Items.First().ProductId, Price = 10.0m };
        var result = new CreateSaleResult { Id = Guid.NewGuid() };

        _productRepository.GetByIdAsync(command.Items.First().ProductId, Arg.Any<CancellationToken>())
            .Returns(product);
        _saleRepository.AddAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);
        _mapper.Map<CreateSaleResult>(Arg.Any<Sale>()).Returns(result);

        // When
        var createResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createResult.Should().NotBeNull();
        createResult.Id.Should().Be(result.Id);
        await _saleRepository.Received(1).AddAsync(Arg.Is<Sale>(s => 
            s.Items.Any(i => i.ProductId == product.Id && i.UnitPrice == product.Price)), 
            Arg.Any<CancellationToken>());
        await _saleRepository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid sale creation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale data When creating sale Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Arrange
        var command = new CreateSaleCommand();

        _validator.When(x => x.ValidateAndThrowAsync(command, Arg.Any<CancellationToken>()))
            .Throw(new ValidationException("Validation failed"));

        // When/Then
        await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    /// <summary>
    /// Tests that when product is not found, an InvalidOperationException is thrown.
    /// </summary>
    [Fact(DisplayName = "Given non-existent product When creating sale Then throws InvalidOperationException")]
    public async Task Handle_ProductNotFound_ThrowsInvalidOperationException()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommandWithItems(1);
        var productId = command.Items.First().ProductId;

        _productRepository.GetByIdAsync(productId, Arg.Any<CancellationToken>())
            .Returns((Product?)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Product not found: {productId}");
    }

    /// <summary>
    /// Tests that product price is used instead of any provided price.
    /// </summary>
    [Fact(DisplayName = "Given valid sale When creating Then uses product price")]
    public async Task Handle_ValidRequest_UsesProductPrice()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommandWithItems(1);
        var productId = command.Items.First().ProductId;
        const decimal productPrice = 99.99m;

        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            BranchId = command.BranchId,
            CustomerId = command.CustomerId,
            SaleDate = command.SaleDate,
            Items = command.Items.Select(i => new SaleItem
            {
                Id = Guid.NewGuid(),
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = 0m // Will be set by handler
            }).ToList()
        };

        _mapper.Map<Sale>(command).Returns(sale);
        _productRepository.GetByIdAsync(productId, Arg.Any<CancellationToken>())
            .Returns(new Product { Id = productId, Price = productPrice });

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        await _saleRepository.Received(1).AddAsync(
            Arg.Is<Sale>(s => s.Items.All(i => i.UnitPrice == productPrice)),
            Arg.Any<CancellationToken>());
    }
} 