namespace PizzaDotNet.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PizzaDotNet.Common;
    using PizzaDotNet.Services;
    using PizzaDotNet.Services.Data;
    using PizzaDotNet.Web.ViewModels.Cart;
    using PizzaDotNet.Web.ViewModels.DTO;

    public class CouponCodesController : BaseController
    {
        private const string COUPON_EXPIRED = "Invalid coupon code";
        private const string COUPON_REMOVED = "Successfuly removed coupon code";
        private const string COUPON_APPLIED = "Coupon code successfully applied";

        private readonly ISessionService sessionService;
        private readonly ICouponCodeService couponCodeService;

        public CouponCodesController(
            ISessionService sessionService,
            ICouponCodeService couponCodeService)
        {
            this.sessionService = sessionService;
            this.couponCodeService = couponCodeService;
        }

        public async Task<ActionResult> ApplyCode(CartViewModel inputModel)
        {
            string couponCodeString = inputModel.CouponCode;
            if (string.IsNullOrEmpty(couponCodeString))
            {
                var sessionCouponCodeDto = new SessionCouponCodeDto
                {
                    Code = null,
                };

                this.sessionService.Set(this.HttpContext.Session, "CouponCode", sessionCouponCodeDto);

                this.TempData["Message"] = COUPON_REMOVED;
                this.TempData["MessageType"] = AlertMessageTypes.Error;

                return this.RedirectToAction("Index", "Cart");
            }

            var couponCode = this.couponCodeService.CanUseCodeByCode(couponCodeString);
            if (couponCode == null)
            {
                this.TempData["Message"] = COUPON_EXPIRED;
                this.TempData["MessageType"] = AlertMessageTypes.Error;

                return this.RedirectToAction("Index", "Cart");
            }

            var SessionCartProductDto = new SessionCouponCodeDto()
            {
                Code = couponCode.Code,
            };

            this.sessionService.Set(this.HttpContext.Session, "CouponCode", SessionCartProductDto);

            this.TempData["Message"] = COUPON_APPLIED;
            this.TempData["MessageType"] = AlertMessageTypes.Success;

            return this.RedirectToAction("Index", "Cart");
        }
    }
}
