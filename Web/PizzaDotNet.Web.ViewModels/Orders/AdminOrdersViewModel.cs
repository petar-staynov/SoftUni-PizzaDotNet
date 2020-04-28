namespace PizzaDotNet.Web.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PizzaDotNet.Common;

    public class AdminOrdersViewModel
    {
        public AdminOrdersViewModel()
        {
            this.SortingCriteriaList = new List<OrderSortingCriteriaListItemViewModel>
            {
                new OrderSortingCriteriaListItemViewModel("none", null),
                new OrderSortingCriteriaListItemViewModel("Price descending", SortingCriterias.ORDER_PRICE_HIGHEST_TO_LOWEST),
                new OrderSortingCriteriaListItemViewModel("Price ascending", SortingCriterias.ORDER_PRICE_LOWEST_TO_HIGHEST),
                new OrderSortingCriteriaListItemViewModel("Date descending", SortingCriterias.ORDER_DATE_NEWEST_TO_OLDEST),
                new OrderSortingCriteriaListItemViewModel("Date ascending", SortingCriterias.ORDER_DATE_OLDEST_TO_NEWEST),
                new OrderSortingCriteriaListItemViewModel("Username descending", SortingCriterias.ORDER_USENAME_DESCENDING),
                new OrderSortingCriteriaListItemViewModel("Username ascending", SortingCriterias.ORDER_USERNAME_ASCENDING),
            };
        }

        [Display(Name="Order by: ")]
        public string SortingCriteria { get; set; }

        public List<OrderSortingCriteriaListItemViewModel> SortingCriteriaList { get; set; }

        public int OrdersCount { get; set; }

        public IEnumerable<AdminOrdersOrderListItemViewModel> Orders { get; set; }
    }
}
