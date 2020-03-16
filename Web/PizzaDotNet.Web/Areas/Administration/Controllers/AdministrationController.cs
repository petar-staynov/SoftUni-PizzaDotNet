namespace PizzaDotNet.Web.Areas.Administration.Controllers
{
    using PizzaDotNet.Common;
    using PizzaDotNet.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
