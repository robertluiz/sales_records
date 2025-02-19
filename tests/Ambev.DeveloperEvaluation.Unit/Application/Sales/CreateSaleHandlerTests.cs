using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
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
    private readonly IEventService _eventService;
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
        _validator = new CreateSaleCommandValidator();
        _eventService = Substitute.For<IEventService>();

        _handler = new CreateSaleHandler(
            _saleRepository,
            _productRepository,
            _mapper,
            _validator,
            _eventService
        );
    }

    /// <summary>
    /// Tests that a valid sale creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid sale data When creating sale Then should return success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var product = ProductTestData.GenerateValidProduct();
        var expectedResult = new CreateSaleResult();

        _productRepository.GetByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(product);
        _mapper.Map<CreateSaleResult>(Arg.Any<Sale>()).Returns(expectedResult);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedResult);

        await _saleRepository.Received(1).AddAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        await _saleRepository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        await _eventService.Received(1).PublishSaleCreatedEvent(Arg.Any<SaleCreatedEvent>());
    }

    /// <summary>
    /// Tests that an invalid sale creation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale data When creating sale Then should throw validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Arrange
        var command = new CreateSaleCommand(); // Comando vazio

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));

        await _saleRepository.DidNotReceive().AddAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        await _saleRepository.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
        await _eventService.DidNotReceive().PublishSaleCreatedEvent(Arg.Any<SaleCreatedEvent>());
    }

    /// <summary>
    /// Tests that when product is not found, an InvalidOperationException is thrown.
    /// </summary>
    [Fact(DisplayName = "Given non-existent product When creating sale Then should throw InvalidOperationException")]
    public async Task Handle_ProductNotFound_ThrowsInvalidOperationException()
    {
        // Arrange
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        _productRepository.GetByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns((Product?)null);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(command, CancellationToken.None));

        await _saleRepository.DidNotReceive().AddAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        await _saleRepository.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
        await _eventService.DidNotReceive().PublishSaleCreatedEvent(Arg.Any<SaleCreatedEvent>());
    }

    /// <summary>
    /// Tests that product price is used instead of any provided price.
    /// </summary>
    [Fact(DisplayName = "Given valid sale When creating Then should use product price")]
    public async Task Handle_ValidRequest_UsesProductPrice()
    {
        // Arrange
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var product = ProductTestData.GenerateValidProduct();
        _productRepository.GetByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(product);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _saleRepository.Received(1).AddAsync(
            Arg.Is<Sale>(s => s.Items.All(i => i.UnitPrice == product.Price)),
            Arg.Any<CancellationToken>()
        );
        await _eventService.Received(1).PublishSaleCreatedEvent(Arg.Any<SaleCreatedEvent>());
    }
} 