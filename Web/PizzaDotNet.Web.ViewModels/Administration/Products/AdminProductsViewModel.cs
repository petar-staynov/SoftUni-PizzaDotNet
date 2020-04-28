namespace PizzaDotNet.Web.ViewModels.Administration.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PizzaDotNet.Common;
    using PizzaDotNet.Web.ViewModels.Orders;

    public class AdminProductsViewModel
    {
        public AdminProductsViewModel()
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
                new SortingCriteriaListItemViewModel("Category name des.", SortingCriterias.PRODUCT_CATEGORY_NAME_DESCENDING),
                new SortingCriteriaListItemViewModel("Category name asc.", SortingCriterias.PRODUCT_CATEGORY_NAME_ASCENDING),
            };
        }

        [Display(Name="Order by: ")]
        public string SortingCriteria { get; set; }

        public List<SortingCriteriaListItemViewModel> SortingCriteriaList { get; set; }

        public IEnumerable<AdminProductsProductViewModel> Products { get; set; }

        public int ProductsCount { get; set; }
    }
}
