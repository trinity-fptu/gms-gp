using Application;
using Application.IRepos;
using Application.IRepos.IInspectionRepo;
using Application.IRepos.UserRoleRepo;
using Application.IRepos.WarehousingRepo;
using Application.IServices;
using Application.IServices.IInspectionServices;
using Application.IServices.InspectionServices;
using Application.IServices.WarehousingServices;
using Application.Services;
using Application.Services.InspectionServices;
using Application.Services.WarehousingServices;
using Infrastructure.Repos;
using Infrastructure.Repos.InspectionRepo;
using Infrastructure.Repos.UserRoleRepo;
using Infrastructure.Repos.WarehousingRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfratstructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            #region Config repositories and services goes here...
            services.AddScoped<IDeliveryStageRepo, DeliveryStageRepo>();
            services.AddScoped<IMaterialCategoryRepo, MaterialCategoryRepo>();
            services.AddScoped<IOrderMaterialRepo, OrderMaterialDetailRepo>();
            services.AddScoped<IPO_ReportRepo, PO_ReportRepo>();
            services.AddScoped<IProductionPlanRepo, ProductionPlanRepo>();
            services.AddScoped<IPurchaseMaterialDetailRepo, PurchaseMaterialRepo>();
            services.AddScoped<IPurchasingOrderRepo, PurchasingOrderRepo>();
            services.AddScoped<IPurchasingPlanRepo, PurchasingPlanRepo>();
            services.AddScoped<IPurchasingTaskRepo, PurchasingTaskRepo>();
            services.AddScoped<IRawMaterialRepo, RawMaterialRepo>();
            services.AddScoped<IRoleRepo, RoleRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IWarehouseMaterialRepo, WarehouseMaterialRepo>();
            services.AddScoped<IWarehouseRepo, WarehouseRepo>();
            services.AddScoped<IInspectorRepo, InspectorRepo>();
            services.AddScoped<IManagerRepo, ManagerRepo>();
            services.AddScoped<IPurchasingManagerRepo, PurchasingManagerRepo>();
            services.AddScoped<IPurchasingStaffRepo, PurchasingStaffRepo>();
            services.AddScoped<ISupplierRepo, SupplierRepo>();
            services.AddScoped<IWarehouseStaffRepo, WarehouseStaffRepo>();
            services.AddScoped<ITempWarehouseRequestRepo, TempWarehouseRequestRepo>();
            services.AddScoped<IImportMainWarehouseRequestRepo, ImportMainWarehouseRequestRepo>();
            services.AddScoped<IWarehouseFormRepo, WarehouseFormRepo>();
            services.AddScoped<IWarehouseFormMaterialRepo, WarehouseFormMaterialRepo>();
            services.AddScoped<INotificationRepo, NotificationRepo>();
            services.AddScoped<ISupplierAccountRequestRepo, SupplierAccountRequestRepo>();
            services.AddScoped<IInspectionFormRepo, InspectionFormRepo>();
            services.AddScoped<IInspectionRequestRepo, InspectionRequestRepo>();
            services.AddScoped<IMaterialInspectResultRepo, MaterialInspectResultRepo>();
            services.AddScoped<IProductMaterialRepo, ProductMaterialRepo>();
            services.AddScoped<IProductCategoryRepo, ProductCategoryRepo>();
            services.AddScoped<IExpectedMaterialRepo, ExpectedMaterialRepo>();
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<IProductInPlanRepo, ProductInPlanRepo>();


            services.AddScoped<IDeliveryStageService, DeliveryStageService>();
            services.AddScoped<IPurchaseMaterialService, PurchaseMaterialService>();
            services.AddScoped<IMaterialCategoryService, MaterialCategoryService>();
            services.AddScoped<IOrderMaterialService, OrderMaterialService>();
            services.AddScoped<IPO_ReportService, PO_ReportService>();
            services.AddScoped<IProductionPlanService, ProductionPlanService>();
            services.AddScoped<IPurchasingOrderService, PurchasingOrderService>();
            services.AddScoped<IPurchasingTaskService, PurchasingTaskService>();
            services.AddScoped<IPurchasingPlanService, PurchasingPlanService>();
            services.AddScoped<IRawMaterialService, RawMaterialService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWarehouseMaterialService, WarehouseMaterialService>();
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddScoped<ITempWarehouseRequestService, TempWarehouseRequestService>();
            services.AddScoped<IImportMainWarehouseRequestService, ImportMainWarehouseRequestService>();
            services.AddScoped<IWarehouseFormService, WarehouseFormService>();
            services.AddScoped<IWarehouseFormMaterialService, WarehouseFormMaterialService>();
            services.AddScoped<ISupplierAccountRequestService, SupplierAccountRequestService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IInspectionFormService, InspectionFormService>();
            services.AddScoped<IInspectionRequestService, InspectionRequestService>();
            services.AddScoped<IMaterialInspectResultService, MaterialInspectResultService>();
            services.AddScoped<IProductMaterialService, ProductMaterialService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IExpectedMaterialService, ExpectedMaterialService>();    
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductInPlanService, ProductInPlanService>();
            #endregion


            #region TODO: Config validator goes here...
            #endregion

            #region Database config
            // Use local DB
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(config.GetConnectionString("MPMS_DB")));
            #endregion


            return services;
        }
    }
}
