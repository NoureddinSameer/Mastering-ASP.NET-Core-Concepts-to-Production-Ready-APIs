using M03.RazorPagesCRUD.Models;
using M03.RazorPagesCRUD.Store;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M03.RazorPagesCRUD.Pages;

public class DetailsModel(ProductStore store): PageModel
{
    public Product? Product { get; private set;}
    public IActionResult OnGet(Guid id)
    {
        Product = store.GetById(id);
        if (Product is null)
            return NotFound();
        return Page();
    }
}