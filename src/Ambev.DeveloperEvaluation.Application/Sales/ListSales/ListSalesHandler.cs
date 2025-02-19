using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

/// <summary>
/// Handler para processar a query de listagem de vendas
/// </summary>
public class ListSalesHandler : IRequestHandler<ListSalesQuery, ListSalesResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Inicializa uma nova instância do handler com as dependências necessárias
    /// </summary>
    public ListSalesHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Processa a query de listagem de vendas
    /// </summary>
    public async Task<ListSalesResult> Handle(ListSalesQuery request, CancellationToken cancellationToken)
    {
        var (sales, totalRecords) = await _saleRepository.ListAsync(
            branchId: request.BranchId,
            customerId: request.CustomerId,
            startDate: request.StartDate,
            endDate: request.EndDate,
            isCancelled: request.IsCancelled,
            page: request.Page,
            pageSize: request.PageSize,
            cancellationToken: cancellationToken);

        var totalPages = (int)Math.Ceiling(totalRecords / (double)request.PageSize);

        return new ListSalesResult
        {
            TotalRecords = totalRecords,
            CurrentPage = request.Page,
            PageSize = request.PageSize,
            TotalPages = totalPages,
            Items = _mapper.Map<List<SaleListItem>>(sales)
        };
    }
} 