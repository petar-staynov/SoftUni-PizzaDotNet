namespace PizzaDotNet.Web.ViewModels.Administration.Categories
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PizzaDotNet.Common;

    public class AdminCategoriesViewModel
    {
        public AdminCategoriesViewModel()
        {
            this.SortingCriteriaList = new List<SortingCriteriaListItemViewModel>
            {
                new SortingCriteriaListItemViewModel("none", null),
                new SortingCriteriaListItemViewModel("Product count descending", SortingCriterias.CATEGORIES_PRODUCT_COUNT_HIGHEST_TO_LOWEST),
                new SortingCriteriaListItemViewModel("Product count  ascending", SortingCriterias.CATEGORIES_PRODUCT_COUNT_LOWEST_TO_HIGHEST),
                new SortingCriteriaListItemViewModel("Name descending", SortingCriterias.CATEGORIES_NAME_DESCENDING),
                new SortingCriteriaListItemViewModel("Name ascending", SortingCriterias.CATEGORIES_NAME_ASCENDING),
            };
        }

        [Display(Name="Order by: ")]
        public string SortingCriteria { get; set; }

        public List<SortingCriteriaListItemViewModel> SortingCriteriaList { get; set; }

        public IEnumerable<AdminCategoriesCategoryViewModel> Categories { get; set; }
    }
}
