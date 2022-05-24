using System;
using System.Collections.Generic;
using System.Linq;
using CentralPerk.Data;
using CentralPerk.Data.Models;
using CentralPerk.Services.Inventory;
using CentralPerk.Services.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CentralPerk.Services.Order
{
    public class OrderService : IOrderService
    {

        private readonly CentralPerkDbContext _db;
        private readonly ILogger<OrderService> _logger;
        private readonly IProductService _productService;
        private readonly IInventoryService _inventoryService;

        public OrderService(
            CentralPerkDbContext db,
            ILogger<OrderService> logger,
            IProductService productService,
            IInventoryService inventoryService
        )
        {
            _db = db;
            _logger = logger;
            _productService = productService;
            _inventoryService = inventoryService;
        }
        
        /// <summary>
        /// Retrieves all Orders.
        /// </summary>
        /// <returns>List of Sales Orders.</returns>
        public List<SalesOrder> GetOrders()
        {
            return _db.SalesOrders
                .Include(so => so.Customer)
                    .ThenInclude(c => c.PrimaryAddress)
                .Include(so => so.SalesOrderItems)
                    .ThenInclude(soi => soi.Product)
                .ToList();
        }

        /// <summary>
        /// Create an open Sales Order.
        /// </summary>
        /// <param name="order"></param>
        /// <returns>Service Response of a bool.</returns>
        public ServiceResponse<bool> GenerateOpenOrder(SalesOrder order)
        {
            _logger.LogInformation("Generating new Order.");

            foreach (var item in order.SalesOrderItems)
            {
                item.Product = _productService.GetProductById(item.Product.Id);
                item.Quantity = item.Quantity;

                var inventoryId = _inventoryService.GetByProductId(item.Product.Id).Id;
                _inventoryService.UpdateUnitsAvailable(inventoryId, -item.Quantity);
            }

            try
            {
                _db.Add(order);
                _db.SaveChanges();

                return new ServiceResponse<bool>
                {
                    Data = true,
                    IsSuccess = true,
                    Message = "Sales Order Created."
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
        /// Mark an open SalesOrder as paid by Order ID.
        /// </summary>
        /// <param name="id">Order ID.</param>
        /// <returns>Service Response of a bool.</returns>
        public ServiceResponse<bool> MarkFulfilled(int id)
        {
            var order = _db.SalesOrders.Find(id);
            order.UpdatedOn = DateTime.UtcNow;
            order.IsPaid = true;

            try
            {
                _db.Update(order);
                _db.SaveChanges();

                return new ServiceResponse<bool>
                {
                    Data = true,
                    IsSuccess = true,
                    Message = $"Order ${id} Closed: Invoice paid in full."
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>
                {
                    Data = true,
                    IsSuccess = true,
                    Message = "Stack Trace: " + e.StackTrace
                };
            }
        }
    }
}
