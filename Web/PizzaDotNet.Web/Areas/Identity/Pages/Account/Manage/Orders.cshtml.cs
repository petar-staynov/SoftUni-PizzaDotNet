namespace PizzaDotNet.Web.Areas.Identity.Pages.Account.Manage
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using PizzaDotNet.Common;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Data;
    using PizzaDotNet.Web.ViewModels.Addresses;
    using PizzaDotNet.Web.ViewModels.Orders;

    public class OrdersModel : PageModel
    {
        private readonly IOrdersService ordersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;

        public OrdersModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper,
            IOrdersService ordersService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.ordersService = ordersService;
        }

        public string Username { get; set; }

        public string UserId { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty]
        public string SortingCriteria { get; set; }

        public class InputModel
        {
            [Display(Name="Order by: ")]
            public List<SelectListItem> SortingCriterias { get; set; }
        }

        public IEnumerable<OrderListItemViewModel> Orders { get; set; }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userId = this.userManager.GetUserId(this.User);
            var userOrders =
                await this.ordersService.GetByUserId<OrderListItemViewModel>(userId, this.SortingCriteria);

            this.Orders = userOrders;

            this.Input = new InputModel()
            {
                SortingCriterias = new List<SelectListItem>()
                {
                    new SelectListItem("none", null),
                    new SelectListItem("Price descending", SortingCriterias.ORDER_PRICE_HIGHEST_TO_LOWEST),
                    new SelectListItem("Price ascending", SortingCriterias.ORDER_PRICE_LOWEST_TO_HIGHEST),
                    new SelectListItem("Date descending", SortingCriterias.ORDER_DATE_NEWEST_TO_OLDEST),
                    new SelectListItem("Date ascending", SortingCriterias.ORDER_DATE_OLDEST_TO_NEWEST),
                },
            };
        }

        public async Task<IActionResult> OnGetAsync(string sortCriteria = null)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            this.UserId = user.Id;
            this.Username = user.UserName;

            this.SortingCriteria = sortCriteria;

            await this.LoadAsync(user);
            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var sortCriteria = this.SortingCriteria;

            return await this.OnGetAsync(sortCriteria);
        }
    }
}
