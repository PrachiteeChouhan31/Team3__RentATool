using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// Read page will show name, picture, description of each tool
    /// with buttons to update or delete.
    /// </summary>
    public class ReadModel : PageModel
    {
        // Data middletier
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public ReadModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // The data to show
        public ProductModel Product;

        /// <summary>
        /// REST Get request
        /// </summary>
        /// <param name="id"></param>
        public IActionResult OnGet(string id)
        {
           
            if (ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(id)) == null)
            {
                return RedirectToPage("/Index");

            }
            Product = ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(id));
            return null;
        }
    }
}