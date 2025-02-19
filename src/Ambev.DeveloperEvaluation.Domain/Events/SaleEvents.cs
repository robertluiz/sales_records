using System;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SaleCreatedEvent
{
    public Guid Id { get; set; }
    public Guid BranchId { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
    public int ItemCount { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class SaleModifiedEvent
{
    public Guid Id { get; set; }
    public Guid BranchId { get; set; }
    public decimal OldTotalAmount { get; set; }
    public decimal NewTotalAmount { get; set; }
    public DateTime ModifiedAt { get; set; }
}

public class SaleCancelledEvent
{
    public Guid Id { get; set; }
    public Guid BranchId { get; set; }
    public decimal RefundAmount { get; set; }
    public DateTime CancelledAt { get; set; }
}

public class SaleItemCancelledEvent
{
    public Guid Id { get; set; }
    public Guid SaleId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal RefundAmount { get; set; }
    public DateTime CancelledAt { get; set; }
} 