using System.Collections.Generic;
using CentralPerk.Data.Models;

namespace CentralPerk.Services.Inventory
{
    public interface IInventoryService
    {
        public List<ProductInventory> GetCurrentInventory();
        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment);
        public ProductInventory GetByProductId(int productId);
        public List<ProductInventorySnapshot> GetSnapshotHistory();
    }
}
