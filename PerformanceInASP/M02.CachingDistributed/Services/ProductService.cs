using System.Text.Json;
using M02.CachingDistributed.Data;
using M02.CachingDistributed.Models;
using M02.CachingDistributed.Requests;
using M02.CachingDistributed.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace M02.CachingDistributed.Services;

public class ProductService(AppDbContext context, IDistributedCache cache) : IProductService
{
    public async Task<List<ProductResponse>> GetProductsAsync()
    {
        var cacheKey = "products";

        var cachedData = await cache.GetStringAsync(cacheKey);

        if (cachedData is not null)
        {
            Console.WriteLine("Cache visited");
            return JsonSerializer.Deserialize<List<ProductResponse>>(cachedData)!;
        }

        var entities = await context.Products.ToListAsync();

        var products = entities.Select(ProductResponse.FromModel).ToList();

        var jsonData = JsonSerializer.Serialize(products);

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) // TTL
        };

        await cache.SetStringAsync(cacheKey, jsonData, options);

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

        await cache.RemoveAsync("products");

        return ProductResponse.FromModel(product);
    }

    public async Task UpdateProductAsync(int productId, UpdateProductRequest request)
    {
        var existingProduct = await context.Products.FirstOrDefaultAsync(p => p.Id == productId)
                                ?? throw new KeyNotFoundException("product not found");

        existingProduct.Name = request.Name;

        existingProduct.Price = request.Price;

        await context.SaveChangesAsync();

        await cache.RemoveAsync("products");
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
