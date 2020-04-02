﻿namespace PizzaDotNet.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<CategoryProductViewModel> Products { get; set; }
    }
}
