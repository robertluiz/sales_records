using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Handler to process the update product command
/// </summary>
public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the handler with required dependencies
    /// </summary>
    public UpdateProductHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Processes the update product command
    /// </summary>
    public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateProductCommandValidator();
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
            throw new InvalidOperationException("Product not found");

        product.Code = request.Code;
        product.Title = request.Title;
        product.Name = request.Name;
        product.Description = request.Description;
        product.Category = request.Category;
        product.Image = request.Image;
        product.Price = request.Price;
        product.IsActive = request.IsActive;
        product.UpdatedAt = DateTime.UtcNow;

        var validationResult = product.Validate();
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors.Select(e => new ValidationFailure("Product", e.ToString())));

        await _productRepository.UpdateAsync(product, cancellationToken);
        await _productRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UpdateProductResult>(product);
    }
} 