using Application.Utils;
using Application.ViewModels.MaterialCategory;
using Application.ViewModels.ProductionPlan;
using Application.ViewModels.RawMaterial;
using Application.ViewModels.Role;
using Application.ViewModels.User;
using Application.ViewModels.User.Supplier;
using Application.ViewModels.WarehouseForm;
using Application.ViewModels.WarehouseFormMaterial;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.UserRole;
using Domain.Entities.Warehousing;
using Application.ViewModels.MainWarehouse;
using Application.ViewModels.TempWarehouse;
using Application.ViewModels.PurchasingPlan;
using Application.ViewModels.PurchasingTask;
using Application.ViewModels.Warehouse;
using Application.ViewModels.WarehouseMaterial;
using Application.ViewModels.SupplierAccountRequest;
using Application.ViewModels.PO_Report;
using Application.ViewModels.PurchasingOrder;
using Application.ViewModels.PurchasingOrder.OrderMaterial;
using Application.ViewModels.Notification;
using Application.ViewModels.DeliveryStage;
using Application.ViewModels.DeliveryStage.PurchaseMaterial;
using Application.ViewModels.DeliveryStage.PurchaseMaterialInRequest;
using Application.ViewModels.Product.ProductMaterial;
using Application.ViewModels.Product;
using Application.ViewModels.InspectionForm.MaterialInspectResult;
using Domain.Entities.Inspection;
using Application.ViewModels.InspectionForm;
using Application.ViewModels.InspectionRequest;
using Domain.Entities.Inspect;
using Application.ViewModels.ExpectedMaterial;
using Application.ViewModels.ProductInPlan;
using Application.ViewModels.ProductCategory;

namespace Infrastructure.MapperConfigs
{
    public class MapperConfigs : Profile
    {
        public MapperConfigs()
        {
            // Create mapping between Pagination
            CreateMap(typeof(Pagination<>), typeof(Pagination<>));

            MappingRole();
            MappingUser();
            MappingRawMaterial();
            MappingMaterialCategory();
            MappingUser();
            MappingWarehouse();
            MappingProductionPlan();
            MappingSupplierAccountRequest();
            MappingPO_Report();
            MappingPurchasingPlan();
            MappingPurchasingTask();
            MappingPurchasingOrder();
            MappingOrderMaterial();
            MappingDeliveryStage();
            MappingPurchaseMaterial();
            MappingNotification();
            MappingProduct();
            MappingProductCategory();
            MappingProductMaterial();
            MappingMaterialInspectResult();
            MappingInspectionForm();
            MappingInspectionRequest();
            MappingExpectedMaterial();
            MappingProductInPlan();
        }

        private void MappingNotification()
        {
            CreateMap<Notification, NotificationVM>().ReverseMap();
            CreateMap<Notification, NotificationAddVM>().ReverseMap();
        }

        private void MappingRole()
        {
            CreateMap<Role, RoleVM>().ReverseMap();
            CreateMap<Role, RoleAddVM>().ReverseMap();
        }

        public void MappingRawMaterial()
        {
            CreateMap<RawMaterial, RawMaterialVM>().ReverseMap();
            CreateMap<RawMaterial, RawMaterialAddVM>().ReverseMap();

            CreateMap<WarehouseMaterial, WarehouseMaterialVM>()
            .ForMember(dest => dest.RawMaterial, opt => opt.MapFrom(src => src.RawMaterial));
            CreateMap<RawMaterial, RawMaterialVMforWarehouseMaterial>();
        }
        public void MappingMaterialCategory()
        {
            CreateMap<MaterialCategory, MaterialCategoryVM>().ReverseMap();
            CreateMap<MaterialCategory, MaterialCategoryAddVM>().ReverseMap();
        }

        public void MappingUser()
        {
            CreateMap<User, UserVM>().ReverseMap();
            CreateMap<User, UserUpdateVM>().ReverseMap();
            CreateMap<User, UserSupplierUpdateVM>().ReverseMap();
            CreateMap<User, UserAddVM>().ReverseMap();
            CreateMap<User, UserSupplierAddVM>().ReverseMap();

            CreateMap<Supplier, SupplierAddVM>().ReverseMap();
            CreateMap<Supplier, SupplierUpdateVM>().ReverseMap();
            CreateMap<Supplier, SupplierVM>().ReverseMap();
        }

        public void MappingWarehouse()
        {
            CreateMap<ImportMainWarehouseRequest, ImportMainWarehouseRequestAddVM>().ReverseMap();
            CreateMap<ImportMainWarehouseRequest, ImportMainWarehouseRequestVM>().ReverseMap();
            CreateMap<ImportMainWarehouseRequest, ImportMainWarehouseRequestUpdateVM>().ReverseMap();
            CreateMap<ImportMainWarehouseRequest, ImportMainWarehouseApproveRequestVM>().ReverseMap();
            CreateMap<TempWarehouseRequest, TempWarehouseRequestAddVM>().ReverseMap();
            CreateMap<TempWarehouseRequest, TempWarehouseApproveRequestVM>().ReverseMap();
            CreateMap<TempWarehouseRequest, TempWarehouseRequestVM>().ReverseMap();
            CreateMap<TempWarehouseRequest, TempWarehouseRequestUpdateVM>().ReverseMap();
            CreateMap<WarehouseForm,  WarehouseFormAddVM>().ReverseMap();
            CreateMap<WarehouseForm, WarehouseFormVM>().ReverseMap();
            CreateMap<WarehouseForm, WarehouseFormUpdateVM>().ReverseMap();
            CreateMap<WarehouseFormMaterial, WarehouseFormMaterialAddVM>().ReverseMap();
            CreateMap<WarehouseFormMaterial, WarehouseFormMaterialVM>().ReverseMap();
            CreateMap<WarehouseFormMaterial, WarehouseFormMaterialUpdateVM>().ReverseMap();
            CreateMap<Warehouse, WarehouseAddVM>().ReverseMap();
            CreateMap<Warehouse, WarehouseVM>().ReverseMap();
            CreateMap<Warehouse, WarehouseUpdateVM>().ReverseMap();
            CreateMap<WarehouseMaterial, WarehouseMaterialAddVM>().ReverseMap();
            CreateMap<WarehouseMaterial, WarehouseMaterialVM>().ReverseMap();
            CreateMap<WarehouseMaterial, WarehouseMaterialUpdateVM>().ReverseMap();

            CreateMap<Warehouse, WarehouseVM>()
            .ForMember(dest => dest.WarehouseMaterials, opt => opt.MapFrom(src => src.WarehouseMaterials)).ReverseMap();
            CreateMap<WarehouseMaterial, WarehouseMaterialVM>().ReverseMap();

            CreateMap<WarehouseForm, UpdateStatusImportWarehouseForm>().ReverseMap();
            CreateMap<WarehouseFormMaterial, UpdateWarehouseFormMaterial>().ReverseMap();
            CreateMap<WarehouseForm, UpdateStatusImportWarehouseForm>().ReverseMap();
            CreateMap<WarehouseFormMaterial, UpdateWarehouseFormMaterial>().ReverseMap();
        }


        public void MappingProductionPlan()
        {
            CreateMap<ProductionPlan, ProductionPlanVM>().ReverseMap();
            CreateMap<ProductionPlan, ProductionPlanAddVM>().ReverseMap();
            CreateMap<ProductionPlan, ProductionPlanUpdateVM>().ReverseMap();
        }

        public void MappingPurchasingPlan()
        {
            CreateMap<PurchasingPlan, PurchasingPlanVM>().ReverseMap();
            CreateMap<PurchasingPlan, PurchasingPlanAddVM>().ReverseMap();
            CreateMap<PurchasingPlan, PurchasingPlanUpdateVM>().ReverseMap();
        }

        public void MappingPurchasingTask()
        {
            CreateMap<PurchasingTask, PurchasingTaskVM>().ReverseMap();
            CreateMap<PurchasingTask, PurchasingTaskAddVM>().ReverseMap();
            CreateMap<PurchasingTask, PurchasingTaskUpdateVM>().ReverseMap();
            CreateMap<PurchasingTask, PurchasingTaskAssignVM>().ReverseMap();
        }

        public void MappingSupplierAccountRequest()
        {
            CreateMap<SupplierAccountRequest, SupplierAccountRequestVM>().ReverseMap();
            CreateMap<SupplierAccountRequest, SupplierAccountRequestAddVM>().ReverseMap();
            CreateMap<SupplierAccountRequest, SupplierAccountRequestUpdateVM>().ReverseMap();
            CreateMap<Supplier, SupplierAccountRequest>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<User, SupplierAccountRequest>().ReverseMap();
        }

        public void MappingPO_Report()
        {
            CreateMap<PO_Report, PO_ReportVM>().ReverseMap();
            CreateMap<PO_Report, PO_ReportAddVM>().ReverseMap();
            CreateMap<PO_Report, PO_ReportSupplierUpdateVM>().ReverseMap();
            CreateMap<PO_Report, PO_ReportPurchasingStaffUpdateVM>().ReverseMap();
        }

        public void MappingPurchasingOrder()
        {
            CreateMap<PurchasingOrder, PurchasingOrderVM>().ReverseMap();
            CreateMap<PurchasingOrder, PurchasingOrderAddVM>().ReverseMap();
            CreateMap<PurchasingOrder, POTitleVM>().ReverseMap();
            CreateMap<PurchasingOrder, PurchasingOrderUpdateVM>().ReverseMap();
        }

        public void MappingOrderMaterial()
        {
            CreateMap<OrderMaterial, OrderMaterialVM>().ReverseMap();
            CreateMap<OrderMaterial, OrderMaterialAddVM>().ReverseMap();
            CreateMap<OrderMaterial, OrderMaterialUpdateVM>().ReverseMap();
        }

        public void MappingDeliveryStage()
        {
            CreateMap<DeliveryStage, DeliveryStageVM>().ReverseMap();
            CreateMap<DeliveryStage, DeliveryStageAddVM>().ReverseMap();
            CreateMap<DeliveryStage, DeliveryStageUpdateVM>().ReverseMap();
            CreateMap<DeliveryStage, DeliveryStageInRequestVM>().ReverseMap();
            CreateMap<DeliveryStage, DeliveryStageUpdateQuantityVM>().ReverseMap();
            CreateMap<DeliveryStage, DeliveryStageWithInspectionRequestVM>().ReverseMap();
        }

        public void MappingPurchaseMaterial()
        {
            CreateMap<PurchaseMaterial, PurchaseMaterialVM>().ReverseMap();
            CreateMap<PurchaseMaterial, PurchaseMaterialAddVM>().ReverseMap();
            CreateMap<PurchaseMaterial, PurchaseMaterialUpdateVM>().ReverseMap();
            CreateMap<PurchaseMaterial, PurchaseMaterialInRequestVM>().ReverseMap();
            CreateMap<PurchaseMaterial, PurchaseMaterialUpdateQuantityVM>().ReverseMap();
            CreateMap<PurchaseMaterial, PurchaseMaterialInFormVM>().ReverseMap();
        }

        public void MappingProductCategory()
        {
            CreateMap<ProductCategory, ProductCategoryVM>().ReverseMap();
            CreateMap<ProductCategory, ProductCategoryAddVM>().ReverseMap();
            CreateMap<ProductCategory, ProductCategoryUpdateVM>().ReverseMap();
        }
        public void MappingProduct()
        {
            CreateMap<Product, ProductVM>().ReverseMap();
            CreateMap<Product, ProductAddVM>().ReverseMap();
            CreateMap<Product, ProductUpdateVM>().ReverseMap();
        }

        public void MappingProductMaterial()
        {
            CreateMap<ProductMaterial, ProductMaterialVM>().ReverseMap();
            CreateMap<ProductMaterial, ProductMaterialAddVM>().ReverseMap();
            CreateMap<ProductMaterial, ProductMaterialUpdateVM>().ReverseMap();
        }

        public void MappingMaterialInspectResult()
        {
            CreateMap<MaterialInspectResult, MaterialInspectResultVM>().ReverseMap();
            CreateMap<MaterialInspectResult, MaterialInspectResultAddVM>().ReverseMap();
            CreateMap<MaterialInspectResult, MaterialInspectResultUpdateVM>().ReverseMap();
        }

        public void MappingInspectionForm()
        {
            CreateMap<InspectionForm, InspectionFormVM>().ReverseMap();
            CreateMap<InspectionForm, InspectionFormAddVM>().ReverseMap();
            CreateMap<InspectionForm, InspectionFormUpdateVM>().ReverseMap();
        }

        public void MappingInspectionRequest()
        {
            CreateMap<InspectionRequest, InspectionRequestVM>().ReverseMap();
            CreateMap<InspectionRequest, InspectionRequestAddVM>().ReverseMap();
            CreateMap<InspectionRequest, InspectionRequestUpdateVM>().ReverseMap();
            CreateMap<InspectionRequest, InspectionApproveRequestVM>().ReverseMap();
        }

        public void MappingExpectedMaterial()
        {
            CreateMap<ExpectedMaterial, ExpectedMaterialVM>().ReverseMap();
            CreateMap<ExpectedMaterial, ExpectedMaterialAddVM>().ReverseMap();
        }

        public void MappingProductInPlan()
        {
            CreateMap<ProductInPlan, ProductInPlanVM>().ReverseMap();
            CreateMap<ProductInPlan, ProductInPlanAddVM>().ReverseMap();
        }
    }
}
