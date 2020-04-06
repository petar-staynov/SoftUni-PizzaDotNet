namespace PizzaDotNet.Web.ViewModels.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using PizzaDotNet.Web.ViewModels.Categories;

    public class ProductCreateInputModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropdownViewModel> Categories { get; set; }

        public IEnumerable<SelectListItem> CategoriesListItems =>
            this.Categories.Select(x => new SelectListItem(x.Name, x.Id.ToString()));
    }
}
