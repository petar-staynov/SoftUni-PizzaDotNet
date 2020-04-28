namespace PizzaDotNet.Web.ViewModels.Administration.Dashboard
{
    public class IndexViewModel
    {
        public int UsersCount { get; set; }

        public int CategoriesCount { get; set; }

        public int ProductsCount { get; set; }

        public int OrdersCount { get; set; }

        public decimal? OrdersTotalProfits { get; set; }
    }
}
