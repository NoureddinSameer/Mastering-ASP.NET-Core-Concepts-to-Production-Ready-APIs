using M02.MVCCRUD.Models;
using M02.MVCCRUD.Store;
using Microsoft.AspNetCore.Mvc;
namespace M02.MVCCRUD.Controllers;

public class ProductsController(ProductStore store) : Controller
{
    // ../products/index
    // ../products
    [HttpGet]
    public IActionResult Index()
    {
        var products = store.GetAll();
        return View(products);
    }

    // ../products/index/{id}

    [HttpGet]
    public IActionResult Details(Guid id)
    {
        var product = store.GetById(id);
        if (product is null)
            return NotFound();
        return View(product);
    }
    
    // ../products/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    // ../products/Create
    [HttpPost]
    public IActionResult Create(Product product)
    {
        product.Id = Guid.NewGuid();
        store.Add(product);
        return RedirectToAction(nameof(Index));
    }

    // ../products/Edit
    [HttpGet]
    public IActionResult Edit(Guid id)
    {
        var product = store.GetById(id);
        if (product is null)
            return NotFound();
        return View(product);
    }
    [HttpPost]
    public IActionResult Edit(Product product)
    {
        var success = store.Update(product);
        if (!success)
            return NotFound();
        return RedirectToAction(nameof(Index));
    }


    [HttpGet]
    public IActionResult Delete(Guid id)
    {
        var product = store.GetById(id);
        if (product == null)
            return NotFound();
        return View(product);
    }

    
    [HttpPost,ActionName("Delete")]
    public IActionResult DeleteConfirmed(Guid id)
    {
        var product = store.GetById(id);

        if (product == null)
            return NotFound();

        store.Delete(product);
        return RedirectToAction(nameof(Index));
    }

}