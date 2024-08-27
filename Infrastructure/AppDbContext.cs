using Domain.Entities;
using Domain.Entities.Inspect;
using Domain.Entities.Inspection;
using Domain.Entities.UserRole;
using Domain.Entities.Warehousing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<OrderMaterial> OrderMaterials { get; set; }
        public DbSet<PO_Report> PO_Reports { get; set; }
        public DbSet<ProductionPlan> ProductionPlans { get; set; }
        public DbSet<PurchasingPlan> PurchasingPlans { get; set; }
        public DbSet<PurchasingTask> PurchasingTasks { get; set; }
        public DbSet<PurchasingOrder> PurchasingOrders { get; set; }
        public DbSet<DeliveryStage> DeliveryStages { get; set; }
        public DbSet<PurchaseMaterial> PurchaseMaterials { get; set; }
        public DbSet<MaterialCategory> MaterialCategories { get; set; }
        public DbSet<RawMaterial> RawMaterials { get; set; }
        public DbSet<TempWarehouseRequest> TempWarehouseRequests { get; set; }
        public DbSet<ImportMainWarehouseRequest> ImportMainWarehouseRequests { get; set; }
        public DbSet<WarehouseForm> WarehouseForms { get; set; }
        public DbSet<WarehouseFormMaterial> WarehouseFormMaterials { get; set; }
        public DbSet<WarehouseMaterial> WarehouseMaterials { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Inspector> Inspectors { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<PurchasingManager> PurchasingManagers { get; set; }
        public DbSet<PurchasingStaff> PurchasingStaffs { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierAccountRequest> SupplierAccountRequests { get; set; }
        public DbSet<WarehouseStaff> WarehouseStaffs { get; set; }
        public DbSet<MaterialInspectResult> MaterialInspectResults { get; set; }
        public DbSet<InspectionForm> InspectionForms { get; set; }
        public DbSet<InspectionRequest> InspectionRequests { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductMaterial> ProductMaterials { get; set; }
        public DbSet<ProductInPlan> ProductInPlans { get; set; }
        public DbSet<ExpectedMaterial> ExpectedMaterials { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.EnableSensitiveDataLogging();
        }

        // DbSet config goes here...
    }
}
