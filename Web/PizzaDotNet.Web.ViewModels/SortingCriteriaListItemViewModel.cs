namespace PizzaDotNet.Web.ViewModels
{
    public class SortingCriteriaListItemViewModel
    {
        public SortingCriteriaListItemViewModel(string description, string name)
        {
            this.Name = name;
            this.Description = description;
        }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
