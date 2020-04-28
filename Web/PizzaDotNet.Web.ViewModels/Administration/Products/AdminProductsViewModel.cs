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
            this.SortingCriteriaList = new List<OrderSortingCriteriaListItemViewModel>
            {
                new OrderSortingCriteriaListItemViewModel("none", null),
                new OrderSortingCriteriaListItemViewModel("Price descending", SortingCriterias.PRODUCT_PRICE_HIGHEST_TO_LOWEST),
                new OrderSortingCriteriaListItemViewModel("Price ascending", SortingCriterias.PRODUCT_PRICE_LOWEST_TO_HIGHEST),
                new OrderSortingCriteriaListItemViewModel("Rating descending", SortingCriterias.PRODUCT_RATING_HIGHEST_TO_LOWEST),
                new OrderSortingCriteriaListItemViewModel("Rating ascending", SortingCriterias.PRODUCT_RATING_LOWEST_TO_HIGHEST),
                new OrderSortingCriteriaListItemViewModel("Name descending", SortingCriterias.PRODUCT_NAME_DESCENDING),
                new OrderSortingCriteriaListItemViewModel("Name ascending", SortingCriterias.PRODUCT_NAME_ASCENDING),
                new OrderSortingCriteriaListItemViewModel("Category name des.", SortingCriterias.PRODUCT_CATEGORY_NAME_DESCENDING),
                new OrderSortingCriteriaListItemViewModel("Category name asc.", SortingCriterias.PRODUCT_CATEGORY_NAME_ASCENDING),
            };
        }

        [Display(Name="Order by: ")]
        public string SortingCriteria { get; set; }

        public List<OrderSortingCriteriaListItemViewModel> SortingCriteriaList { get; set; }

        public IEnumerable<AdminProductsProductViewModel> Products { get; set; }

        public int ProductsCount { get; set; }
    }
}
