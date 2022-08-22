using Microsoft.AspNetCore.Mvc;
using Shopping.API.Controllers.Base;
using Shopping.Application.Resources.Product;
using Shopping.Application.Services.Abstract;

namespace Shopping.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ApplicationControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>
    /// Get List Of Products
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ICollection<ProductResponse>> GetList()
    {
        return await _productService.GetListAsync();
    }

    /// <summary>
    /// Get Product By Id
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{productId}")]
    public async Task<ProductResponse> GetById(Guid productId)
    {
        return await _productService.GetByIdAsync(productId);
    }

    /// <summary>
    /// Create New Product
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ProductResponse> Create(CreateProductRequest request)
    {
        return await _productService.CreateAsync(request);
    }

    /// <summary>
    /// Update Product
    /// </summary>
    /// <param name="request"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{productId}")]
    public async Task<ProductResponse> Update(UpdateProductRequest request, Guid productId)
    {
        return await _productService.UpdateAsync(request, productId);
    }

    /// <summary>
    /// Remove Product
    /// </summary>
    /// <param name="productId"></param>
    [HttpDelete]
    [Route("{productId}")]
    public async Task Remove(Guid productId)
    {
        await _productService.RemoveAsync(productId);
    }
}