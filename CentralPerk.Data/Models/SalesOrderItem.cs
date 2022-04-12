namespace CentralPerk.Data.Models
{
    public class SalesOrderItem : BaseObject
    {
        public int Quantity { get; set; }
        public Product Product { get; set; }
    }
}
