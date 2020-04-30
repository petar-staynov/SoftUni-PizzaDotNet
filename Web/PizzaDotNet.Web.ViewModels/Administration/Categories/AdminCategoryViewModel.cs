namespace PizzaDotNet.Web.ViewModels.Administration.Categories
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PizzaDotNet.Common;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class AdminCategoryViewModel : IMapFrom<Category>
    {
        public AdminCategoryViewModel()
        {
            this.SortingCriteriaList = new List<SortingCriteriaListItemViewModel>
            {
                new SortingCriteriaListItemViewModel("none", null),
                new SortingCriteriaListItemViewModel("Price descending", SortingCriterias.PRODUCT_PRICE_HIGHEST_TO_LOWEST),
                new SortingCriteriaListItemViewModel("Price ascending", SortingCriterias.PRODUCT_PRICE_LOWEST_TO_HIGHEST),
                new SortingCriteriaListItemViewModel("Rating descending", SortingCriterias.PRODUCT_RATING_HIGHEST_TO_LOWEST),
                new SortingCriteriaListItemViewModel("Rating ascending", SortingCriterias.PRODUCT_RATING_LOWEST_TO_HIGHEST),
                new SortingCriteriaListItemViewModel("Name descending", SortingCriterias.PRODUCT_NAME_DESCENDING),
                new SortingCriteriaListItemViewModel("Name ascending", SortingCriterias.PRODUCT_NAME_ASCENDING),
            };

            this.Products = new List<AdminCategoryProductViewModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual IEnumerable<AdminCategoryProductViewModel> Products { get; set; }

        public string ImageUrl { get; set; }

        public string ImageStorageName { get; set; }

        [Display(Name="Order by: ")]
        public string SortingCriteria { get; set; }

        public List<SortingCriteriaListItemViewModel> SortingCriteriaList { get; set; }
    }
}
