using CentralPerk.Data.Models;
using CentralPerk.Web.ViewModels;

namespace CentralPerk.Web.Serialization
{
    public static class ProductMapper
    {
        /// <summary>
        /// Maps a Product Data Model to Product View Model.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static Product SerializeProductModel(ProductModel product) =>
            new Product
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.UpdatedOn,
                Description = product.Description,
            };
        
        /// <summary>
        /// Maps a Product View Model to Product Data Model.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static ProductModel SerializeProductModel(Product product) =>
            new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.UpdatedOn,
                Description = product.Description,
            };
    }
}
