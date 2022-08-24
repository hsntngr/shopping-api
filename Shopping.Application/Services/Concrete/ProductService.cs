using AutoMapper;
using Shopping.Application.Http.Exceptions;
using Shopping.Application.Resources.Product;
using Shopping.Application.Services.Abstract;
using Shopping.Domain.Entities;
using Shopping.EntityFrameworkCore.Repositories.Abstract;
using Shopping.EntityFrameworkCore.UnitOfWork.Abstract;

namespace Shopping.Application.Services.Concrete;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IProductRepository productRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ICollection<ProductResponse>> GetListAsync()
    {
        ICollection<Product> products = await _productRepository.GetAllAsync();
        return _mapper.Map<ICollection<Product>, ICollection<ProductResponse>>(products);
    }

    public async Task<ProductResponse> GetByIdAsync(Guid productId)
    {
        Product product = await _productRepository.GetByIdAsync(productId);
        return _mapper.Map<Product, ProductResponse>(product);
    }

    public async Task<ProductResponse> CreateAsync(CreateProductRequest request)
    {
        Product product = _mapper.Map<CreateProductRequest, Product>(request);
        _productRepository.Add(product);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<Product, ProductResponse>(product);
    }

    public async Task<ProductResponse> UpdateAsync(UpdateProductRequest request, Guid productId)
    {
        Product product = await _productRepository.GetByIdAsync(productId);
        if (product == null) throw new NotFoundException();
        _mapper.Map<UpdateProductRequest, Product>(request, product);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<Product, ProductResponse>(product);
    }

    public async Task RemoveAsync(Guid productId)
    {
        Product product = await _productRepository.GetByIdAsync(productId);
        if (product == null) throw new NotFoundException();
        _productRepository.Remove(product);
        await _unitOfWork.SaveChangesAsync();
    }
}