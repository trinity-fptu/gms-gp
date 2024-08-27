using Application.IRepos;
using Application.IRepos.IInspectionRepo;
using Application.IRepos.UserRoleRepo;
using Application.IRepos.WarehousingRepo;

namespace Application
{
    public interface IUnitOfWork
    {
        public IDeliveryStageRepo DeliveryStageRepo { get; }
        public IMaterialCategoryRepo MaterialCategoryRepo { get; }
        public IOrderMaterialRepo OrderMaterialRepo { get; }
        public IPO_ReportRepo PO_ReportRepo { get; }
        public IProductionPlanRepo ProductionPlanRepo { get; }
        public IPurchaseMaterialDetailRepo PurchaseMaterialRepo { get; }
        public IPurchasingOrderRepo PurchasingOrderRepo { get; }
        public IPurchasingPlanRepo PurchasingPlanRepo { get; }
        public IPurchasingTaskRepo PurchasingTaskRepo { get; }
        public IRawMaterialRepo RawMaterialRepo { get; }
        public IRoleRepo RoleRepo { get; }
        public IUserRepo UserRepo { get; }
        public IWarehouseMaterialRepo WarehouseMaterialRepo { get; }
        public IWarehouseRepo WarehouseRepo { get; }
        public IInspectorRepo InspectorRepo { get; }
        public IManagerRepo ManagerRepo { get; }
        public IPurchasingManagerRepo PurchasingManagerRepo { get; }
        public IPurchasingStaffRepo PurchasingStaffRepo { get; }
        public ISupplierRepo SupplierRepo { get; }
        public IWarehouseStaffRepo WarehouseStaffRepo { get; }
        public ITempWarehouseRequestRepo TempWarehouseRequestRepo { get; }
        public IImportMainWarehouseRequestRepo ImportMainWarehouseRequestRepo { get; }
        public IWarehouseFormRepo WarehouseFormRepo { get; }
        public IWarehouseFormMaterialRepo WarehouseFormMaterialRepo { get; }
        public INotificationRepo NotificationRepo { get; }
        public ISupplierAccountRequestRepo SupplierAccountRequestRepo { get; }
        public IInspectionRequestRepo InspectionRequestRepo { get; }
        public IInspectionFormRepo InspectionFormRepo { get; }
        public IMaterialInspectResultRepo MaterialInspectResultRepo { get; }
        public IProductMaterialRepo ProductMaterialRepo { get; }
        public IProductCategoryRepo ProductCategoryRepo { get; }
        public IExpectedMaterialRepo PlanMaterialRepo { get; }
        public IProductRepo ProductRepo { get; }
        public IProductInPlanRepo ProductInPlanRepo { get; }

        public Task<int> SaveChangesAsync();
    }
}