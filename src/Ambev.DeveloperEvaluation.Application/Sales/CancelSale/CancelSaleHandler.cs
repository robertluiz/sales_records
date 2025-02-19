using MediatR;
using AutoMapper;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Handler to process the cancel sale command
/// </summary>
public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly CancelSaleCommandValidator _validator;

    /// <summary>
    /// Initializes a new instance of the handler with required dependencies
    /// </summary>
    public CancelSaleHandler(
        ISaleRepository saleRepository, 
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _validator = new CancelSaleCommandValidator(saleRepository);
    }

    /// <summary>
    /// Processes the cancel sale command
    /// </summary>
    public async Task<CancelSaleResult> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (sale == null)
            throw new InvalidOperationException("Sale not found");

        if (sale.IsCancelled)
            throw new InvalidOperationException("Sale is already cancelled");

        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        sale.Cancel();
        await _saleRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CancelSaleResult>(sale);
    }
} 