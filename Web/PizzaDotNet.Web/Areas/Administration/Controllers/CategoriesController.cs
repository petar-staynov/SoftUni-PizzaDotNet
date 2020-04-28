namespace PizzaDotNet.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : AdministrationController
    {
        // GET
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
