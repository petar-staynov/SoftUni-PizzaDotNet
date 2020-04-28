namespace PizzaDotNet.Web.ViewModels.Administration.Products
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class AdminProductsProductViewModel : IMapFrom<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<AdminProductSizeViewModel> Sizes { get; set; }

        public int SizesCount => this.Sizes.Count;

        public decimal PriceAverage =>
            Math.Round(this.Sizes.Select(s => s.Price).Average(), 2);

        public virtual ICollection<AdminProductRatingViewModel> Ratings { get; set; }

        public double RatingAverage => this.Ratings.Count > 0
            ? Math.Round((this).Ratings.Select(r => r.Value).Average(), 2)
            : 0;
    }
}
