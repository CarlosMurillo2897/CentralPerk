using System;

namespace CentralPerk.Data.Models
{
    public class BaseObject
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
