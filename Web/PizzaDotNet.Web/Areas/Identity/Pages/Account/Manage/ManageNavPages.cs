﻿namespace PizzaDotNet.Web.Areas.Identity.Pages.Account.Manage
{
    using System;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public static class ManageNavPages
    {
        public static string Index => "Index";

        public static string Email => "Email";

        public static string Addresses => "Addresses";

        public static string Orders => "Orders";

        public static string CouponCodes => "CouponCodes";

        public static string ChangePassword => "ChangePassword";

        public static string ExternalLogins => "ExternalLogins";

        public static string PersonalData => "PersonalData";

        public static string TwoFactorAuthentication => "TwoFactorAuthentication";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string AddressesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Addresses);

        public static string OrdersNavClass(ViewContext viewContext) => PageNavClass(viewContext, Orders);

        public static string CouponCodesNavClass(ViewContext viewContext) => PageNavClass(viewContext, CouponCodes);

        public static string EmailNavClass(ViewContext viewContext) => PageNavClass(viewContext, Email);

        public static string ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

        public static string ExternalLoginsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ExternalLogins);

        public static string PersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, PersonalData);

        public static string TwoFactorAuthenticationNavClass(ViewContext viewContext) => PageNavClass(viewContext, TwoFactorAuthentication);

        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
