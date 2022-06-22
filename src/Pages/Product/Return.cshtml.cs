using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Globalization;

namespace ContosoCrafts.WebSite.Pages.Product
{
    public class ReturnModel : PageModel
    {
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public ReturnModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // The data to show
        [BindProperty]
        public ProductModel Product { get; set; }

        /// <summary>
        /// to store information from return form
        /// </summary
        [TempData]
        public string FormResult{get; set;}
        public class ReturnInfo 
        {
           
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Email { get; set; }
        }
        public ReturnInfo Info { get; set; }
        
        /// <summary>
        /// REST Get request
        /// </summary>
        /// <param name="id"></param>
        public IActionResult OnGet(string id)
        {
            if (ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(id)) == null)
            {
                FormResult = "Can't process your return, please provide valid information and try again.";
                return RedirectToPage("/ToolPage");

            }

            Product = ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(id));
            return null;
        }
        /// <summary>
        /// REST POST request and returns to Index page
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost(ReturnInfo Info)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //string firstName = Request.Form["firstname"].ToString(); //to bind
            var check= ProductService.RemoveUserInfoReturn(Product.Id, Info.Firstname, Info.Lastname, Info.Email);
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            if (check != null)
            {
                Info.Firstname = textInfo.ToTitleCase(Info.Firstname);
                FormResult = "Successfully initiated the returned of "+ check.ToolName+","+Info.Firstname+".";
      
            }
            else 
            {
                FormResult = "Can't process your return, please provide valid information and try again.";
            }

            
            return RedirectToPage("/ToolPage");
        }
    }
}