using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Ambev.DeveloperEvaluation.Domain.Models;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Handler to process the create product command
/// </summary>
public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the handler with required dependencies
    /// </summary>
    public CreateProductHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Processes the create product command
    /// </summary>
    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateProductCommandValidator();
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var product = new Product
        {
            Code = request.Code,
            Title = request.Title,
            Name = request.Name,
            Description = request.Description,
            Category = request.Category,
            Image = request.Image,
            Price = request.Price,
            Rating = request.Rating,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        var validationResult = product.Validate();
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors.Select(e => new ValidationFailure("Product", e.ToString())));

        await _productRepository.AddAsync(product, cancellationToken);
        await _productRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CreateProductResult>(product);
    }
} 