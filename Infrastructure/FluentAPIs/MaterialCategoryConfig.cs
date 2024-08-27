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
    public class MaterialCategoryConfig : IEntityTypeConfiguration<MaterialCategory>
    {
        public void Configure(EntityTypeBuilder<MaterialCategory> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Relationships
            builder.HasMany(x => x.RawMaterials)
                .WithOne(x => x.MaterialCategory)
                .HasForeignKey(x => x.MaterialCategoryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
