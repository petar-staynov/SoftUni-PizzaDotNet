namespace PizzaDotNet.Web.ViewModels.DTO
{
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class SessionCouponCodeDto : IMapFrom<CouponCode>
    {
        public string Code { get; set; }
    }
}
