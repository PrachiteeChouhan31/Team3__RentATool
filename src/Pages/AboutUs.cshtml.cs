using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// THis class creates a model for AboutUs page
    /// </summary>
    public class AboutUsModel : PageModel
    {
        private readonly ILogger<AboutUsModel> _logger;

        /// <summary>
        /// this method create a service, logger
        /// </summary>
        
        public AboutUsModel (ILogger<AboutUsModel> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// this method gets a service
        /// </summary>
        public void OnGet()
        {
            //REST HTTP GET
        }
    }
}
