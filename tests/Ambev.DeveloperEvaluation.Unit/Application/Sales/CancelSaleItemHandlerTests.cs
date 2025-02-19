using Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the <see cref="CancelSaleItemHandler"/> class.
/// </summary>
public class CancelSaleItemHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IEventService _eventService;
    private readonly CancelSaleItemCommandValidator _validator;
    private readonly CancelSaleItemHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CancelSaleItemHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public CancelSaleItemHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _eventService = Substitute.For<IEventService>();
        _validator = new CancelSaleItemCommandValidator(_saleRepository);

        _handler = new CancelSaleItemHandler(
            _saleRepository,
            _mapper,
            _eventService,
            _validator);
    }

    /// <summary>
    /// Tests that a valid sale item cancellation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid cancellation data When canceling item Then should return success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            Id = Guid.NewGuid(),
            ProductId = 1,
            Quantity = 2,
            UnitPrice = 50m,
            Total = 100m
        };

        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            Items = new List<SaleItem> { saleItem }
        };

        var command = new CancelSaleItemCommand
        {
            SaleId = sale.Id,
            ItemId = saleItem.Id
        };

        var expectedResult = new CancelSaleItemResult();

        _saleRepository.GetByIdWithDetailsAsync(sale.Id, Arg.Any<CancellationToken>())
            .Returns(sale);
        _mapper.Map<CancelSaleItemResult>(Arg.Any<SaleItem>())
            .Returns(expectedResult);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedResult);

        await _saleRepository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        await _eventService.Received(1).PublishSaleItemCancelledEvent(Arg.Is<SaleItemCancelledEvent>(e =>
            e.Id == saleItem.Id &&
            e.SaleId == sale.Id &&
            e.ProductId == saleItem.ProductId &&
            e.Quantity == saleItem.Quantity &&
            e.RefundAmount == saleItem.Total));
    }

    /// <summary>
    /// Tests that an invalid sale item cancellation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid cancellation data When canceling sale item Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Arrange
        var command = new CancelSaleItemCommand();

        _saleRepository.GetByIdWithDetailsAsync(command.SaleId, Arg.Any<CancellationToken>())
            .Returns((Sale?)null);

        // When/Then
        await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    /// <summary>
    /// Tests that when sale is not found, an InvalidOperationException is thrown.
    /// </summary>
    [Fact(DisplayName = "Dado venda inexistente Quando cancelar item Então deve lançar InvalidOperationException")]
    public async Task Handle_SaleNotFound_ThrowsInvalidOperationException()
    {
        // Arrange
        var command = new CancelSaleItemCommand
        {
            SaleId = Guid.NewGuid(),
            ItemId = Guid.NewGuid()
        };

        _saleRepository.GetByIdWithDetailsAsync(command.SaleId, Arg.Any<CancellationToken>())
            .Returns((Sale?)null);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(command, CancellationToken.None));

        await _saleRepository.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
        await _eventService.DidNotReceive().PublishSaleItemCancelledEvent(Arg.Any<SaleItemCancelledEvent>());
    }

    /// <summary>
    /// Tests that when item is not found in sale, an InvalidOperationException is thrown.
    /// </summary>
    [Fact(DisplayName = "Dado item inexistente Quando cancelar item Então deve lançar InvalidOperationException")]
    public async Task Handle_ItemNotFound_ThrowsInvalidOperationException()
    {
        // Arrange
        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            Items = new List<SaleItem>()
        };

        var command = new CancelSaleItemCommand
        {
            SaleId = sale.Id,
            ItemId = Guid.NewGuid()
        };

        _saleRepository.GetByIdWithDetailsAsync(sale.Id, Arg.Any<CancellationToken>())
            .Returns(sale);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(command, CancellationToken.None));

        await _saleRepository.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
        await _eventService.DidNotReceive().PublishSaleItemCancelledEvent(Arg.Any<SaleItemCancelledEvent>());
    }

    /// <summary>
    /// Tests that when item is already cancelled, an InvalidOperationException is thrown.
    /// </summary>
    [Fact(DisplayName = "Dado item já cancelado Quando cancelar item Então deve lançar InvalidOperationException")]
    public async Task Handle_ItemAlreadyCancelled_ThrowsInvalidOperationException()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            Id = Guid.NewGuid(),
            IsCancelled = true
        };

        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            Items = new List<SaleItem> { saleItem }
        };

        var command = new CancelSaleItemCommand
        {
            SaleId = sale.Id,
            ItemId = saleItem.Id
        };

        _saleRepository.GetByIdWithDetailsAsync(sale.Id, Arg.Any<CancellationToken>())
            .Returns(sale);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(command, CancellationToken.None));

        await _saleRepository.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
        await _eventService.DidNotReceive().PublishSaleItemCancelledEvent(Arg.Any<SaleItemCancelledEvent>());
    }
} 