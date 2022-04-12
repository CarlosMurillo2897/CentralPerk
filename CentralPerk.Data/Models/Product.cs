using System.ComponentModel.DataAnnotations;

namespace CentralPerk.Data.Models
{
    public class Product : BaseObject
    {
        [MaxLength(64)]
        public string Name { get; set; }
        [MaxLength(128)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsTaxable { get; set; }
        public bool IsArchived { get; set; }

    }
}
