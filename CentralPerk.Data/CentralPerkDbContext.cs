using System;
using CentralPerk.Data.Configuration;
using CentralPerk.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CentralPerk.Data
{
    public class CentralPerkDbContext : IdentityDbContext
    {
        public CentralPerkDbContext() { }

        public CentralPerkDbContext(DbContextOptions options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Add default values to CreatedOn & UpdatedOn fields.
            BaseObjectConfiguration.Configure(builder.Entity<Customer>());
            BaseObjectConfiguration.Configure(builder.Entity<CustomerAddress>());
            BaseObjectConfiguration.Configure(builder.Entity<Product>());
            BaseObjectConfiguration.Configure(builder.Entity<ProductInventory>());
            BaseObjectConfiguration.Configure(builder.Entity<ProductInventorySnapshot>());
            BaseObjectConfiguration.Configure(builder.Entity<SalesOrder>());
            BaseObjectConfiguration.Configure(builder.Entity<SalesOrderItem>());

            base.OnModelCreating(builder);
        }

        public virtual DbSet<Customer> Customers { get; set;  }
        public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductInventory> ProductInventories { get; set; }
        public virtual DbSet<ProductInventorySnapshot> ProductInventorySnapshots { get; set; }
        public virtual DbSet<SalesOrder> SalesOrders { get; set; }
        public virtual DbSet<SalesOrderItem> SalesOrderItems { get; set; }
    }
}
