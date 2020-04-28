namespace PizzaDotNet.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using PizzaDotNet.Web.ViewModels;

    public class ErrorController : BaseController
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorViewModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier,
            };

            return this.View(errorViewModel);
        }

        public IActionResult LoginFailed()
        {
            return this.View();
        }

        [Route("[controller]/Code/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    this.ViewData["ErrorCode"] = "404";
                    this.ViewData["ErrorMessage"] = "Sorry but the page you are looking for can not be found.";
                    return this.View("StatusCodeError");
                default:
                    this.ViewData["ErrorCode"] = "Unknown";
                    this.ViewData["ErrorMessage"] = "An unknown error ahs occured";
                    return this.View("StatusCodeError");
            }
        }
    }
}
