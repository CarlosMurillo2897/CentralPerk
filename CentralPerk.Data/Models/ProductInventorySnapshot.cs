namespace CentralPerk.Data.Models
{
    public class ProductInventorySnapshot : BaseObject
    {
        public int QuantityOnHand { get; set; }
        public Product Product { get; set; }

    }
}
