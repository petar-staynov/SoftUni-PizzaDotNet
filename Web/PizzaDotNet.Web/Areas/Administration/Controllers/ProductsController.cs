using Microsoft.AspNetCore.Mvc;

namespace PizzaDotNet.Web.Areas.Administration.Controllers
{
    public class ProductsController : AdministrationController
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}