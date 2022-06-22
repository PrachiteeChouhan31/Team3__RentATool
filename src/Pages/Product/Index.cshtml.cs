using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// Index Page will return all the tool data to show
    /// </summary>
    public class IndexModel : PageModel
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="productService"></param>
        public IndexModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }
        

        // Data Service
        public JsonFileProductService ProductService { get; }
        
        // Collection of the Data
        public IEnumerable<ProductModel> Products { get; private set; }
        [TempData]
        public string FormResult { get; set; }
        /// <summary>
        /// REST OnGet, return all tool data
        /// </summary>
        public void OnGet(string sort="")
        {
            if (sort.ToLower().Contains("byname"))
            {
                Products = ProductService.GetAllData().OrderBy(x => x.ToolName);
            }
            else
            {
                Products = ProductService.GetAllData();
            }
        }
    }
}