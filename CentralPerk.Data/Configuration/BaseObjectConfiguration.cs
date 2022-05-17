using System;
using CentralPerk.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CentralPerk.Data.Configuration
{
    public static class BaseObjectConfiguration
    {
        // Using generic type "<T>" as safety mechanism. We access BaseObject properties using lambdas, if BaseObject changes properties code will NOT be compiled.
        public static void Configure<T>(EntityTypeBuilder<T> builder) where T : BaseObject
        {
            builder
                .Property<DateTime>(x => x.CreatedOn)
                .HasDefaultValue(DateTime.Now);

            builder
                .Property<DateTime>(x => x.UpdatedOn)
                .HasDefaultValue(DateTime.Now);
        }
    }
}
