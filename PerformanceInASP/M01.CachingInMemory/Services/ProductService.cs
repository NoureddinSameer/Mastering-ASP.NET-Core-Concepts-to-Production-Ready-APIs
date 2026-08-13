using M01.CachingInMemory.Data;
using M01.CachingInMemory.Models;
using M01.CachingInMemory.Requests;
using M01.CachingInMemory.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;


namespace M01.CachingInMemory.Services;

public class ProductService(AppDbContext context, IMemoryCache cache) : IProductService
{

    public async Task<List<ProductResponse>> GetProductsAsync()
    {
        return await cache.GetOrCreate("products", async entry=>
        {
            // there is no cache
            entry.Size = 1;
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30); // TTL = Time To Live

            var entities = await context.Products.ToListAsync();

            Console.WriteLine("DB visited");

            var productResponse = entities?.Select(p => ProductResponse.FromModel(p)).ToList() ?? [];

            return  productResponse!;
        })!;
    }


    public async Task<List<ProductResponse>> GetProductsAsync_Old()
    {
        var cacheKey = "products";
        if(cache.TryGetValue(cacheKey, out List<ProductResponse>? products))
        {
            Console.WriteLine("Cache visited");
            return products!;
        }

        var entities = await context.Products.ToListAsync();

        Console.WriteLine("DB visited");

        products = entities?.Select(p => ProductResponse.FromModel(p)).ToList() ?? [];

        cache.Set(cacheKey, products, new MemoryCacheEntryOptions
        {
            Size = 1,
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) // TTL = Time To Live
        });

        return  products;
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

        return ProductResponse.FromModel(product);
    }

    public async Task UpdateProductAsync(int productId, UpdateProductRequest request)
    {
        var existingProduct = await context.Products.FirstOrDefaultAsync(p => p.Id == productId)
                                ?? throw new KeyNotFoundException("product not found");

        existingProduct.Name = request.Name;

        existingProduct.Price = request.Price;

        await context.SaveChangesAsync();

    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id)
                      ?? throw new KeyNotFoundException("product not found");

        context.Products.Remove(product);

        await context.SaveChangesAsync();
    }
}
