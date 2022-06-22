using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Pages
{

    /// <summary>
    /// This class is a model for tool page
    /// </summary>
    public class ToolPageModel : PageModel
    {
        private readonly ILogger<ToolPageModel> _logger;

        /// <summary>
        /// This method created a service, logger service and add the json file product service
        /// </summary>
        
        public ToolPageModel(ILogger<ToolPageModel> logger,
            JsonFileProductService productService)
        {
           
            _logger = logger;
            //create a variable for ProductService
            ProductService = productService;
        }

        /// <summary>
        /// this method save the json file product service
        /// </summary>
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// this method retrive the products for index page.(save product here)
        /// </summary>
        public IEnumerable<ProductModel> Products { get; private set; }
        [TempData]
        public string FormResult { get; set; }
        /// <summary>
        /// this method get the product (functionality of razor page)
        /// </summary>
        public void OnGet()
        {
            //REST HTTP GET
            Products = ProductService.GetAllData();
        }
    }
}
