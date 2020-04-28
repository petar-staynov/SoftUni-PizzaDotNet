namespace PizzaDotNet.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using PizzaDotNet.Services;
    using PizzaDotNet.Services.Data;
    using PizzaDotNet.Web.ViewModels.Products;
    using PizzaDotNet.Web.ViewModels.ProductSize;

    public class ProductsController : BaseController
    {
        private readonly IProductsService productsService;
        private readonly IRatingsService ratingsService;
        private readonly IProductSizeService productSizeService;

        private readonly IMapper mapper;

        public ProductsController(
            IProductsService productsService,
            IRatingsService ratingsService,
            IProductSizeService productSizeService,
            IMapper mapper)
        {
            this.productsService = productsService;
            this.ratingsService = ratingsService;
            this.productSizeService = productSizeService;
            this.mapper = mapper;
        }

        [Route("[controller]/{id}")]
        public IActionResult View(int id)
        {
            var productViewModel = this.productsService.GetById<ProductViewInputModel>(id);
            if (productViewModel == null)
            {
                return this.NotFound();
            }

            productViewModel.Rating = this.ratingsService.GetProductRating(id);
            productViewModel.Sizes = this.productSizeService.GetAllProductSizes<ProductSizeViewModel>(id);

            return this.View(productViewModel);
        }
    }
}
