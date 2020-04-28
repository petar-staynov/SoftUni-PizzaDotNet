using Microsoft.AspNetCore.Mvc;

namespace PizzaDotNet.Web.Areas.Administration.Controllers
{
    public class RatingsController : AdministrationController
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}