namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

/// <summary>
/// Resultado da listagem de vendas
/// </summary>
public class ListSalesResult
{
    /// <summary>
    /// Obtém ou define o total de registros
    /// </summary>
    public int TotalRecords { get; set; }

    /// <summary>
    /// Obtém ou define a página atual
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Obtém ou define o tamanho da página
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Obtém ou define o total de páginas
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Obtém ou define a lista de vendas
    /// </summary>
    public List<SaleListItem> Items { get; set; } = new();
}

/// <summary>
/// Item da lista de vendas
/// </summary>
public class SaleListItem
{
    /// <summary>
    /// Obtém ou define o ID da venda
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Obtém ou define o ID da filial
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Obtém ou define o ID do cliente
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Obtém ou define a data da venda
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Obtém ou define o subtotal antes dos descontos
    /// </summary>
    public decimal Subtotal { get; set; }

    /// <summary>
    /// Obtém ou define o valor total dos descontos
    /// </summary>
    public decimal DiscountAmount { get; set; }

    /// <summary>
    /// Obtém ou define o valor total após os descontos
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Obtém ou define se a venda está cancelada
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Obtém ou define quando a venda foi cancelada
    /// </summary>
    public DateTime? CancelledAt { get; set; }

    /// <summary>
    /// Obtém ou define quando a venda foi criada
    /// </summary>
    public DateTime CreatedAt { get; set; }
} 