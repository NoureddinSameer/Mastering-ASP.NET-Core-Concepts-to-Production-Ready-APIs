using M03.RazorPagesCRUD.Models;
using M03.RazorPagesCRUD.Store;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace  M03.RazorPagesCRUD.Pages;

public class DeleteModel(ProductStore store) : PageModel
{
    [BindProperty]
    public Product? Product { get; set; }

    public IActionResult OnGet(Guid id)
    {
        Product = store.GetById(id);
        if (Product == null)
            return NotFound();

        return Page();
    }

    public IActionResult OnPost()
    {
      if (Product == null)
        return NotFound();

        var existing = store.GetById(Product.Id);
        if (existing == null)
            return NotFound();

        var deleted = store.Delete(existing);

        if (!deleted)
            return NotFound();

        return RedirectToPage("Index");
    }
}