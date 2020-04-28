namespace PizzaDotNet.Web.ViewModels.Administration.Orders
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PizzaDotNet.Common;
    using PizzaDotNet.Web.ViewModels.Orders;

    public class AdminOrdersViewModel
    {
        public AdminOrdersViewModel()
        {
            this.SortingCriteriaList = new List<SortingCriteriaListItemViewModel>
            {
                new SortingCriteriaListItemViewModel("none", null),
                new SortingCriteriaListItemViewModel("Price descending", SortingCriterias.ORDER_PRICE_HIGHEST_TO_LOWEST),
                new SortingCriteriaListItemViewModel("Price ascending", SortingCriterias.ORDER_PRICE_LOWEST_TO_HIGHEST),
                new SortingCriteriaListItemViewModel("Date descending", SortingCriterias.ORDER_DATE_NEWEST_TO_OLDEST),
                new SortingCriteriaListItemViewModel("Date ascending", SortingCriterias.ORDER_DATE_OLDEST_TO_NEWEST),
                new SortingCriteriaListItemViewModel("Username descending", SortingCriterias.ORDER_USENAME_DESCENDING),
                new SortingCriteriaListItemViewModel("Username ascending", SortingCriterias.ORDER_USERNAME_ASCENDING),
            };
        }

        [Display(Name="Order by: ")]
        public string SortingCriteria { get; set; }

        public List<SortingCriteriaListItemViewModel> SortingCriteriaList { get; set; }

        public int OrdersCount { get; set; }

        public IEnumerable<AdminOrdersOrderListItemViewModel> Orders { get; set; }
    }
}
