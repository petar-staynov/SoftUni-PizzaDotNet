namespace PizzaDotNet.Web.ViewModels.TodoItems
{
    using System.ComponentModel.DataAnnotations;

    using PizzaDotNet.Common.Mapping;
    using PizzaDotNet.Data.Models;

    public class TodoItemBindingModel : IMapTo<TodoItem>
    {
        [Required]
        public string Title { get; set; }
    }
}
