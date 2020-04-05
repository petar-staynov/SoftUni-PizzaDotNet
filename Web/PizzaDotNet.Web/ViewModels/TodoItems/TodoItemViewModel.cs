namespace PizzaDotNet.Web.ViewModels.TodoItems
{
    using PizzaDotNet.Common.Mapping;
    using PizzaDotNet.Data.Models;

    public class TodoItemViewModel : IMapFrom<TodoItem>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsDone { get; set; }
    }
}
