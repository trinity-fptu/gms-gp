using Domain.Entities;
using Domain.Entities.UserRole;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FluentAPIs.UserRoleConfig
{
    public class InspectorConfig : IEntityTypeConfiguration<Inspector>
    {
        public void Configure(EntityTypeBuilder<Inspector> builder)
        {
            builder.ToTable("Inspectors");
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.InspectionRequests)
                .WithOne(x => x.ApprovingInspector)
                .HasForeignKey(x => x.ApprovingInspectorId);
            builder.HasMany(x => x.ImportMainWarehouseRequests)
                .WithOne(x => x.Inspector)
                .HasForeignKey(x => x.InspectorId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.TempWarehouseRequests)
                .WithOne(x => x.RequestInspector)
                .HasForeignKey(x => x.RequestInspectorId);
        }
    }
}
