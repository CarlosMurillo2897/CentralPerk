using System.ComponentModel.DataAnnotations;

namespace CentralPerk.Data.Models
{
    public class CustomerAddress : BaseObject
    {
        [MaxLength(100)]
        public string AddressLine1 { get; set; }
        
        [MaxLength(100)]
        public string AddressLine2 { get; set; }
        
        [MaxLength(100)]
        public string City { get; set; }
        
        [MaxLength(2)]
        public string State { get; set; }
        
        [MaxLength(10)]
        public string PostalCode { get; set; }
        
        [MaxLength(32)]
        public string Country { get; set; }
    }
}