namespace PizzaDotNet.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : AdministrationController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
