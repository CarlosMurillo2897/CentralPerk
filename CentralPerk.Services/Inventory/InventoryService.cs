using System;
using System.Collections.Generic;
using System.Linq;
using CentralPerk.Data;
using CentralPerk.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace CentralPerk.Services.Inventory
{
    public class InventoryService : IInventoryService
    {

        private readonly CentralPerkDbContext _db;
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(
            CentralPerkDbContext db,
            ILogger<InventoryService> logger
        )
        {
            _db = db;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all Current Inventories.
        /// </summary>
        /// <returns>List of Product Inventories.</returns>
        public List<ProductInventory> GetCurrentInventory()
        {
            return _db.ProductInventories
                .Include(pi => pi.Product)
                .Where(pi => !pi.Product.IsArchived)
                .ToList();
        }

        /// <summary>
        /// Updates Units Available from Product Inventory by Product ID.
        /// Adjusts QuantityOnHand by adjustment value.
        /// </summary>
        /// <param name="id">productId</param>
        /// <param name="adjustment">number of units added / removed from.</param>
        /// <returns>Service Response of Product Inventory.</returns>
        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment)
        {
            try
            {
                var inventory = _db.ProductInventories
                    .Include(pi => pi.Product)
                    .First(pi => pi.Product.Id == id);

                inventory.QuantityOnHand += adjustment;

                try
                {
                    CreateSnapshot(inventory);
                }
                catch (Exception e)
                {
                    _logger.LogError("Error Creating Inventory Snapshot.");
                    _logger.LogError(e.StackTrace);
                }

                _db.SaveChanges();

                return new ServiceResponse<ProductInventory>
                {
                    Data = inventory,
                    IsSuccess = true,
                    Message = $"Product {id} Inventory Updated."
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<ProductInventory>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = $"Failed: Product Inventory Update."
                };
            }
        }

        /// <summary>
        /// Retrieves a Product Inventory by productId.
        /// </summary>
        /// <param name="productId">Product int primary key.</param>
        /// <returns>Product Inventory.</returns>
        public ProductInventory GetByProductId(int productId)
        {
            return _db.ProductInventories
                .Include(pi => pi.Product)
                .FirstOrDefault(pi => pi.Id == productId);
        }

        /// <summary>
        /// Retrieve Snapshot History for the previous 6 hours.
        /// </summary>
        /// <returns>List of Product Inventory Snapshots.</returns>
        public List<ProductInventorySnapshot> GetSnapshotHistory()
        {
            var earliest = DateTime.UtcNow - TimeSpan.FromHours(6);
            return _db.ProductInventorySnapshots
                .Include(snap => snap.Product)
                .Where(
                    snap => snap.CreatedOn > earliest && !snap.Product.IsArchived
                    )
                .ToList();
        }

        /// <summary>
        /// Create a Product Inventory Snapshot.
        /// </summary>
        /// <param name="inventory"></param>
        private void CreateSnapshot(ProductInventory inventory)
        {
            var snapshot = new ProductInventorySnapshot
            {
                Product = inventory.Product,
                QuantityOnHand = inventory.QuantityOnHand
            };

            _db.Add(snapshot);
        }
    }
}
