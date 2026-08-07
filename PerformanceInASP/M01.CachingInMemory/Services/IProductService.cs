using M01.CachingInMemory.Models;
using M01.CachingInMemory.Requests;
using M01.CachingInMemory.Responses;

namespace M01.CachingInMemory.Services;

public interface IProductService
{
    Task<List<ProductResponse>> GetProductsAsync();

    Task<ProductResponse?> GetProductByIdAsync(int productId);

    Task<ProductResponse> AddProductAsync(CreateProductRequest request);

    Task UpdateProductAsync(int productId, UpdateProductRequest request);

    Task DeleteProductAsync(int id);
}
