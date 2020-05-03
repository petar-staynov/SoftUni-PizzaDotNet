namespace PizzaDotNet.Web.Areas.Identity.Pages.Account.Manage
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Data;
    using PizzaDotNet.Web.ViewModels.CouponCodes;

    public class CouponCodesModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICouponCodeService couponCodeService;

        public CouponCodesModel(
            UserManager<ApplicationUser> userManager,
            ICouponCodeService couponCodeService)
        {
            this.userManager = userManager;
            this.couponCodeService = couponCodeService;
        }

        public string Username { get; set; }

        public string UserId { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public IEnumerable<CouponCodeViewModel> CouponCodes { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var couponCodes = await this.couponCodeService.GetValidByUserId<CouponCodeViewModel>(user.Id);

            this.CouponCodes = couponCodes;

            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return null;
        }
    }
}
