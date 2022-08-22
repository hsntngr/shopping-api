using Shopping.Application.Resources.Product;

namespace Shopping.Application.Services.Abstract;

public interface IProductService
{
    Task<ICollection<ProductResponse>> GetListAsync();
    Task<ProductResponse> GetByIdAsync(Guid productId);
    Task<ProductResponse> CreateAsync(CreateProductRequest request);
    Task<ProductResponse> UpdateAsync(UpdateProductRequest request, Guid productId);
    Task RemoveAsync(Guid productId);
}