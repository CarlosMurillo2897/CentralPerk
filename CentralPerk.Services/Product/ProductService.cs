using System;
using System.Linq;
using CentralPerk.Data;
using System.Collections.Generic;
using CentralPerk.Data.Models;

namespace CentralPerk.Services.Product
{
    public class ProductService : IProductService
    {

        private readonly CentralPerkDbContext _db;

        public ProductService(CentralPerkDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Retrieves All Products from DB.
        /// </summary>
        /// <returns>List of Products.</returns>
        public List<Data.Models.Product> GetAllProducts()
        {
            return _db.Products.ToList();
        }

        /// <summary>
        /// Retrieves Product by ID from DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product</returns>
        public Data.Models.Product GetProductById(int id)
        {
            return _db.Products.Find(id);
        }

        /// <summary>
        /// Adds a new Product to DB.
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Product</returns>
        public ServiceResponse<Data.Models.Product> CreateProduct(Data.Models.Product product)
        {
            try
            {
                var newInventory = new ProductInventory
                {
                    Product = product,
                    QuantityOnHand = 0,
                    IdealQuantity = 10
                };

                _db.Products.Add(product);
                _db.ProductInventories.Add(newInventory);
                _db.SaveChanges();
                
                return new ServiceResponse<Data.Models.Product>
                {
                    Data = product,
                    IsSuccess = true,
                    Message = "Created new Product.",
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Product>
                {
                    Data = product,
                    IsSuccess = false,
                    Message = "StackTrace: " + e.StackTrace,
                };
            }
        }

        /// <summary>
        /// Archive Product from DB by setting boolean isArchived to true.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product</returns>
        public ServiceResponse<Data.Models.Product> ArchiveProduct(int id)
        {
            try
            {
                var product = _db.Products.Find(id);
                product.IsArchived = true;
                _db.SaveChanges();

                return new ServiceResponse<Data.Models.Product>
                {
                    Data = product,
                    IsSuccess = true,
                    Message = "Archived Product.",
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Product>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "Stack Trace: " + e.StackTrace
                };
            }
        }
    }
}
