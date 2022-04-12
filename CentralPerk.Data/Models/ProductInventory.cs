namespace CentralPerk.Data.Models
{
    public class ProductInventory : BaseObject
    {
        public int QuantityOnHand { get; set; }
        public int IdealQuantity { get; set; }
        public Product Product { get; set; }
    }
}
