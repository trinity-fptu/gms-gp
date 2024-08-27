using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FluentAPIs
{
    public class WarehouseConfig : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Relationships
            builder.HasMany(x => x.WarehouseMaterials)
                .WithOne(x => x.Warehouse)
                .HasForeignKey(x => x.WarehouseId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
