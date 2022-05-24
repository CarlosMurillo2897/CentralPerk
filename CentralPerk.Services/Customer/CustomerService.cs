using System;
using System.Collections.Generic;
using System.Linq;
using CentralPerk.Data;
using Microsoft.EntityFrameworkCore;

namespace CentralPerk.Services.Customer
{
    public class CustomerService : ICustomerService
    {

        public readonly CentralPerkDbContext _db;

        public CustomerService(CentralPerkDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Retrieves a List of Customers order by Last Name.
        /// </summary>
        /// <returns>List of Customers.</returns>
        public List<Data.Models.Customer> GetAllCustomers()
        {
            return _db.Customers
                .Include(c => c.PrimaryAddress)
                .OrderBy(c => c.LastName)
                .ToList();
        }

        /// <summary>
        /// Adds a new Customer.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>Service Response of a Customer.</returns>
        public ServiceResponse<Data.Models.Customer> CreateCustomer(Data.Models.Customer customer)
        {
            try
            {
                _db.Add(customer);
                _db.SaveChanges();

                return new ServiceResponse<Data.Models.Customer>
                {
                    Data = customer,
                    IsSuccess = true,
                    Message = "Customer Created."
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Customer>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "StackTrace: " + e.StackTrace
                };
            }
        }

        /// <summary>
        /// Deletes a Customer by ID.
        /// </summary>
        /// <param name="id">Customer int primary key</param>
        /// <returns>Service Response of a bool.</returns>
        public ServiceResponse<bool> DeleteCustomer(int id)
        {
            var customer = _db.Customers.Find(id);
            if (customer == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    IsSuccess = false,
                    Message = "Failed: Customer Delete. Reason: Customer not found."
                };
            }

            try
            {
                _db.Customers.Remove(customer);
                _db.SaveChanges();

                return new ServiceResponse<bool>
                {
                    Data = true,
                    IsSuccess = true,
                    Message = "Customer Deleted."
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    IsSuccess = false,
                    Message = "Stack Trace: " + e.StackTrace
                };
            }
        }

        /// <summary>
        /// Retrieves Customer by ID.
        /// </summary>
        /// <param name="id">Customer int primary key.</param>
        /// <returns>Customer.</returns>
        public Data.Models.Customer GetById(int id)
        {
            return _db.Customers.Find(id);
        }
    }
}
