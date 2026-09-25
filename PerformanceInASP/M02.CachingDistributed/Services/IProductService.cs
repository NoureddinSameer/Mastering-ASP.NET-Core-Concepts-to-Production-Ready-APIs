using M02.CachingDistributed.Requests;
using M02.CachingDistributed.Responses;

namespace M02.CachingDistributed.Services;

public interface IProductService
{
    Task<List<ProductResponse>> GetProductsAsync();

    Task<ProductResponse?> GetProductByIdAsync(int productId);

    Task<ProductResponse> AddProductAsync(CreateProductRequest request);

    Task UpdateProductAsync(int productId, UpdateProductRequest request);

    Task DeleteProductAsync(int id);
}
