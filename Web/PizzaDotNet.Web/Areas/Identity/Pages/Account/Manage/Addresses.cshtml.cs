namespace PizzaDotNet.Web.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Data;
    using PizzaDotNet.Web.ViewModels.Addresses;

    public partial class AddressesModel : PageModel
    {
        private readonly IAddressesService addressesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AddressesModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IAddressesService addressesService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.addressesService = addressesService;
        }

        public string Username { get; set; }

        public string UserId { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string PersonName { get; set; }

            public string Area { get; set; }

            public string Street { get; set; }

            public string Building { get; set; }

            public string Floor { get; set; }

            public string Apartment { get; set; }

            [Phone]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await this.userManager.GetUserNameAsync(user);
            var userId = await this.userManager.GetUserIdAsync(user);
            var userAddresses =
                this.addressesService.GetByUserId<AddressViewModel>(userId) ?? new AddressViewModel();

            this.Username = userName;
            this.UserId = userId;

            var addressInputModel = new InputModel()
            {
                PersonName = userAddresses.PersonName,
                Area = userAddresses.Area,
                Street = userAddresses.Street,
                Building = userAddresses.Building,
                Floor = userAddresses.Floor,
                Apartment = userAddresses.Apartment,
                PhoneNumber = userAddresses.PhoneNumber,
            };

            this.Input = addressInputModel;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            await this.LoadAsync(user);
            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            if (!this.ModelState.IsValid)
            {
                await this.LoadAsync(user);
                return this.Page();
            }

            var userId = await this.userManager.GetUserIdAsync(user);

            var userAddresses = this.addressesService.GetByUserId<UserAddress>(userId);
            if (userAddresses != null)
            {
                userAddresses.Area = this.Input.Area;
                userAddresses.Apartment = this.Input.Apartment;
                userAddresses.Building = this.Input.Building;
                userAddresses.Floor = this.Input.Floor;
                userAddresses.PhoneNumber = this.Input.PhoneNumber;
                userAddresses.Street = this.Input.Street;

                var createAddressResult = await this.addressesService.UpdateAddressAsync(userAddresses);
            }

            userAddresses = new UserAddress()
            {
                Area = this.Input.Area,
                Apartment = this.Input.Apartment,
                Building = this.Input.Building,
                Floor = this.Input.Floor,
                PhoneNumber = this.Input.PhoneNumber,
                Street = this.Input.Street,
            };
            var result = await this.addressesService.CreateAddressAsync(userAddresses);

            this.StatusMessage = "Your address has been updated";
            return this.RedirectToPage();
        }
    }
}
