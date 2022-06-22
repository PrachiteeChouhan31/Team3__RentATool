using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// This class creates a model for create page
    /// </summary>
    public class CreateModel : PageModel
    {
        /// <summary>
        /// this method gets the json product service
        /// </summary>
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public CreateModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        //// The data to show
        //public ProductModel Product;
        // The data to show, bind to it for the post
        [BindProperty]
        public ProductModel Product { get; set; }

        [TempData]
        public string FormResult { get; set; }

        /// <summary>
        /// REST Get request and redirecting to update page
        /// </summary>
        /// <param name="id"></param>
        public IActionResult OnGet()
        {
            Product = new ProductModel();
            return null;
        }

        /// <summary>
        /// Post the model back to the page
        /// The model is in the class variable Product
        /// Call the data layer to Update that data
        /// Then return to the index page
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            Product.Id = System.Guid.NewGuid().ToString();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result=ProductService.CreateData(Product);
           
            FormResult = "Successfully added the tool, " + result.ToolName + ".";
            return RedirectToPage("./Index");
        }
    }
}