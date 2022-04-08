namespace CentralPerk.Data.Models
{
    public class Customer : BaseObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public CustomerAddress PrimaryAddress { get; set; }
    }
}
