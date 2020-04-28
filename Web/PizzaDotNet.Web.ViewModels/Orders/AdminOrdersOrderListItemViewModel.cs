namespace PizzaDotNet.Web.ViewModels.Orders
{
    using System;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class AdminOrdersOrderListItemViewModel : IMapFrom<Order>
    {
        private DateTime createdOn;

        public int Id { get; set; }

        public DateTime CreatedOn
        {
            get => this.createdOn;
            set => this.createdOn = value;
        }

        public virtual OrderStatus OrderStatus { get; set; }

        public decimal? TotalPriceDiscounted { get; set; }

        public ApplicationUser User { get; set; }
    }
}
