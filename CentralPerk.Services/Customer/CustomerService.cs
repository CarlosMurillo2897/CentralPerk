using System.Collections.Generic;
using CentralPerk.Data;

namespace CentralPerk.Services.Customer
{
    public class CustomerService : ICustomerService
    {

        public readonly CentralPerkDbContext _db;

        public CustomerService(CentralPerkDbContext db)
        {
            _db = db;
        }

        public List<Data.Models.Customer> GetAllCustomers()
        {
            throw new System.NotImplementedException();
        }

        public ServiceResponse<Data.Models.Customer> CreateCustomer(Data.Models.Customer customer)
        {
            throw new System.NotImplementedException();
        }

        public ServiceResponse<bool> DeleteCustomer(int id)
        {
            throw new System.NotImplementedException();
        }

        public Data.Models.Customer GetById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
