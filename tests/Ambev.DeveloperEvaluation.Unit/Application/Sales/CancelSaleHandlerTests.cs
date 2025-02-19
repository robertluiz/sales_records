using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class CancelSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IEventService _eventService;
    private readonly CancelSaleCommandValidator _validator;
    private readonly CancelSaleHandler _handler;

    public CancelSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _eventService = Substitute.For<IEventService>();
        _validator = new CancelSaleCommandValidator(_saleRepository);

        _handler = new CancelSaleHandler(
            _saleRepository,
            _mapper,
            _eventService,
            _validator);
    }

    [Fact(DisplayName = "Given valid cancellation data When canceling sale Then should return success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            TotalAmount = 100m,
            Items = new List<SaleItem>
            {
                new() { Id = Guid.NewGuid(), Total = 50m },
                new() { Id = Guid.NewGuid(), Total = 50m }
            }
        };

        var command = new CancelSaleCommand { Id = sale.Id };
        var expectedResult = new CancelSaleResult();

        _saleRepository.GetByIdAsync(sale.Id, Arg.Any<CancellationToken>())
            .Returns(sale);
        _mapper.Map<CancelSaleResult>(Arg.Any<Sale>())
            .Returns(expectedResult);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedResult);

        await _saleRepository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        await _eventService.Received(1).PublishSaleCancelledEvent(Arg.Is<SaleCancelledEvent>(e =>
            e.Id == sale.Id &&
            e.BranchId == sale.BranchId &&
            e.RefundAmount == sale.TotalAmount));
    }

    [Fact(DisplayName = "Dado venda inexistente Quando cancelar venda Então deve lançar InvalidOperationException")]
    public async Task Handle_SaleNotFound_ThrowsInvalidOperationException()
    {
        // Arrange
        var command = new CancelSaleCommand { Id = Guid.NewGuid() };
        _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns((Sale?)null);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(command, CancellationToken.None));

        await _saleRepository.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
        await _eventService.DidNotReceive().PublishSaleCancelledEvent(Arg.Any<SaleCancelledEvent>());
    }

    [Fact(DisplayName = "Dado venda já cancelada Quando cancelar venda Então deve lançar InvalidOperationException")]
    public async Task Handle_AlreadyCancelled_ThrowsInvalidOperationException()
    {
        // Arrange
        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            IsCancelled = true
        };

        var command = new CancelSaleCommand { Id = sale.Id };
        _saleRepository.GetByIdAsync(sale.Id, Arg.Any<CancellationToken>())
            .Returns(sale);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(command, CancellationToken.None));

        await _saleRepository.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
        await _eventService.DidNotReceive().PublishSaleCancelledEvent(Arg.Any<SaleCancelledEvent>());
    }
} 