namespace Ambev.DeveloperEvaluation.Domain.Models;

/// <summary>
/// Representa as informações de avaliação de um produto
/// </summary>
public class ProductRating
{
    /// <summary>
    /// Obtém ou define a nota média do produto (0-5)
    /// </summary>
    public decimal Rate { get; set; }

    /// <summary>
    /// Obtém ou define o número total de avaliações
    /// </summary>
    public int Count { get; set; }
} 