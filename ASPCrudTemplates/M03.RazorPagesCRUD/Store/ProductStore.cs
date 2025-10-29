using M03.RazorPagesCRUD.Models;

namespace M03.RazorPagesCRUD.Store;

public class ProductStore
{
    private readonly List<Product> _products = [
        new () {Id = Guid.NewGuid(), Name = "Soda", Price = 2.99m},
        new () {Id = Guid.NewGuid(), Name = "Ice-Cream", Price = 3.99m}
    ];
    
    public IEnumerable<Product> GetAll() => _products;

    public Product? GetById(Guid id) => _products.FirstOrDefault(p=>p.Id == id);

    public void Add(Product product)
    {
        _products.Add(product);
    }
    
    public bool Update(Product updatedProduct)
    {
        var existing = _products.FirstOrDefault(p => p.Id == updatedProduct.Id);

        if(existing is null)
            return false;
        
        existing.Name = updatedProduct.Name;
        existing.Price = updatedProduct.Price;

        return true;
    }

     public bool Delete(Product product)
    {
        var existing = _products.FirstOrDefault(p => p.Id == product.Id);
 
        return existing != null && _products.Remove(product);
    }
}