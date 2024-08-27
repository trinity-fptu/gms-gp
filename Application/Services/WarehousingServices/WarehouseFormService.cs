using Application.Exceptions;
using Application.IServices;
using Application.IServices.WarehousingServices;
using Application.ViewModels.TempWarehouse;
using Application.ViewModels.WarehouseForm;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Warehousing;
using Domain.Enums.DeliveryStage;
using Domain.Enums;
using System.Net;
using Domain.Enums.Warehousing;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using Application.ViewModels.WarehouseFormMaterial;
using Application.ViewModels.WarehouseMaterial;
using ClosedXML.Excel;
using Application.Utils;
using DocumentFormat.OpenXml;
using System;
using static Domain.Enums.RawMaterialEnum;
using Application.ViewModels.RawMaterial;
using DocumentFormat.OpenXml.Spreadsheet;
using Application.ViewModels.DeliveryStage.PurchaseMaterial;
using System.IO;
using DocumentFormat.OpenXml.Office2016.Excel;
using Domain.Enums.PurchasingOrder;
using Application.ViewModels.MainWarehouse;

namespace Application.Services.WarehousingServices
{
    public class WarehouseFormService : IWarehouseFormService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;
        private readonly IDeliveryStageService _deliveryStageService;

        public WarehouseFormService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService, IDeliveryStageService deliveryStageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsService = claimsService;
            _deliveryStageService = deliveryStageService;
        }

        public async Task CreateAsync(WarehouseFormAddVM warehouseFormAddVM)
        {
            if (warehouseFormAddVM.TempWarehouseRequestId != null)
            {
                var tempWarehouseRequest = await _unitOfWork.TempWarehouseRequestRepo.GetByIdAsync(warehouseFormAddVM.TempWarehouseRequestId.Value);
                if (tempWarehouseRequest == null)
                {
                    throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND + " - Temp warehouse request");
                }
                if (tempWarehouseRequest.ApproveStatus != ApproveEnum.Approved)
                {
                    throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.REQUEST_NOTAPPROVE), ExceptionMessage.REQUEST_NOTAPPROVE);
                }
            }

            // Calculate total price for each material
            warehouseFormAddVM.WarehouseFormMaterials.ForEach(x =>
                x.TotalPrice = x.RequestQuantity * x.PackagePrice);

            var createdWarehouseForm = _mapper.Map<WarehouseForm>(warehouseFormAddVM);

            if (createdWarehouseForm.FormType == WarehouseFormTypeEnum.Import)
            {
                if (createdWarehouseForm.ReceiveWarehouse == WarehouseTypeEnum.TempWarehouse)
                {
                    var currentUserId = _claimsService.GetCurrentUserId;
                    var currentUser = await _unitOfWork.UserRepo.GetByIdAsync(currentUserId);
                    var tempWarehouseRequest = await _unitOfWork.TempWarehouseRequestRepo.GetByIdAsync(createdWarehouseForm.TempWarehouseRequestId.Value);
                    if (currentUser == null || tempWarehouseRequest.ApproveWStaffId != currentUser.WarehouseStaffId)
                    {
                        throw new APIException(HttpStatusCode.Unauthorized,
                            ExceptionMessage.NOT_ALLOW, ExceptionMessage.NOT_ALLOW);
                    }
                }
            }

            await CheckForDuplicateWarehouseFormMaterial(createdWarehouseForm);

            await _unitOfWork.WarehouseFormRepo.AddAsync(createdWarehouseForm);
            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_CREATE_ERROR), ExceptionMessage.ENTITY_CREATE_ERROR);
        }

        public async Task<WarehouseFormVM> GetByIdAsync(int id)
        {
            var warehouseForm = await _unitOfWork.WarehouseFormRepo.GetByIdWithMaterialsAsync(id);

            if (warehouseForm == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            }
            var result = _mapper.Map<WarehouseFormVM>(warehouseForm);
            return result;
        }

        public async Task<List<WarehouseFormVM>> GetAllAsync()
        {
            try
            {
                var warehouseForm = await _unitOfWork.WarehouseFormRepo.GetAllAsync();
                var result = _mapper.Map<List<WarehouseFormVM>>(warehouseForm);
                return result;
            }
            catch (Exception ex)
            {
                throw new APIException(HttpStatusCode.InternalServerError,
                    ExceptionMessage.ENTITY_RETRIEVED_ERROR, ExceptionMessage.ENTITY_RETRIEVED_ERROR, ex);
            }
        }

        public async Task UpdateAsync(WarehouseFormUpdateVM warehouseFormUpdateVM)
        {
            var existingWarehouseForm = await _unitOfWork.WarehouseFormRepo.GetByIdAsync(warehouseFormUpdateVM.Id);
            if (existingWarehouseForm == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }

            _mapper.Map(warehouseFormUpdateVM, existingWarehouseForm);
            //_mapper.Map(warehouseFormUpdateVM, existingWarehouseForm);

            await CheckForDuplicateWarehouseFormMaterial(existingWarehouseForm);
            _unitOfWork.WarehouseFormRepo.Update(existingWarehouseForm);
            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }

        public async Task DeleteAsync(int id)
        {
            var itemToDelete = await _unitOfWork.WarehouseFormRepo.GetByIdAsync(id);
            if (itemToDelete == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }

            _unitOfWork.WarehouseFormRepo.SoftRemove(itemToDelete);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_DELETE_ERROR), ExceptionMessage.ENTITY_DELETE_ERROR);
        }

        public async Task<WarehouseFormVM> GetByTempWarehouseRequestIdAsync(int tempWarehouseRequestId)
        {
            var itemList = await _unitOfWork.WarehouseFormRepo.GetAllAsync();
            var item = itemList.Where(x => x.TempWarehouseRequestId == tempWarehouseRequestId).FirstOrDefault();
            var result = _mapper.Map<WarehouseFormVM>(itemList);

            return result;
        }

        public async Task<WarehouseFormVM> GetByImportMainWarehouseRequestIdAsync(int importMainWarehouseRequestId)
        {
            var itemList = await _unitOfWork.WarehouseFormRepo.GetAllAsync();
            var item = itemList.Where(x => x.ImportMainWarehouseRequestId == importMainWarehouseRequestId).FirstOrDefault();
            var result = _mapper.Map<WarehouseFormVM>(itemList);

            return result;
        }

        private async Task CheckForDuplicateWarehouseFormMaterial(WarehouseForm warehouseForm)
        {
            var duplicateId = warehouseForm.WarehouseFormMaterials
                .Where(x => x.IsDeleted == false)
                .GroupBy(e => e.PurchaseMaterialId)
                .Where(g => g.Count() > 1)
                .Select(x => x.Key);

            if (duplicateId.Count() > 0)
            {
                var idString = "";
                foreach (var materialId in duplicateId)
                {
                    //var purchaseMaterial = await _unitOfWork.PurchaseMaterialRepo.GetByIdAsync(materialId.Value);
                    idString += materialId + ", ";
                }

                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + $" - Duplicate material id {idString} in warehouse form");
            }
        }


        // Create a temporary warehouse form (It can be export or import form)
        public async Task CreateTempWarehouseform(int requestId)
        {
            var request = await _unitOfWork.TempWarehouseRequestRepo.GetByIdWithDSAsync(requestId);
            if (DateTime.Today < request.RequestExecutionDate.Value.Date)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                    ExceptionMessage.INVALID_INFORMATION, ExceptionMessage.INVALID_INFORMATION + " - Cannot create warehouse form before the execution date");
            }
            var listInReq = await _unitOfWork.InspectionRequestRepo.GetAllByDeliveryStageIdAsync(request.DeliveryStageId);
            var InReq = listInReq.FirstOrDefault(x => x.ApproveStatus == ApproveEnum.Approved);
            if (request == null)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                    ExceptionMessage.NOT_FOUND, ExceptionMessage.NOT_FOUND + "Request not found");
            }
            if (request.ApproveStatus != ApproveEnum.Approved)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                    ExceptionMessage.REQUEST_NOTAPPROVE, ExceptionMessage.REQUEST_NOTAPPROVE + "Request has not been approved");
            }
            if (request.WarehouseForm != null)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                    ExceptionMessage.REQUESTFORM_ALREADYCREATED, ExceptionMessage.REQUESTFORM_ALREADYCREATED);
            }

            if (request.RequestType == WarehouseRequestTypeEnum.Import)
            {
                var currentUserId = _claimsService.GetCurrentUserId;
                var currentUser = await _unitOfWork.UserRepo.GetByIdAsync(currentUserId);

                if (currentUser == null || request.ApproveWStaffId != currentUser.WarehouseStaffId)
                {
                    throw new APIException(HttpStatusCode.Unauthorized,
                        ExceptionMessage.NOT_ALLOW, ExceptionMessage.NOT_ALLOW);
                }
            }

            if (request.RequestType == WarehouseRequestTypeEnum.Import)
            {
                WarehouseForm warehouseForm = new WarehouseForm();
                warehouseForm.FormCode = "IPTWH" + StringUtils.GenerateRandomNumberString(6);
                warehouseForm.POCode = request.POCode;
                warehouseForm.FormType = WarehouseFormTypeEnum.Import;
                warehouseForm.ReceiveCompanyName = "MPMS Corporation";
                warehouseForm.CompanyAddress = "Lô E2a-7, Đường D1, Đ. D1, Long Thạnh Mỹ, Thành Phố Thủ Đức, Thành phố Hồ Chí Minh";
                warehouseForm.ReceiveWarehouse = WarehouseTypeEnum.TempWarehouse;
                var rqStaff = await _unitOfWork.PurchasingStaffRepo.GetPurchasingStaffByIdAsync(request.RequestStaffId.Value);
                var approveStaff = await _unitOfWork.WarehouseStaffRepo.GetWarehouseStaffByIdAsync(request.ApproveWStaffId.Value);
                var PO = await _unitOfWork.PurchasingOrderRepo.GetByPOCodeAsync(request.POCode);
                var supplier = await _unitOfWork.SupplierRepo.GetSupplierByIdAsync(PO.SupplierId);
                warehouseForm.RequestStaffName = rqStaff.User.FullName;
                warehouseForm.ApproveWarehouseStaffName = approveStaff.User.FullName;
                warehouseForm.SupplierName = supplier.User.FullName;
                warehouseForm.SupplierName = supplier.CompanyName;
                warehouseForm.TempWarehouseRequestId = request.Id;
                List<WarehouseFormMaterial> listMaterial = new List<WarehouseFormMaterial>();
                foreach (var purchaseMaterial in request.DeliveryStage.PurchaseMaterials.Where(x => x.WarehouseStatus != DeliveryStageStatusEnum.SupInactive))
                {
                    WarehouseFormMaterial whMaterial = new WarehouseFormMaterial();
                    whMaterial.PurchaseMaterialId = purchaseMaterial.Id;
                    whMaterial.MaterialName = purchaseMaterial.MaterialName;
                    var rawMaterial = await _unitOfWork.RawMaterialRepo.GetByIdAsync(purchaseMaterial.RawMaterialId);
                    whMaterial.MaterialCode = rawMaterial.Code;
                    whMaterial.RequestQuantity = purchaseMaterial.DeliveredQuantity;
                    whMaterial.MaterialPerPackage = purchaseMaterial.MaterialPerPackage;
                    whMaterial.PackagePrice = purchaseMaterial.PackagePrice;

                    whMaterial.TotalPrice = whMaterial.RequestQuantity * whMaterial.PackagePrice;

                    // Confirmed quantity
                    whMaterial.FormStatus = WarehouseFormStatusEnum.Executed;
                    whMaterial.ExecutionDate = DateTime.UtcNow;


                    listMaterial.Add(whMaterial);
                }
                warehouseForm.WarehouseFormMaterials = listMaterial;
                warehouseForm.TotalPrice = warehouseForm.WarehouseFormMaterials.Sum(x => x.TotalPrice);
                await _unitOfWork.WarehouseFormRepo.AddAsync(warehouseForm);
                if (await _unitOfWork.SaveChangesAsync() == 0)
                {
                    throw new APIException(HttpStatusCode.BadRequest,
                    ExceptionMessage.ENTITY_CREATE_ERROR, ExceptionMessage.ENTITY_CREATE_ERROR);
                }

                // update the form status
                await UpdateTempImportFormStatusAsync(warehouseForm.Id);
            }
            else
            {
                WarehouseForm warehouseForm = new WarehouseForm();
                warehouseForm.FormCode = "EPTWH" + StringUtils.GenerateRandomNumberString(6);
                warehouseForm.POCode = request.POCode;
                warehouseForm.FormType = WarehouseFormTypeEnum.Export;
                warehouseForm.ReceiveCompanyName = "MPMS Corporation";
                warehouseForm.CompanyAddress = "Lô E2a-7, Đường D1, Đ. D1, Long Thạnh Mỹ, Thành Phố Thủ Đức, Thành phố Hồ Chí Minh";
                warehouseForm.ReceiveWarehouse = WarehouseTypeEnum.MainWarehouse;
                warehouseForm.TotalPrice = null;
                var approveStaff = await _unitOfWork.WarehouseStaffRepo.GetWarehouseStaffByIdAsync(request.ApproveWStaffId.Value);
                if (request.RequestStaffId != null)
                {
                    var rqStaff = await _unitOfWork.PurchasingStaffRepo.GetPurchasingStaffByIdAsync(request.RequestStaffId.Value);
                    warehouseForm.RequestStaffName = rqStaff.User.FullName;
                }
                else if (request.RequestInspectorId != null)
                {
                    var rqStaff = await _unitOfWork.InspectorRepo.GetInspectorByIdAsync(request.RequestInspectorId.Value);
                    warehouseForm.RequestStaffName = rqStaff.User.FullName;
                }

                warehouseForm.ApproveWarehouseStaffName = approveStaff.User.FullName;
                warehouseForm.TempWarehouseRequestId = request.Id;
                List<WarehouseFormMaterial> listMaterial = new List<WarehouseFormMaterial>();
                foreach (var purchaseMaterial in request.DeliveryStage.PurchaseMaterials.Where(x => x.WarehouseStatus != DeliveryStageStatusEnum.SupInactive))
                {
                    WarehouseFormMaterial whMaterial = new WarehouseFormMaterial();
                    whMaterial.PurchaseMaterialId = purchaseMaterial.Id;
                    whMaterial.MaterialName = purchaseMaterial.MaterialName;
                    var rawMaterial = await _unitOfWork.RawMaterialRepo.GetByIdAsync(purchaseMaterial.RawMaterialId);
                    whMaterial.MaterialCode = rawMaterial.Code;
                    var InMaterial = InReq.InspectionForm.MaterialInspectResults.FirstOrDefault(x => x.PurchaseMaterialId == purchaseMaterial.Id);
                    whMaterial.RequestQuantity = InMaterial.InspectionPassQuantity;
                    whMaterial.MaterialPerPackage = purchaseMaterial.MaterialPerPackage;
                    whMaterial.PackagePrice = purchaseMaterial.PackagePrice;
                    whMaterial.TotalPrice = whMaterial.RequestQuantity * whMaterial.PackagePrice;
                    whMaterial.FormStatus = WarehouseFormStatusEnum.Processing;
                    listMaterial.Add(whMaterial);
                }
                warehouseForm.WarehouseFormMaterials = listMaterial;
                await _unitOfWork.WarehouseFormRepo.AddAsync(warehouseForm);
                if (await _unitOfWork.SaveChangesAsync() == 0)
                {
                    throw new APIException(HttpStatusCode.BadRequest,
                    ExceptionMessage.ENTITY_CREATE_ERROR, ExceptionMessage.ENTITY_CREATE_ERROR);
                }
            }
        }

        // Create a temporary warehouse form (It can be export or import form)
        public async Task CreateTempExportWarehouseFormByDeliveryStage(int deliveryStageId)
        {
            var deliveryStage = await _unitOfWork.DeliveryStageRepo.GetByIdWithPO(deliveryStageId);
            var listInReq = await _unitOfWork.InspectionRequestRepo.GetAllByDeliveryStageIdAsync(deliveryStage.Id);
            var InReq = listInReq.FirstOrDefault(x => x.ApproveStatus == ApproveEnum.Approved);

            if (deliveryStage == null)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                    ExceptionMessage.NOT_FOUND, ExceptionMessage.NOT_FOUND + "Delivery stage not found");
            }
            if (deliveryStage.DeliveryStatus != DeliveryStageStatusEnum.Inspected)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                    ExceptionMessage.REQUEST_NOTAPPROVE, ExceptionMessage.REQUEST_NOTAPPROVE + "Delivery stage has not been inspected, cannot create warehouse form");
            }



            WarehouseForm warehouseForm = new WarehouseForm();
            warehouseForm.DeliveryStageId = deliveryStageId;
            warehouseForm.FormCode = "EPTWH" + StringUtils.GenerateRandomNumberString(6);
            warehouseForm.POCode = deliveryStage.PurchasingOrder.POCode;
            warehouseForm.FormType = WarehouseFormTypeEnum.Export;
            warehouseForm.ReceiveCompanyName = "MPMS Corporation";
            warehouseForm.CompanyAddress = "Lô E2a-7, Đường D1, Đ. D1, Long Thạnh Mỹ, Thành Phố Thủ Đức, Thành phố Hồ Chí Minh";
            warehouseForm.ReceiveWarehouse = WarehouseTypeEnum.MainWarehouse;
            warehouseForm.TotalPrice = null;

            var warehouseStaffUserId = _claimsService.GetCurrentUserId;
            var warehouseStaff = await _unitOfWork.UserRepo.GetByIdAsync(warehouseStaffUserId);

            if (warehouseStaff != null)
            {
                var rqStaff = await _unitOfWork.WarehouseStaffRepo.GetWarehouseStaffByIdAsync(warehouseStaff.WarehouseStaffId.Value);
                warehouseForm.RequestStaffName = rqStaff.User.FullName;
            }

            List<WarehouseFormMaterial> listMaterial = new List<WarehouseFormMaterial>();
            foreach (var purchaseMaterial in deliveryStage.PurchaseMaterials.Where(x => x.WarehouseStatus != DeliveryStageStatusEnum.SupInactive))
            {
                WarehouseFormMaterial whMaterial = new WarehouseFormMaterial();
                whMaterial.PurchaseMaterialId = purchaseMaterial.Id;
                whMaterial.MaterialName = purchaseMaterial.MaterialName;
                var rawMaterial = await _unitOfWork.RawMaterialRepo.GetByIdAsync(purchaseMaterial.RawMaterialId);
                whMaterial.MaterialCode = rawMaterial.Code;
                var InMaterial = InReq.InspectionForm.MaterialInspectResults.FirstOrDefault(x => x.PurchaseMaterialId == purchaseMaterial.Id);
                whMaterial.RequestQuantity = InMaterial.InspectionPassQuantity;
                whMaterial.MaterialPerPackage = purchaseMaterial.MaterialPerPackage;
                whMaterial.PackagePrice = purchaseMaterial.PackagePrice;
                whMaterial.TotalPrice = whMaterial.RequestQuantity * whMaterial.PackagePrice;
                whMaterial.FormStatus = WarehouseFormStatusEnum.Processing;
                listMaterial.Add(whMaterial);
            }
            warehouseForm.TotalPrice = listMaterial.Sum(x => x.TotalPrice);

            warehouseForm.WarehouseFormMaterials = listMaterial;
            await _unitOfWork.WarehouseFormRepo.AddAsync(warehouseForm);
            if (await _unitOfWork.SaveChangesAsync() == 0)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                ExceptionMessage.ENTITY_CREATE_ERROR, ExceptionMessage.ENTITY_CREATE_ERROR);
            }

            await UpdateTempExportFormStatusAsync(warehouseForm.Id);
        }

        public async Task CreateImportMainWarehouseformByDeliveryStageId(int deliveryStageId)
        {
            var deliveryStage = await _unitOfWork.DeliveryStageRepo.GetByIdWithPO(deliveryStageId);

            if (deliveryStage == null)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                    ExceptionMessage.NOT_FOUND, ExceptionMessage.NOT_FOUND + "Delivery stage not found");
            }
            if (deliveryStage.DeliveryStatus != DeliveryStageStatusEnum.TempWarehouseExported)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                    ExceptionMessage.INVALID_INFORMATION, ExceptionMessage.INVALID_INFORMATION + "Delivery stage has not been exported from temp warehouse, cannot create warehouse form");
            }
            // check if this stage has existing warehouse form
            var warehouseFormList = await _unitOfWork.WarehouseFormRepo.GetAllAsync();
            var existedWarehouseForm = warehouseFormList.FirstOrDefault(x => x.DeliveryStageId == deliveryStage.Id
                && x.FormType == WarehouseFormTypeEnum.Import && x.ReceiveWarehouse == WarehouseTypeEnum.MainWarehouse);
            if (existedWarehouseForm != null)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                        ExceptionMessage.REQUESTFORM_ALREADYCREATED, ExceptionMessage.REQUESTFORM_ALREADYCREATED);
            }

            WarehouseForm warehouseForm = new WarehouseForm();
            warehouseForm.DeliveryStageId = deliveryStageId;
            warehouseForm.FormCode = "IPMWH" + StringUtils.GenerateRandomNumberString(6);
            warehouseForm.POCode = deliveryStage.PurchasingOrder.POCode;
            warehouseForm.FormType = WarehouseFormTypeEnum.Import;
            warehouseForm.ReceiveCompanyName = "MPMS Corporation";
            warehouseForm.CompanyAddress = "Lô E2a-7, Đường D1, Đ. D1, Long Thạnh Mỹ, Thành Phố Thủ Đức, Thành phố Hồ Chí Minh";
            warehouseForm.ReceiveWarehouse = WarehouseTypeEnum.MainWarehouse;

            var warehouseStaffUserId = _claimsService.GetCurrentUserId;
            var warehouseStaff = await _unitOfWork.UserRepo.GetByIdAsync(warehouseStaffUserId);

            if (warehouseStaff != null)
            {
                var rqStaff = await _unitOfWork.WarehouseStaffRepo.GetWarehouseStaffByIdAsync(warehouseStaff.WarehouseStaffId.Value);
                warehouseForm.RequestStaffName = rqStaff.User.FullName;
            }

            List<WarehouseFormMaterial> listMaterial = new List<WarehouseFormMaterial>();
            foreach (var purchaseMaterial in deliveryStage.PurchaseMaterials.Where(x => x.WarehouseStatus != DeliveryStageStatusEnum.SupInactive))
            {
                WarehouseFormMaterial whMaterial = new WarehouseFormMaterial();
                whMaterial.PurchaseMaterialId = purchaseMaterial.Id;
                whMaterial.MaterialName = purchaseMaterial.MaterialName;
                var rawMaterial = await _unitOfWork.RawMaterialRepo.GetByIdAsync(purchaseMaterial.RawMaterialId);
                whMaterial.MaterialCode = rawMaterial.Code;
                whMaterial.RequestQuantity = purchaseMaterial.AfterInspectQuantity;

                var inspectMaterial = await _unitOfWork.MaterialInspectResultRepo.GetByPmId(purchaseMaterial.Id);
                whMaterial.RequestQuantity = inspectMaterial.InspectionPassQuantity;
                whMaterial.MaterialPerPackage = purchaseMaterial.MaterialPerPackage;
                whMaterial.PackagePrice = purchaseMaterial.PackagePrice;
                whMaterial.TotalPrice = whMaterial.RequestQuantity * whMaterial.PackagePrice;
                whMaterial.FormStatus = WarehouseFormStatusEnum.Processing;
                listMaterial.Add(whMaterial);
            }

            warehouseForm.WarehouseFormMaterials = listMaterial;
            warehouseForm.TotalPrice = warehouseForm.WarehouseFormMaterials.Sum(x => x.TotalPrice);
            await _unitOfWork.WarehouseFormRepo.AddAsync(warehouseForm);
            if (await _unitOfWork.SaveChangesAsync() == 0)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                ExceptionMessage.ENTITY_CREATE_ERROR, ExceptionMessage.ENTITY_CREATE_ERROR);
            }

            await UpdateMainImportFormStatusAsync(warehouseForm.Id);
        }

        public async Task CreateImportMainWarehouseform(int requestId)
        {
            var request = await _unitOfWork.ImportMainWarehouseRequestRepo.GetByIdWithFormAsync(requestId);
            if (request == null)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                    ExceptionMessage.NOT_FOUND, ExceptionMessage.NOT_FOUND + "Request not found");
            }
            if (request.ApproveStatus != ApproveEnum.Approved)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                    ExceptionMessage.REQUEST_NOTAPPROVE, ExceptionMessage.REQUEST_NOTAPPROVE + "Request has not been approved");
            }
            if (request.WarehouseForm != null)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                    ExceptionMessage.REQUESTFORM_ALREADYCREATED, ExceptionMessage.REQUESTFORM_ALREADYCREATED);
            }
            WarehouseForm warehouseForm = new WarehouseForm();
            warehouseForm.FormCode = "IPMWH" + StringUtils.GenerateRandomNumberString(6);
            warehouseForm.POCode = request.POCode;
            warehouseForm.FormType = WarehouseFormTypeEnum.Import;
            warehouseForm.ReceiveCompanyName = "MPMS Corporation";
            warehouseForm.CompanyAddress = "Lô E2a-7, Đường D1, Đ. D1, Long Thạnh Mỹ, Thành Phố Thủ Đức, Thành phố Hồ Chí Minh";
            warehouseForm.ReceiveWarehouse = WarehouseTypeEnum.MainWarehouse;
            var rqStaff = await _unitOfWork.InspectorRepo.GetInspectorByIdAsync(request.InspectorId);
            var approveStaff = await _unitOfWork.WarehouseStaffRepo.GetWarehouseStaffByIdAsync(request.ApproveWStaffId.Value);
            var PO = await _unitOfWork.PurchasingOrderRepo.GetByPOCodeAsync(request.POCode);
            var supplier = await _unitOfWork.SupplierRepo.GetSupplierByIdAsync(PO.SupplierId);
            warehouseForm.RequestStaffName = rqStaff.User.FullName;
            warehouseForm.ApproveWarehouseStaffName = approveStaff.User.FullName;
            warehouseForm.SupplierName = supplier.User.FullName;
            warehouseForm.SupplierName = supplier.CompanyName;
            warehouseForm.ImportMainWarehouseRequestId = request.Id;
            List<WarehouseFormMaterial> listMaterial = new List<WarehouseFormMaterial>();
            foreach (var purchaseMaterial in request.DeliveryStage.PurchaseMaterials.Where(x => x.WarehouseStatus != DeliveryStageStatusEnum.SupInactive))
            {
                WarehouseFormMaterial whMaterial = new WarehouseFormMaterial();
                whMaterial.PurchaseMaterialId = purchaseMaterial.Id;
                whMaterial.MaterialName = purchaseMaterial.MaterialName;
                var rawMaterial = await _unitOfWork.RawMaterialRepo.GetByIdAsync(purchaseMaterial.RawMaterialId);
                whMaterial.MaterialCode = rawMaterial.Code;
                whMaterial.RequestQuantity = purchaseMaterial.AfterInspectQuantity;

                var inspectMaterial = await _unitOfWork.MaterialInspectResultRepo.GetByPmId(purchaseMaterial.Id);
                whMaterial.RequestQuantity = inspectMaterial.InspectionPassQuantity;
                whMaterial.MaterialPerPackage = purchaseMaterial.MaterialPerPackage;
                whMaterial.PackagePrice = purchaseMaterial.PackagePrice;
                whMaterial.TotalPrice = whMaterial.RequestQuantity * whMaterial.PackagePrice;
                whMaterial.FormStatus = WarehouseFormStatusEnum.Processing;
                listMaterial.Add(whMaterial);
            }
            warehouseForm.WarehouseFormMaterials = listMaterial;
            warehouseForm.TotalPrice = warehouseForm.WarehouseFormMaterials.Sum(x => x.TotalPrice);
            await _unitOfWork.WarehouseFormRepo.AddAsync(warehouseForm);
            if (await _unitOfWork.SaveChangesAsync() == 0)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                ExceptionMessage.ENTITY_CREATE_ERROR, ExceptionMessage.ENTITY_CREATE_ERROR);
            }
        }

        public async Task<byte[]> GenerateImportWarehouseFormExcelFile(int warehouseFormId, byte[] templateFileBytes)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


            using (var stream = new MemoryStream(templateFileBytes))
            {
                using (var output = new MemoryStream())
                {
                    using (ExcelPackage package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];

                        var warehouseForm = await _unitOfWork.WarehouseFormRepo.GetByIdWithMaterialsAsync(warehouseFormId);
                        if (warehouseForm.FormType == WarehouseFormTypeEnum.Export)
                        {
                            throw new APIException(HttpStatusCode.BadRequest,
                                ExceptionMessage.INVALID_INFORMATION, ExceptionMessage.INVALID_INFORMATION + " - Cannot create import warehouse file from export form");
                        }
                        worksheet.Cells[4, 3].Value = warehouseForm.CreatedDate;

                        worksheet.Cells[6, 3].Value = warehouseForm.SupplierName;
                        worksheet.Cells[6, 7].Value = warehouseForm.SupplierCompanyName;

                        worksheet.Cells[9, 3].Value = warehouseForm.POCode;
                        worksheet.Cells[9, 7].Value = warehouseForm.FormCode;

                        worksheet.Cells[11, 3].Value = warehouseForm.ReceiveWarehouse == WarehouseTypeEnum.TempWarehouse ?
                            "Temp warehouse" : "Main warehouse";
                        worksheet.Cells[11, 7].Value = warehouseForm.ReceiveCompanyName;

                        // Define the dropdown list values
                        var dropdownList = new string[] { "Processing", "Executed" };

                        var count = 1;
                        var totalPrice = 0.0;
                        foreach (var material in warehouseForm.WarehouseFormMaterials)
                        {
                            var purchaseMaterial = await _unitOfWork.PurchaseMaterialRepo.GetByIdAsync(material.PurchaseMaterialId);
                            var rawMaterial = await _unitOfWork.RawMaterialRepo.GetByIdAsync(purchaseMaterial.RawMaterialId);
                            worksheet.Cells[14 + count, 1].Value = count;
                            worksheet.Cells[14 + count, 2].Value = material.MaterialName;
                            worksheet.Cells[14 + count, 3].Value = material.MaterialCode;
                            worksheet.Cells[14 + count, 4].Value = Enum.GetName(typeof(PackageUnitEnum), rawMaterial.Package);
                            worksheet.Cells[14 + count, 5].Value = material.RequestQuantity;
                            worksheet.Cells[14 + count, 6].Value = Enum.GetName(typeof(RawMaterialUnitEnum), rawMaterial.Unit);
                            worksheet.Cells[14 + count, 7].Value = material.MaterialPerPackage;
                            worksheet.Cells[14 + count, 8].Value = material.PackagePrice;
                            worksheet.Cells[14 + count, 9].Value = material.TotalPrice;
                            if (material.ExecutionDate != null)
                            {
                                worksheet.Cells[14 + count, 10].Value = material.ExecutionDate.Value.ToString("dd/MM/yyyy");
                            }

                            totalPrice += material.TotalPrice.Value;

                            if (warehouseForm.WarehouseFormMaterials.Count > count) worksheet.Cells[$"A{15 + count}"].Insert(eShiftTypeInsert.EntireRow);
                            count++;
                        }

                        worksheet.Cells[14 + count, 9].Value = totalPrice;
                        worksheet.Cells[19 + count, 3].Value = warehouseForm.RequestStaffName;
                        worksheet.Cells[19 + count, 7].Value = warehouseForm.ApproveWarehouseStaffName;

                        // Convert the workbook to a byte array
                        package.SaveAs(output);
                        var content = output.ToArray();

                        return content;
                    }
                }
            }
        }

        public async Task<byte[]> GenerateExportWarehouseFormExcelFile(int warehouseFormId, byte[] templateFileBytes)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


            using (var stream = new MemoryStream(templateFileBytes))
            {
                using (var output = new MemoryStream())
                {
                    using (ExcelPackage package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];

                        var warehouseForm = await _unitOfWork.WarehouseFormRepo.GetByIdWithMaterialsAsync(warehouseFormId);
                        if (warehouseForm.FormType == WarehouseFormTypeEnum.Import)
                        {
                            throw new APIException(HttpStatusCode.BadRequest,
                                ExceptionMessage.INVALID_INFORMATION, ExceptionMessage.INVALID_INFORMATION + " - Cannot create export warehouse file from import form");
                        }
                        worksheet.Cells[4, 3].Value = warehouseForm.CreatedDate;

                        worksheet.Cells[6, 3].Value = warehouseForm.ReceiveCompanyName;
                        worksheet.Cells[6, 7].Value = warehouseForm.CompanyAddress;

                        worksheet.Cells[9, 3].Value = warehouseForm.POCode;
                        worksheet.Cells[9, 7].Value = warehouseForm.FormCode;

                        worksheet.Cells[11, 3].Value = "Temp warehouse";

                        var count = 1;
                        var totalPrice = 0.0;
                        foreach (var material in warehouseForm.WarehouseFormMaterials)
                        {
                            var purchaseMaterial = await _unitOfWork.PurchaseMaterialRepo.GetByIdAsync(material.PurchaseMaterialId);
                            var rawMaterial = await _unitOfWork.RawMaterialRepo.GetByIdAsync(purchaseMaterial.RawMaterialId);
                            worksheet.Cells[14 + count, 1].Value = count;
                            worksheet.Cells[14 + count, 2].Value = material.MaterialName;
                            worksheet.Cells[14 + count, 3].Value = material.MaterialCode;
                            worksheet.Cells[14 + count, 4].Value = Enum.GetName(typeof(PackageUnitEnum), rawMaterial.Package);
                            worksheet.Cells[14 + count, 5].Value = material.RequestQuantity;
                            worksheet.Cells[14 + count, 6].Value = Enum.GetName(typeof(RawMaterialUnitEnum), rawMaterial.Unit);
                            worksheet.Cells[14 + count, 7].Value = material.MaterialPerPackage;
                            worksheet.Cells[14 + count, 8].Value = material.PackagePrice;
                            worksheet.Cells[14 + count, 9].Value = material.TotalPrice;
                            if (material.ExecutionDate != null)
                            {
                                worksheet.Cells[14 + count, 10].Value = material.ExecutionDate.Value.ToString("dd/MM/yyyy");
                            }

                            totalPrice += material.TotalPrice.Value;
                            if (warehouseForm.WarehouseFormMaterials.Count > count) worksheet.Cells[$"A{15 + count}"].Insert(eShiftTypeInsert.EntireRow);
                            count++;
                        }

                        worksheet.Cells[14 + count, 9].Value = totalPrice;
                        worksheet.Cells[19 + count, 3].Value = warehouseForm.RequestStaffName;
                        worksheet.Cells[19 + count, 7].Value = warehouseForm.ApproveWarehouseStaffName;

                        // Convert the workbook to a byte array
                        package.SaveAs(output);
                        var content = output.ToArray();

                        return content;
                    }
                }
            }
        }

        public async Task UpdateTempImportFormStatusAsync(int formId)
        {
            var existingWarehouseForm = await _unitOfWork.WarehouseFormRepo.GetByIdWithMaterialsAsync(formId);
            if (existingWarehouseForm == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }

            foreach (var warehouseFormMaterial in existingWarehouseForm.WarehouseFormMaterials)
            {
                warehouseFormMaterial.FormStatus = WarehouseFormStatusEnum.Executed;
                warehouseFormMaterial.ExecutionDate = DateTime.Today;
            }

            // update delivery status after create form
            var tempform = await _unitOfWork.TempWarehouseRequestRepo.GetByIdWithDSAsync(existingWarehouseForm.TempWarehouseRequestId.Value);
            var dStage = await _unitOfWork.DeliveryStageRepo.GetByIdWithDetailAsync(tempform.DeliveryStageId);
            dStage.DeliveryStatus = DeliveryStageStatusEnum.TempWarehouseImported;
            foreach (var item in dStage.PurchaseMaterials.Where(x => x.WarehouseStatus != DeliveryStageStatusEnum.SupInactive))
            {
                item.WarehouseStatus = DeliveryStageStatusEnum.TempWarehouseImported;
                item.TempImportDate = DateTime.Now;
            }
            _unitOfWork.DeliveryStageRepo.Update(dStage);

            // update quantity in warehouse
            foreach (var item in existingWarehouseForm.WarehouseFormMaterials)
            {
                var purchaseMaterial = await _unitOfWork.PurchaseMaterialRepo.GetByIdAsync(item.PurchaseMaterialId);
                var tempMaterial = await _unitOfWork.WarehouseMaterialRepo.GetTempMaterial(purchaseMaterial.RawMaterialId);
                tempMaterial.Quantity = tempMaterial.Quantity + item.RequestQuantity.Value * item.MaterialPerPackage;
                tempMaterial.TotalPrice += item.RequestQuantity.Value * item.PackagePrice;
                _unitOfWork.WarehouseMaterialRepo.Update(tempMaterial);
            }

            _unitOfWork.WarehouseFormRepo.Update(existingWarehouseForm);
            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }

        public async Task UpdateMainImportFormStatusAsync(int formId)
        {
            var existingWarehouseForm = await _unitOfWork.WarehouseFormRepo.GetByIdWithMaterialsAsync(formId);
            if (existingWarehouseForm == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }

            foreach (var warehouseFormMaterial in existingWarehouseForm.WarehouseFormMaterials)
            {
                warehouseFormMaterial.FormStatus = WarehouseFormStatusEnum.Executed;
                warehouseFormMaterial.ExecutionDate = DateTime.Today;
            }

            // update delivery status after create form
            var dStage = await _unitOfWork.DeliveryStageRepo.GetByIdWithDetailAsync(existingWarehouseForm.DeliveryStageId.Value);
            dStage.DeliveryStatus = DeliveryStageStatusEnum.MainWarehouseImported;
            foreach (var item in dStage.PurchaseMaterials.Where(x => x.WarehouseStatus != DeliveryStageStatusEnum.SupInactive))
            {
                item.WarehouseStatus = DeliveryStageStatusEnum.MainWarehouseImported;
                item.MainImportDate = DateTime.Now;
            }
            var po = await _unitOfWork.PurchasingOrderRepo.GetByIdWithDetailAsync(dStage.PurchasingOrderId.Value);

            _unitOfWork.DeliveryStageRepo.Update(dStage);

            foreach (var item in existingWarehouseForm.WarehouseFormMaterials)
            {
                // update quantity in warehouse
                var purchaseMaterial = item.PurchaseMaterial;
                var mainMaterial = await _unitOfWork.WarehouseMaterialRepo.GetMainMaterial(purchaseMaterial.RawMaterialId);
                mainMaterial.Quantity = mainMaterial.Quantity + item.RequestQuantity.Value * item.MaterialPerPackage;
                mainMaterial.TotalPrice += item.RequestQuantity.Value * item.PackagePrice;

                // update quantity in purchasing task
                var purchasingPlan = await _unitOfWork.PurchasingPlanRepo.GetByIdWithDetailAsync(po.PurchasingPlanId);
                var task = purchasingPlan.PurchaseTasks.FirstOrDefault(x => x.RawMaterialId == purchaseMaterial.RawMaterialId);

                if (item.RequestQuantity.Value >= purchaseMaterial.TotalQuantity)
                {
                    task.ProcessedQuantity = task.ProcessedQuantity - purchaseMaterial.TotalQuantity * item.MaterialPerPackage;
                }
                else
                {
                    task.ProcessedQuantity = task.ProcessedQuantity - item.RequestQuantity.Value * item.MaterialPerPackage;
                }
                task.FinishedQuantity = task.FinishedQuantity + item.RequestQuantity.Value * item.MaterialPerPackage;

                // If quantity in this purchasing task is enough, set this task finished
                if (task.FinishedQuantity >= task.Quantity)
                {
                    if (task.TaskStatus != PurchasingTaskEnum.PurchasingTaskStatus.Overdue)
                    {
                        task.TaskStatus = PurchasingTaskEnum.PurchasingTaskStatus.Finished;
                    }
                }

                // If all task in this purchasing plan is completed, set this plan finished
                var check = purchasingPlan.PurchaseTasks.All(x => x.TaskStatus == PurchasingTaskEnum.PurchasingTaskStatus.Finished);
                if (check == true)
                {
                    if (purchasingPlan.ProcessStatus != ProcessStatus.Overdue)
                    {
                        purchasingPlan.ProcessStatus = ProcessStatus.Finished;
                    }
                }

                _unitOfWork.PurchasingPlanRepo.Update(purchasingPlan);
                _unitOfWork.PurchasingTaskRepo.Update(task);
                _unitOfWork.WarehouseMaterialRepo.Update(mainMaterial);
            }
            _unitOfWork.WarehouseFormRepo.Update(existingWarehouseForm);
            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);

            po = await _unitOfWork.PurchasingOrderRepo.GetByIdWithDetailAsync(dStage.PurchasingOrderId.Value);
            // check if supplemental delivery stage needed
            var subDs = po.DeliveryStages.FirstOrDefault(x => x.IsSupplemental);

            if (po.DeliveryStages.Where(x => !x.IsSupplemental).All(y =>
                    y.DeliveryStatus == DeliveryStageStatusEnum.MainWarehouseImported
                    || y.DeliveryStatus == DeliveryStageStatusEnum.Cancelled))
            {
                if (subDs.PurchaseMaterials.Any(x => x.TotalQuantity > 0) && subDs.DeliveryStatus == DeliveryStageStatusEnum.SupInactive)
                {
                    subDs.DeliveryStatus = DeliveryStageStatusEnum.Approved;

                    foreach (var item in subDs.PurchaseMaterials)
                    {
                        if (item.TotalQuantity > 0)
                        {
                            item.TotalPrice = item.TotalQuantity * item.PackagePrice;
                            item.WarehouseStatus = DeliveryStageStatusEnum.Approved;
                        }
                    }

                    subDs.TotalPrice = subDs
                        .PurchaseMaterials
                        .Where(x => x.TotalQuantity.Value > 0)
                        .Sum(x => x.TotalPrice.Value);

                    subDs.TotalTypeMaterial = subDs
                        .PurchaseMaterials
                        .Where(x => x.TotalQuantity.Value > 0)
                        .Count();
                }
            }
            _unitOfWork.DeliveryStageRepo.Update(subDs);
            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);

            // Check if purchasing order status is finished            
            po = await _unitOfWork.PurchasingOrderRepo.GetByIdWithDetailAsync(dStage.PurchasingOrderId.Value);

            if (dStage.IsSupplemental)
            {
                // If all stage including sup delivery stage are imported to main warehouse, set po to finished
                po.OrderStatus = PurchasingOrderStatusEnum.Finished;
            }
            else // If this is not the last delivery stage
            {
                var hasNoSupplementalDs = !po.DeliveryStages.Any(x => x.IsSupplemental && x.DeliveryStatus != DeliveryStageStatusEnum.SupInactive);


                // If all non-supplemental stage are imported to main warehouse, set po to finished                
                if (po.DeliveryStages.Where(x => !x.IsSupplemental).All(x =>
                    x.DeliveryStatus == DeliveryStageStatusEnum.MainWarehouseImported
                    || x.DeliveryStatus == DeliveryStageStatusEnum.Cancelled)
                    && hasNoSupplementalDs)
                {
                    po.OrderStatus = PurchasingOrderStatusEnum.Finished;
                }
            }

            _unitOfWork.PurchasingOrderRepo.Update(po);
            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);

        }

        public async Task UpdateTempExportFormStatusAsync(int formId)
        {
            var exportForm = await _unitOfWork.WarehouseFormRepo.GetByIdWithMaterialsAsync(formId);
            //var request = await _unitOfWork.TempWarehouseRequestRepo.GetByIdAsync(exportForm.TempWarehouseRequestId.Value);
            var listInReq = await _unitOfWork.InspectionRequestRepo.GetAllByDeliveryStageIdAsync(exportForm.DeliveryStageId.Value);
            var InReq = listInReq.FirstOrDefault(x => x.ApproveStatus == ApproveEnum.Approved);
            foreach (var item in exportForm.WarehouseFormMaterials)
            {
                if (item.FormStatus == WarehouseFormStatusEnum.Executed)
                {
                    throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR + "Form Already Executed");
                }
                item.FormStatus = WarehouseFormStatusEnum.Executed;
                item.ExecutionDate = DateTime.Today;
                var purchaseMaterial = await _unitOfWork.PurchaseMaterialRepo.GetByIdAsync(item.PurchaseMaterialId);
                purchaseMaterial.WarehouseStatus = DeliveryStageStatusEnum.TempWarehouseExported;
                purchaseMaterial.TempExportDate = DateTime.Now;

                var tempMaterial = await _unitOfWork.WarehouseMaterialRepo.GetTempMaterial(purchaseMaterial.RawMaterialId);
                tempMaterial.Quantity = tempMaterial.Quantity - item.RequestQuantity.Value * item.MaterialPerPackage;
                tempMaterial.TotalPrice -= item.RequestQuantity.Value * item.PackagePrice;

                var InMaterial = InReq.InspectionForm.MaterialInspectResults.FirstOrDefault(x => x.PurchaseMaterialId == purchaseMaterial.Id);
                //tempMaterial.ReturnQuantity = tempMaterial.ReturnQuantity + InMaterial.InspectionFailQuantity.Value * InMaterial.MaterialPerPackage;
                //purchaseMaterial.ReturnQuantity = purchaseMaterial.ReturnQuantity + InMaterial.InspectionFailQuantity.Value * InMaterial.MaterialPerPackage;

                _unitOfWork.WarehouseMaterialRepo.Update(tempMaterial);
                _unitOfWork.PurchaseMaterialRepo.Update(purchaseMaterial);
                _unitOfWork.WarehouseFormMaterialRepo.Update(item);
            }

            // update status in delivery stage to TempWarehouseExported
            var ds = await _unitOfWork.DeliveryStageRepo.GetByIdAsync(exportForm.DeliveryStageId.Value);
            ds.DeliveryStatus = DeliveryStageStatusEnum.TempWarehouseExported;
            _unitOfWork.DeliveryStageRepo.Update(ds);

            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }

        public async Task<List<WarehouseFormVM>> GetByPOCodeAsync(string poCode)
        {
            var itemList = await _unitOfWork.WarehouseFormRepo.GetAllAsync();
            var item = itemList.Where(x => x.POCode == poCode);
            var result = _mapper.Map<List<WarehouseFormVM>>(item);

            return result;
        }
    }

}
