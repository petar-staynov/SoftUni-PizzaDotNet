using Microsoft.AspNetCore.Mvc;

namespace PizzaDotNet.Web.Areas.Administration.Controllers
{
    public class UsersController : AdministrationController
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}
