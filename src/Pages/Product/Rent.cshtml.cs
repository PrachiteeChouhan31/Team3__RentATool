using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Globalization;

namespace ContosoCrafts.WebSite.Pages.Product
{
    public class RentModel : PageModel
    {
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public RentModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // The data to show
        [BindProperty]
        public ProductModel Product { get; set; }
        //another way to do binding
        public string firstname;
        public string lastname;
        public string email;
        public string phonenumber;
        public string rentaldate;


        [BindProperty]
        public Rental RentalInfo { get; set; }

        [TempData]
        public string FormResult { get; set; }
        /// <summary>
        /// REST Get request
        /// </summary>
        /// <param name="id"></param>
        public IActionResult OnGet(string id)
        {
            if (ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(id)) == null)
            {
                FormResult = "Can't process your renting, please provide valid information and try again.";
                return RedirectToPage("/ToolPage");
                
            }
            Product = ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(id));
            return null;
        }
        /// <summary>
        /// REST POST request and returns to Index page
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //var firstname = Request.Form["firstname"].ToString(); another way to bind

           
            var resultCheck=ProductService.AddUserInfoRent(Product.Id, RentalInfo);
            TextInfo textInfo = new CultureInfo("en-US",false).TextInfo;
            if (resultCheck!= null) 
            {
                RentalInfo.FirstName = textInfo.ToTitleCase(RentalInfo.FirstName);
                FormResult= "Successfully initiated the renting process for " + resultCheck.ToolName + "," + RentalInfo.FirstName+ ".";
            }
            else
            {
                FormResult = "Can't process your renting, please provide valid information and try again.";

            }
            

            return RedirectToPage("/ToolPage");
        }
    }
}