namespace PizzaDotNet.Web.ViewModels.Administration.Orders
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;
    using PizzaDotNet.Web.ViewModels.Orders;

    public class AdminOrderInputModel : IMapFrom<Order>
    {
        [Required]
        public int Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        [Required]
        public virtual OrderAddress OrderAddress { get; set; }

        [Required]
        [Display(Name = "Status")]
        public int OrderStatusId { get; set; }

        public virtual IEnumerable<AdminOrderStatusViewModel> OrderStatuses { get; set; }


        public virtual IEnumerable<AdminOrderProductViewModel> OrderProducts { get; set; }


        public int? CouponCodeId { get; set; }

        public virtual CouponCode CouponCode { get; set; }

        public decimal? TotalPrice { get; set; }

        public decimal? TotalPriceDiscounted { get; set; }

        public string OrderNotes { get; set; }
    }
}
