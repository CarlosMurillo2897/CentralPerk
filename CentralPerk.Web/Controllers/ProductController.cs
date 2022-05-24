using System.Linq;
using CentralPerk.Services.Product;
using CentralPerk.Web.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CentralPerk.Web.Controllers {
    
    [ApiController]
    public class ProductController : ControllerBase
    {
        
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController (
            ILogger<ProductController> logger,
            IProductService productService
        ) {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet("/api/product")]
        public ActionResult GetProduct ()
        {
            _logger.LogInformation("Getting all Products.");
            var products = _productService.GetAllProducts();
            var productViewModels = products.Select(ProductMapper.SerializeProductModel);
            return Ok(productViewModels);
        }
    }
}