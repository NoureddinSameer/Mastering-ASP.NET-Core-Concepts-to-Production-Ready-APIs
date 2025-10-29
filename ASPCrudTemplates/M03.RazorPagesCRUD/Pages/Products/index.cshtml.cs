using M03.RazorPagesCRUD.Models;
using M03.RazorPagesCRUD.Store;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace M03.RazorPagesCRUD.Pages;

public class IndexModel(ProductStore store) : PageModel
{
    public IEnumerable<Product> Products { get; set; } = [];
    
    public void OnGet()
    {
        Products = store.GetAll();
    }
}

