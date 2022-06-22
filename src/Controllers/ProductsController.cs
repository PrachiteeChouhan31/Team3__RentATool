using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Controllers
{
    /// <summary>
    /// this class makes a controllers in this MVC
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        /// <summary>
        /// this method is constructor to initailze the servive for product
        /// </summary>
        
        public ProductsController(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        /// <summary>
        /// this method gets the product service
        /// </summary>
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// this method returns product for HTTP GET
        /// </summary>
        
        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            return ProductService.GetAllData();
        }
        /// <summary>
        /// this method calls to add the rating as a HTTP PATCH i.e. for update
        /// </summary>
        
        [HttpPatch]
        public ActionResult Patch([FromBody] RatingRequest request)
        {
            ProductService.AddRating(request.ProductId, request.Rating);
            
            return Ok();
        }


        /// <summary>
        /// this class get and set productId and Rating for controller
        /// </summary>
        public class RatingRequest
        {
            /// <summary>
            /// this methods get and set productId
            /// </summary>
            public string ProductId { get; set; }

            /// <summary>
            /// this method get and set the Rating
            /// </summary>
            public int Rating { get; set; }
        }
    }
}