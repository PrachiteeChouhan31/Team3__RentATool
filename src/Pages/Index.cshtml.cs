using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;


namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// This class is a modal for index page
    /// </summary>
    public class IndexModel : PageModel
    {
        
        private readonly ILogger<IndexModel> _logger;

        /// <summary>
        /// this method creats a service  for index page, logger
        /// </summary>


        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            
        }

        /// <summary>
        /// this method gets the service for index page
        /// </summary>
        public void OnGet()
        {
         }
    }
}