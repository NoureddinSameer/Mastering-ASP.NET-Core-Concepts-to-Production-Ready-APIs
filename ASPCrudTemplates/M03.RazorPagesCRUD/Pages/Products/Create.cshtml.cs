using M03.RazorPagesCRUD.Models;
using M03.RazorPagesCRUD.Store;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M03.RazorPagesCRUD.Pages;

public class CreateModel(ProductStore store) : PageModel
{
    [BindProperty]
    public Product Product { get; set; } = new();
    public IActionResult OnPost()
    {
        Product.Id = Guid.NewGuid();
        store.Add(Product);
        return RedirectToPage("Index");
    }

}