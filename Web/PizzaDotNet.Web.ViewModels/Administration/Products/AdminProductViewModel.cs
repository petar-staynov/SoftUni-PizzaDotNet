namespace PizzaDotNet.Web.ViewModels.Administration.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;
    using PizzaDotNet.Web.ViewModels.ProductSize;

    public class AdminProductViewModel : IMapFrom<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public AdminProductCategoryViewModel Category { get; set; }

        public string ImageUrl { get; set; }

        public string ImageStorageName { get; set; }

        public virtual ICollection<ProductSizeViewModel> Sizes { get; set; }

        public virtual ICollection<AdminProductRatingViewModel> Ratings { get; set; }

        public double? RatingAverage => this.Ratings.Count > 0
            ? this.Ratings.Average(r => r.Value)
            : 0;
    }
}
