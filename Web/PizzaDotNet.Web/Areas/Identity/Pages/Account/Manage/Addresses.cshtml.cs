namespace PizzaDotNet.Web.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Data;
    using PizzaDotNet.Web.ViewModels.Addresses;
    using PizzaDotNet.Web.ViewModels.DTO;

    public partial class AddressesModel : PageModel
    {
        private readonly IAddressesService addressesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;

        public AddressesModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper,
            IAddressesService addressesService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.addressesService = addressesService;
        }

        public string Username { get; set; }

        public string UserId { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public AddressViewInputModel Input { get; set; }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userAddress =
                await this.addressesService.GetByUserId<AddressViewInputModel>(this.UserId) ?? new AddressViewInputModel();

            this.Input = userAddress;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            this.UserId = user.Id;
            this.Username = user.UserName;

            await this.LoadAsync(user);
            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var claimsPrincipal = this.User;
            if (claimsPrincipal != null)
            {
                var user = await this.userManager.GetUserAsync(claimsPrincipal);
                if (user == null)
                {
                    return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(claimsPrincipal)}'.");
                }

                if (!this.ModelState.IsValid)
                {
                    await this.LoadAsync(user);
                    return this.Page();
                }
            }

            var userId = this.userManager.GetUserId(this.User);

            var userAddress = await this.addressesService.GetBaseByUserId(userId);
            if (userAddress == null)
            {
                userAddress = this.mapper.Map<UserAddress>(this.Input);
                userAddress.UserId = userId;
                await this.addressesService.CreateAsync(userAddress);

                this.StatusMessage = "Your address has been updated";
                return this.RedirectToPage();
            }

            userAddress = this.mapper.Map(this.Input, userAddress);

            await this.addressesService.UpdateAsync(userAddress);
            this.StatusMessage = "Your address has been updated";
            return this.RedirectToPage();
        }
    }
}
