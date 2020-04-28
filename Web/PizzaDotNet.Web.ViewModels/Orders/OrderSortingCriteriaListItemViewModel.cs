namespace PizzaDotNet.Web.ViewModels.Orders
{
    public class OrderSortingCriteriaListItemViewModel
    {
        public OrderSortingCriteriaListItemViewModel(string description, string name)
        {
            this.Name = name;
            this.Description = description;
        }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
