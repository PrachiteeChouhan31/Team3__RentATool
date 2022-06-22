using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ContosoCrafts.WebSite.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

    /// <summary>
    /// This class creates a model for Error page
    /// </summary>

    public class ErrorModel : PageModel
    {
        /// <summary>
        /// this method get and set the Request ID
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// this methods return boolean request id is null or empty
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        
        private readonly ILogger<ErrorModel> _logger;

        /// <summary>
        /// this methods create aservice,logger for error page
        /// </summary>
        
        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// this methods gets an error for razor page.
        /// </summary>
        public void OnGet()
        {
            //REST HTTP GET
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}