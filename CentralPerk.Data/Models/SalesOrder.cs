using System.Collections.Generic;

namespace CentralPerk.Data.Models
{
    public class SalesOrder : BaseObject
    {
        public Customer Customer { get; set; }
        public List<SalesOrderItem> SalesOrderItems { get; set; }
        public bool IsPaid { get; set; }
    }
}
