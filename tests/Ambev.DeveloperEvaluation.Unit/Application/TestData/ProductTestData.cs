using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Models;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class ProductTestData
{
    public static CreateProductCommand GenerateValidCreateCommand()
    {
        return new CreateProductCommand
        {
            Code = "PROD-001",
            Title = "Test Product",
            Name = "Test Product Name",
            Description = "Test Product Description",
            Category = "Test Category",
            Image = "http://test.com/image.jpg",
            Price = 99.99m,
            Rating = new ProductRating
            {
                Rate = 4.5m,
                Count = 10
            }
        };
    }

    public static UpdateProductCommand GenerateValidUpdateCommand(int id)
    {
        return new UpdateProductCommand
        {
            Id = id,
            Code = "PROD-001-UPDATED",
            Title = "Updated Test Product",
            Name = "Updated Test Product Name",
            Description = "Updated Test Product Description",
            Category = "Updated Test Category",
            Image = "http://test.com/updated-image.jpg",
            Price = 149.99m,
            Rating = 4.8m,
            RatingCount = 15,
            IsActive = true
        };
    }

    public static Product GenerateValidProduct()
    {
        return new Product
        {
            Id = 1,
            Code = "PROD-001",
            Title = "Test Product",
            Name = "Test Product Name",
            Description = "Test Product Description",
            Category = "Test Category",
            Image = "http://test.com/image.jpg",
            Price = 99.99m,
            Rating = new ProductRating
            {
                Rate = 4.5m,
                Count = 10
            },
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };
    }
} 