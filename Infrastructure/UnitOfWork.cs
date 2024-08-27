using Application;
using Application.IRepos;
using Application.IRepos.IInspectionRepo;
using Application.IRepos.UserRoleRepo;
using Application.IRepos.WarehousingRepo;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IDeliveryStageRepo _deliveryStageRepo;
        private readonly IMaterialCategoryRepo _materialCategoryRepo;
        private readonly IOrderMaterialRepo _orderMaterialDetailRepo;
        private readonly IPO_ReportRepo _po_ReportRepo;
        private readonly IProductionPlanRepo _productionPlanRepo;
        private readonly IPurchaseMaterialDetailRepo _purchaseMaterialDetailRepo;
        private readonly IPurchasingOrderRepo _purchasingOrderRepo;
        private readonly IPurchasingPlanRepo _purchasingPlanRepo;
        private readonly IPurchasingTaskRepo _purchasingTaskRepo;
        private readonly IRawMaterialRepo _rawMaterialRepo;
        private readonly IRoleRepo _roleRepo;
        private readonly IUserRepo _userRepo;
        private readonly IWarehouseMaterialRepo _warehouseMaterialRepo;
        private readonly IWarehouseRepo _warehouseRepo;
        private readonly IInspectorRepo _inspectorRepo;
        private readonly IManagerRepo _managerRepo;
        private readonly IPurchasingManagerRepo _purchasingManagerRepo;
        private readonly IPurchasingStaffRepo _purchasingStaffRepo;
        private readonly ISupplierRepo _supplierRepo;
        private readonly IWarehouseStaffRepo _warehouseStaffRepo;
        private readonly IImportMainWarehouseRequestRepo _importMainWarehouseRequestRepo;
        private readonly ITempWarehouseRequestRepo _tempWarehouseRequestRepo;
        private readonly IWarehouseFormRepo _warehouseFormRepo;
        private readonly IWarehouseFormMaterialRepo _warehouseFormMaterialRepo;
        private readonly INotificationRepo _notificationRepo;
        private readonly ISupplierAccountRequestRepo _supplierAccountRequestRepo;
        private readonly IInspectionFormRepo _inspectionFormRepo;
        private readonly IInspectionRequestRepo _inspectionRequestRepo;
        private readonly IMaterialInspectResultRepo _materialInspectResultRepo;
        private readonly IProductInPlanRepo _productInPlanRepo;
        private readonly IProductMaterialRepo _productMaterialRepo;
        private readonly IProductCategoryRepo _productCategoryRepo;
        private readonly IExpectedMaterialRepo _planMaterialRepo;
        private readonly IProductRepo _productRepo;

        public UnitOfWork(AppDbContext context,
    IDeliveryStageRepo deliveryStageRepo,
    IMaterialCategoryRepo materialCategoryRepo,
    IOrderMaterialRepo orderMaterialDetailRepo,
    IPO_ReportRepo po_ReportRepo,
    IProductionPlanRepo productionPlanRepo,
    IPurchaseMaterialDetailRepo purchaseMaterialDetailRepo,
    IPurchasingOrderRepo purchasingOrderRepo,
    IPurchasingPlanRepo purchasingPlanRepo,
    IPurchasingTaskRepo purchasingTaskRepo,
    IRawMaterialRepo rawMaterialRepo,
    IRoleRepo roleRepo,
    IUserRepo userRepo,
    IWarehouseMaterialRepo warehouseMaterialRepo,
    IWarehouseRepo warehouseRepo,
    IInspectorRepo inspectorRepo,
    IManagerRepo managerRepo,
    IPurchasingManagerRepo purchasingManagerRepo,
    IPurchasingStaffRepo purchasingStaffRepo,
    ISupplierRepo supplierRepo,
    IWarehouseStaffRepo warehouseStaffRepo,
    IImportMainWarehouseRequestRepo importMainWarehouseRequestRepo,
    ITempWarehouseRequestRepo tempWarehouseRequestRepo,
    IWarehouseFormRepo warehouseFormRepo,
    IWarehouseFormMaterialRepo warehouseFormMaterialRepo,
    INotificationRepo notificationRepo,
    ISupplierAccountRequestRepo supplierAccountRequestRepo,
    IInspectionFormRepo inspectionFormRepo,
    IInspectionRequestRepo inspectionRequestRepo,
    IMaterialInspectResultRepo materialInspectResultRepo,
    IProductInPlanRepo productInPlanRepo,
    IProductMaterialRepo productMaterialRepo,
    IProductCategoryRepo productCategoryRepo,
    IExpectedMaterialRepo planMaterialRepo,
    IProductRepo productRepo)
        {
            _context = context;
            _deliveryStageRepo = deliveryStageRepo;
            _materialCategoryRepo = materialCategoryRepo;
            _orderMaterialDetailRepo = orderMaterialDetailRepo;
            _po_ReportRepo = po_ReportRepo;
            _productionPlanRepo = productionPlanRepo;
            _purchaseMaterialDetailRepo = purchaseMaterialDetailRepo;
            _purchasingOrderRepo = purchasingOrderRepo;
            _purchasingPlanRepo = purchasingPlanRepo;
            _purchasingTaskRepo = purchasingTaskRepo;
            _rawMaterialRepo = rawMaterialRepo;
            _roleRepo = roleRepo;
            _userRepo = userRepo;
            _warehouseMaterialRepo = warehouseMaterialRepo;
            _warehouseRepo = warehouseRepo;
            _inspectorRepo = inspectorRepo;
            _managerRepo = managerRepo;
            _purchasingManagerRepo = purchasingManagerRepo;
            _purchasingStaffRepo = purchasingStaffRepo;
            _supplierRepo = supplierRepo;
            _warehouseStaffRepo = warehouseStaffRepo;
            _importMainWarehouseRequestRepo = importMainWarehouseRequestRepo;
            _tempWarehouseRequestRepo = tempWarehouseRequestRepo;
            _warehouseFormRepo = warehouseFormRepo;
            _warehouseFormMaterialRepo = warehouseFormMaterialRepo;
            _notificationRepo = notificationRepo;
            _supplierAccountRequestRepo = supplierAccountRequestRepo;
            _inspectionFormRepo = inspectionFormRepo;
            _inspectionRequestRepo = inspectionRequestRepo;
            _materialInspectResultRepo = materialInspectResultRepo;
            _productInPlanRepo = productInPlanRepo;
            _productMaterialRepo = productMaterialRepo;
            _productCategoryRepo = productCategoryRepo;
            _planMaterialRepo = planMaterialRepo;
            _productRepo = productRepo;
        }

        public IDeliveryStageRepo DeliveryStageRepo => _deliveryStageRepo;
        public IMaterialCategoryRepo MaterialCategoryRepo => _materialCategoryRepo;
        public IOrderMaterialRepo OrderMaterialRepo => _orderMaterialDetailRepo;
        public IPO_ReportRepo PO_ReportRepo => _po_ReportRepo;
        public IProductionPlanRepo ProductionPlanRepo => _productionPlanRepo;
        public IPurchaseMaterialDetailRepo PurchaseMaterialRepo => _purchaseMaterialDetailRepo;
        public IPurchasingOrderRepo PurchasingOrderRepo => _purchasingOrderRepo;
        public IPurchasingPlanRepo PurchasingPlanRepo => _purchasingPlanRepo;

        public IPurchasingTaskRepo PurchasingTaskRepo => _purchasingTaskRepo;
        public IRawMaterialRepo RawMaterialRepo => _rawMaterialRepo;
        public IRoleRepo RoleRepo => _roleRepo;
        public IUserRepo UserRepo => _userRepo;
        public IWarehouseMaterialRepo WarehouseMaterialRepo => _warehouseMaterialRepo;
        public IWarehouseRepo WarehouseRepo => _warehouseRepo;
        public IInspectorRepo InspectorRepo => _inspectorRepo;
        public IManagerRepo ManagerRepo => _managerRepo;
        public IPurchasingManagerRepo PurchasingManagerRepo => _purchasingManagerRepo;
        public IPurchasingStaffRepo PurchasingStaffRepo => _purchasingStaffRepo;
        public ISupplierRepo SupplierRepo => _supplierRepo;
        public IWarehouseStaffRepo WarehouseStaffRepo => _warehouseStaffRepo;
        public IImportMainWarehouseRequestRepo ImportMainWarehouseRequestRepo => _importMainWarehouseRequestRepo;
        public ITempWarehouseRequestRepo TempWarehouseRequestRepo => _tempWarehouseRequestRepo;
        public IWarehouseFormRepo WarehouseFormRepo => _warehouseFormRepo;
        public IWarehouseFormMaterialRepo WarehouseFormMaterialRepo => _warehouseFormMaterialRepo;
        public INotificationRepo NotificationRepo => _notificationRepo;
        public ISupplierAccountRequestRepo SupplierAccountRequestRepo => _supplierAccountRequestRepo;
        public IInspectionFormRepo InspectionFormRepo => _inspectionFormRepo;
        public IInspectionRequestRepo InspectionRequestRepo => _inspectionRequestRepo;
        public IMaterialInspectResultRepo MaterialInspectResultRepo => _materialInspectResultRepo;
        public IProductMaterialRepo ProductMaterialRepo => _productMaterialRepo;
        public IProductCategoryRepo ProductCategoryRepo => _productCategoryRepo;
        public IExpectedMaterialRepo PlanMaterialRepo => _planMaterialRepo;
        public IProductRepo ProductRepo => _productRepo;
        public IProductInPlanRepo ProductInPlanRepo => _productInPlanRepo;

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}