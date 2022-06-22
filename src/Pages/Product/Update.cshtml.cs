using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// Manage the Update of the data for a single record
    /// </summary>
    public class UpdateModel : PageModel
    {
        // Data middletier
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public UpdateModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // The data to show, bind to it for the post
        [BindProperty]
        public ProductModel Product { get; set; }

        [TempData]
        public string FormResult { get; set; }

        /// <summary>
        /// REST Get request
        /// Loads the Data
        /// </summary>
        /// <param name="id"></param>
        public IActionResult OnGet(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToPage("/Index");
            }
            if (ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(id)) == null)
            {
                return RedirectToPage("/Index");

            }
            Product  = ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(id));
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result= ProductService.UpdateData(Product);
            if (result == null)
            {
                FormResult = "Can't update the tool, please provide valid information and try again.";
                return RedirectToPage("./Index");
            }
            FormResult = "Successfully updated the tool, " + result.ToolName + ".";
            return RedirectToPage("./Index");
        }
    }
}