

using System.Data;
using Dapper;
using M02.Dapper.Models;

public class ProductRepository(IDbConnection _db)
{
    public int GetProductsCount() =>
        _db.ExecuteScalar<int>("SELECT COUNT(*) FROM Products");


    public List<Product> GetProductsPage(int page = 1, int pageSize = 10)
    {
        return _db.Query<Product>(
           "SELECT * FROM Products LIMIT @Limit OFFSET @Offset",
           new { Limit = pageSize, Offset = (page - 1) * pageSize }
        ).ToList();
    }

    public Product? GetProductById(Guid productId)
    {
        return _db.QuerySingleOrDefault<Product>(
            "SELECT * FROM Products WHERE Id = @Id", new { Id = productId.ToString() });
    }

    public List<ProductReview> GetProductReviews(Guid productId)
    {
        return _db.Query<ProductReview>(
            "SELECT * FROM ProductReviews WHERE ProductId = @ProductId",
            new { ProductId = productId.ToString() }
            ).ToList();
    }

    public ProductReview? GetReview(Guid productId, Guid reviewId)
    {
        return _db.QuerySingleOrDefault<ProductReview>(
            "SELECT * FROM ProductReviews WHERE ProductId = @ProductId AND Id = @Id",
            new { ProductId = productId, Id = reviewId.ToString() });
    }

    public bool AddProduct(Product product)
    {
        var rows = _db.Execute(
            "INSERT INTO Products (Id, Name, Price) VALUES (@Id, @Name, @Price)",
            new { product.Id, product.Name, product.Price });
        return rows > 0;
    }

    public bool AddProductReview(ProductReview review)
    {
        var exists = _db.ExecuteScalar<int>(
            "SELECT COUNT(*) FROM Products WHERE Id = @Id", new { Id = review.ProductId.ToString() });
        if (exists == 0) return false;

        var rows = _db.Execute("""
                INSERT INTO ProductReviews (Id, ProductId, Reviewer, Stars)
                VALUES (@Id, @ProductId, @Reviewer, @Stars)
                """,
            new
            {
                Id = review.Id.ToString(),
                ProductId = review.ProductId.ToString(),
                review.Reviewer,
                review.Stars
            });

        return rows > 0;
    }

    public bool UpdateProduct(Product updatedProduct)
    {
        var rows = _db.Execute(
            "UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id",
            new
            {
                Id = updatedProduct.Id.ToString(),
                updatedProduct.Name,
                updatedProduct.Price
            });

        return rows > 0;
    }

    public bool DeleteProduct(Guid id)
    {
        var rows = _db.Execute("DELETE FROM Products WHERE Id = @Id", new { Id = id.ToString() });
        _db.ExecuteAsync("DELETE FROM ProductReviews WHERE ProductId = @Id", new { Id = id.ToString() });
        return rows > 0;
    }
    public bool ExistsById(Guid id)
    {
        return _db.ExecuteScalar<bool>(
            "SELECT EXISTS(SELECT 1 FROM Products WHERE Id = @Id)", new { Id = id.ToString() });
    }

    public bool ExistsByName(string? name)
    {
        return _db.ExecuteScalar<bool>(
            "SELECT EXISTS(SELECT 1 FROM Products WHERE Name = @Name COLLATE NOCASE)", new { Name = name });

    }
}