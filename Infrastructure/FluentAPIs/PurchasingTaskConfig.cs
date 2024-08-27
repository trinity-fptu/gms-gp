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
    public class PurchasingTaskConfig : IEntityTypeConfiguration<PurchasingTask>
    {
        public void Configure(EntityTypeBuilder<PurchasingTask> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Relationships
            builder.HasOne(x => x.PurchasingPlan)
                .WithMany(x => x.PurchaseTasks)
                .HasForeignKey(x => x.PurchasingPlanId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.RawMaterial)
                .WithMany(x => x.PurchasingTasks)
                .HasForeignKey(x => x.RawMaterialId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.PurchasingStaff)
                .WithMany(x => x.PurchasingTasks)
                .HasForeignKey(x => x.PurchasingStaffId);
        }
    }
}
