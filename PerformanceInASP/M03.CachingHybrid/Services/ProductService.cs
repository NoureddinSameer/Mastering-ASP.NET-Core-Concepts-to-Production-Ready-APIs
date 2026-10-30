using M03.CachingHybrid.Data;
using M03.CachingHybrid.Models;
using M03.CachingHybrid.Requests;
using M03.CachingHybrid.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;

namespace M03.CachingHybrid.Services;

public class ProductService(AppDbContext context, HybridCache cache) : IProductService
{
    public async Task<List<ProductResponse>> GetProductsAsync()
    {
        var products = await cache.GetOrCreateAsync("products",
        async ct =>
        {
            var entities = await context.Products.ToListAsync(ct);

            var productResponse = entities?.Select(p => ProductResponse.FromModel(p)).ToList() ?? [];

            Console.WriteLine("DB Visited");

            return productResponse;
        },
        options: new HybridCacheEntryOptions
        {

        },
        tags: ["products-tag"]
        );

        return products;
    }

    public async Task<ProductResponse?> GetProductByIdAsync(int productId)
    {
        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        return product is null ? null : ProductResponse.FromModel(product);
    }

    public async Task<ProductResponse> AddProductAsync(CreateProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price
        };

        context.Products.Add(product);

        await context.SaveChangesAsync();

        await cache.RemoveAsync("products"); // invalidate

        return ProductResponse.FromModel(product);
    }

    public async Task UpdateProductAsync(int productId, UpdateProductRequest request)
    {
        var existingProduct = await context.Products.FirstOrDefaultAsync(p => p.Id == productId)
                                ?? throw new KeyNotFoundException("product not found");

        existingProduct.Name = request.Name;

        existingProduct.Price = request.Price;

        await context.SaveChangesAsync();

        await cache.RemoveAsync("products"); // invalidate

        await cache.RemoveByTagAsync("products-tag");

    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id)
                      ?? throw new KeyNotFoundException("product not found");

        context.Products.Remove(product);

        await context.SaveChangesAsync();

        await cache.RemoveAsync("products"); // invalidate
    }
}
