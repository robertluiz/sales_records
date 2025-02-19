using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

/// <summary>
/// Query para listar vendas com filtros
/// </summary>
public class ListSalesQuery : IRequest<ListSalesResult>
{
    /// <summary>
    /// Obtém ou define o ID da filial para filtrar
    /// </summary>
    public Guid? BranchId { get; set; }

    /// <summary>
    /// Obtém ou define o ID do cliente para filtrar
    /// </summary>
    public Guid? CustomerId { get; set; }

    /// <summary>
    /// Obtém ou define a data inicial para filtrar
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Obtém ou define a data final para filtrar
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Obtém ou define se deve incluir apenas vendas canceladas
    /// </summary>
    public bool? IsCancelled { get; set; }

    /// <summary>
    /// Obtém ou define o número da página (1-based)
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Obtém ou define o tamanho da página
    /// </summary>
    public int PageSize { get; set; } = 10;
} 