using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Sales;

public class Branch
{
    public Branch()
    {
        CreatedAt = DateTime.UtcNow;
        Sales = new List<Sale>();
    }

    [Key]
    public Guid Id { get; set; }
    
    public string Code { get; set; } = null!;
    
    public string Name { get; set; } = null!;
    
    public string? Address { get; set; }
    
    public bool IsActive { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    public virtual ICollection<Sale> Sales { get; set; }
} 