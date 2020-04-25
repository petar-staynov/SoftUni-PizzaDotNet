namespace PizzaDotNet.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : BaseController
    {
        public IActionResult PlaceOrder()
        {
            return this.Ok("ORDER PLACED");
        }
    }
}
