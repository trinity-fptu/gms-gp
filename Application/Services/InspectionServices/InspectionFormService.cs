using Application.Exceptions;
using Application.IServices;
using Application.IServices.InspectionServices;
using Application.Utils;
using Application.ViewModels.DeliveryStage;
using Application.ViewModels.DeliveryStage.PurchaseMaterial;
using Application.ViewModels.InspectionForm;
using Application.ViewModels.InspectionForm.MaterialInspectResult;
using Application.ViewModels.PurchasingOrder.OrderMaterial;
using Application.ViewModels.PurchasingPlan;
using Application.ViewModels.TempWarehouse;
using Application.ViewModels.WarehouseForm;
using AutoMapper;
using DocumentFormat.OpenXml.Office2016.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Entities;
using Domain.Entities.Inspection;
using Domain.Entities.Warehousing;
using Domain.Enums;
using Domain.Enums.DeliveryStage;
using Domain.Enums.Warehousing;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.Net;
using static Domain.Enums.PurchasingTaskEnum;
using static Domain.Enums.RawMaterialEnum;

namespace Application.Services.InspectionServices
{
    public class InspectionFormService : IInspectionFormService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;
        private readonly IDeliveryStageService _deliveryStageService;
        public InspectionFormService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService, IDeliveryStageService deliveryStageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsService = claimsService;
            _deliveryStageService = deliveryStageService;
        }
        public async Task CreateAsync(InspectionFormAddVM inspectionFormVM)
        {
            var inspectionRequest = await _unitOfWork.InspectionRequestRepo.GetByIdAsync(inspectionFormVM.InspectionRequestId.Value);

            if (inspectionRequest == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND + " - Inspection request");
            }

            inspectionFormVM.POCode = inspectionRequest.POCode;

            // Calculate quantity for each inspected material 
            foreach (var materialInspectResult in inspectionFormVM.MaterialInspectResults)
            {
                var purchaseMaterial = await _unitOfWork.PurchaseMaterialRepo.GetByIdAsync(materialInspectResult.PurchaseMaterialId.Value);

                materialInspectResult.MaterialName = purchaseMaterial.MaterialName;
                materialInspectResult.MaterialCode = purchaseMaterial.CompanyMaterialCode;
                materialInspectResult.RequestQuantity = purchaseMaterial.TotalQuantity.Value;
            }

            var createdInspectedForm = _mapper.Map<InspectionForm>(inspectionFormVM);

            await _unitOfWork.InspectionFormRepo.AddAsync(createdInspectedForm);

            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_CREATE_ERROR), ExceptionMessage.ENTITY_CREATE_ERROR);
        }

        public async Task<InspectionFormVM> GetByIdAsync(int id)
        {
            var inspectionForm = await _unitOfWork.InspectionFormRepo.GetByIdWithDetailAsync(id);

            if (inspectionForm == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            }

            var result = _mapper.Map<InspectionFormVM>(inspectionForm);
            return result;
        }

        public async Task<List<InspectionFormVM>> GetAllAsync()
        {
            var warehouseForm = await _unitOfWork.InspectionFormRepo.GetAllWithDetailAsync();
            var result = _mapper.Map<List<InspectionFormVM>>(warehouseForm);
            return result;
        }

        public async Task UpdateAsync(InspectionFormUpdateVM inspectionFormVM)
        {
            var existingInspectionForm = await _unitOfWork.InspectionFormRepo.GetByIdAsync(inspectionFormVM.Id);
            if (existingInspectionForm == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }

            _mapper.Map(inspectionFormVM, existingInspectionForm);

            _unitOfWork.InspectionFormRepo.Update(existingInspectionForm);
            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }

        public async Task DeleteAsync(int id)
        {
            var itemToDelete = await _unitOfWork.InspectionFormRepo.GetByIdAsync(id);
            if (itemToDelete == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }

            _unitOfWork.InspectionFormRepo.SoftRemove(itemToDelete);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_DELETE_ERROR), ExceptionMessage.ENTITY_DELETE_ERROR);
        }

        public async Task CreateInspectionForm(int requestId)
        {
            var request = await _unitOfWork.InspectionRequestRepo.GetByIdWithInspectionFormAsync(requestId); 

            if (DateTime.Today < request.ApprovedDate.Value.Date)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                    ExceptionMessage.INVALID_INFORMATION, ExceptionMessage.INVALID_INFORMATION + " - Cannot create inspection form before the execution date");
            }
            var deliveryStage = await _unitOfWork.DeliveryStageRepo.GetByIdWithDetailAsync(request.DeliveryStageId);
            var ListTempReq = await _unitOfWork.TempWarehouseRequestRepo.GetAllByDeliveryStageIdAsync(deliveryStage.Id);
            var TempReq = ListTempReq.FirstOrDefault(x => x.ApproveStatus == ApproveEnum.Approved);
            if (request == null)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                    ExceptionMessage.NOT_FOUND, ExceptionMessage.NOT_FOUND + "Request not found");
            }
            if (request.ApproveStatus != ApproveEnum.Approved)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                    ExceptionMessage.REQUEST_NOTAPPROVE, ExceptionMessage.REQUEST_NOTAPPROVE);
            }

            if (request.InspectionForm != null)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                    ExceptionMessage.REQUESTFORM_ALREADYCREATED, ExceptionMessage.REQUESTFORM_ALREADYCREATED);
            }

            var currentUserId = _claimsService.GetCurrentUserId;
            var currentUser = await _unitOfWork.UserRepo.GetByIdAsync(currentUserId);
            if (currentUser == null || request.ApprovingInspectorId != currentUser.InspectorId)
            {
                throw new APIException(HttpStatusCode.Unauthorized,
                    ExceptionMessage.NOT_ALLOW, ExceptionMessage.NOT_ALLOW);
            }
            var createInspectorUser = await _unitOfWork.UserRepo.GetByIdAsync(_claimsService.GetCurrentUserId);

            if (createInspectorUser == null || createInspectorUser.InspectorId == null)
            {
                throw new APIException(HttpStatusCode.Unauthorized,
                                       ExceptionMessage.NOT_ALLOW, ExceptionMessage.NOT_ALLOW);
            }

            var inspectionForm = new InspectionForm
            {
                InspectionRequestId = requestId,
                ResultCode = "IF" + StringUtils.GenerateRandomNumberString(6),
                POCode = request.POCode,
                InspectLocation = "Temp warehouse",
                InspectorName = createInspectorUser.FullName,
                CreatedDate = DateTime.Now,
                MaterialInspectResults = new List<MaterialInspectResult>()
            };

            foreach (var purchaseMaterial in request.DeliveryStage.PurchaseMaterials.Where(x => x.WarehouseStatus != DeliveryStageStatusEnum.SupInactive))
            {
                var tempMaterial = TempReq.WarehouseForm.WarehouseFormMaterials.FirstOrDefault(x => x.PurchaseMaterialId == purchaseMaterial.Id);
                var materialInspectResult = new MaterialInspectResult
                {
                    RequestQuantity = purchaseMaterial.DeliveredQuantity.Value,
                    PurchaseMaterialId = purchaseMaterial.Id,
                    MaterialName = purchaseMaterial.MaterialName,
                    MaterialCode = purchaseMaterial.CompanyMaterialCode,
                    MaterialPerPackage = purchaseMaterial.MaterialPerPackage,
                    CreatedDate = DateTime.Now,
                    InspectStatus = MaterialInspectResultEnum.Pending,
                };

                inspectionForm.MaterialInspectResults.Add(materialInspectResult);
            }


            await _unitOfWork.InspectionFormRepo.AddAsync(inspectionForm);
            if (await _unitOfWork.SaveChangesAsync() == 0)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                ExceptionMessage.ENTITY_CREATE_ERROR, ExceptionMessage.ENTITY_CREATE_ERROR);
            }
        }

        public async Task UpdateInspectionFormStatusAsync(InspectionFormUpdateStatusVM inspectionFormDTO)
        {
            var existingInspectionForm = await _unitOfWork.InspectionFormRepo.GetByIdWithDetailAsync(inspectionFormDTO.Id);
            var inspectionRequest = await _unitOfWork.InspectionRequestRepo.GetByIdAsync(existingInspectionForm.InspectionRequestId);
            var deliveryStage = await _unitOfWork.DeliveryStageRepo.GetByIdWithDetailAsync(inspectionRequest.DeliveryStageId);  
            foreach (var item in existingInspectionForm.MaterialInspectResults)
            {
                var materialInspectResultDTO = inspectionFormDTO.MaterialInspectResults.FirstOrDefault(x => x.Id == item.Id);
                var sumcheck = materialInspectResultDTO.InspectionPassQuantity + materialInspectResultDTO.InspectionFailQuantity;
                if (sumcheck != item.RequestQuantity)
                {
                    throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + " - Passed Quantity wasn't equal to Receive Quantity");
                }
            }
            if (existingInspectionForm == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }

            if (existingInspectionForm.MaterialInspectResults.All(x => x.InspectStatus == MaterialInspectResultEnum.Inspected))
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR + " - Inspection form has been confirm, cannot update");
            }

            var currentUserId = _claimsService.GetCurrentUserId;
            var currentUser = await _unitOfWork.UserRepo.GetByIdAsync(currentUserId);
            if (currentUser == null || inspectionRequest.ApprovingInspectorId != currentUser.InspectorId)
            {
                throw new APIException(HttpStatusCode.Unauthorized,
                    ExceptionMessage.NOT_ALLOW, ExceptionMessage.NOT_ALLOW);
            }

            // Set inspection quantity to MaterialInspectResults
            inspectionRequest = await _unitOfWork.InspectionRequestRepo.GetByIdAsync(existingInspectionForm.InspectionRequestId);
            deliveryStage = await _unitOfWork.DeliveryStageRepo.GetByIdWithDetailAsync(inspectionRequest.DeliveryStageId);
            foreach (var materialInspectResult in existingInspectionForm.MaterialInspectResults)
            {
                foreach (var material in inspectionFormDTO.MaterialInspectResults)
                {
                    
                    if (materialInspectResult.Id == material.Id)
                    {
                        if (deliveryStage.IsSupplemental && material.InspectionPassQuantity < materialInspectResult.PurchaseMaterial.TotalQuantity)
                        {
                            throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + $" - Inspection pass quantity for raw material {materialInspectResult.MaterialName} must be equal to request quantity in supplemental delivery stage");
                        }
                        
                        materialInspectResult.InspectStatus = MaterialInspectResultEnum.Inspected;
                        materialInspectResult.InspectionPassQuantity = material.InspectionPassQuantity;
                        materialInspectResult.InspectionFailQuantity = material.InspectionFailQuantity;
                        materialInspectResult.Note = material.Note;
                    }
                }
            }

            // update delivery status after create form
            inspectionRequest = await _unitOfWork.InspectionRequestRepo.GetByIdAsync(existingInspectionForm.InspectionRequestId);
            deliveryStage = await _unitOfWork.DeliveryStageRepo.GetByIdWithDetailAsync(inspectionRequest.DeliveryStageId);
            deliveryStage.DeliveryStatus = DeliveryStageStatusEnum.Inspected;
            foreach (var item in deliveryStage.PurchaseMaterials.Where(x => x.WarehouseStatus != DeliveryStageStatusEnum.SupInactive))
            {
                item.WarehouseStatus = DeliveryStageStatusEnum.Inspected;

                var inspectedMaterial = existingInspectionForm.MaterialInspectResults.FirstOrDefault(x => x.PurchaseMaterialId == item.Id);
                item.AfterInspectQuantity = inspectedMaterial.InspectionPassQuantity;
                item.PlanInspectDate = DateTime.Today;
            }

            // if inspected has failed quantity, if this is last delivery stage, set supplemental delivery stage to approved
            var PO = await _unitOfWork.PurchasingOrderRepo.GetByIdWithDetailAsync(deliveryStage.PurchasingOrderId.Value);
            var subDs = PO.DeliveryStages.FirstOrDefault(x => x.IsSupplemental == true);
                        
            // Add failed quantity to supplemental delivery stage
            foreach (var materialInspectResult in existingInspectionForm.MaterialInspectResults)
            {
                var pm = await _unitOfWork.PurchaseMaterialRepo.GetByIdAsync(materialInspectResult.PurchaseMaterialId);
                foreach (var item in subDs.PurchaseMaterials)
                {
                    if (item.RawMaterialId == pm.RawMaterialId)
                    {
                        item.TotalQuantity = item.TotalQuantity + materialInspectResult.InspectionFailQuantity;
                        item.TotalPrice = item.TotalQuantity * item.PackagePrice;
                        if (item.TotalQuantity > 0) item.WarehouseStatus = DeliveryStageStatusEnum.Approved;
                    }
                }
            }

            // Add failed quantity to return quantity 
            foreach (var materialInspectResult in existingInspectionForm.MaterialInspectResults)
            {
                var pm = await _unitOfWork.PurchaseMaterialRepo.GetByIdAsync(materialInspectResult.PurchaseMaterialId);
                var tempMaterial = await _unitOfWork.WarehouseMaterialRepo.GetTempMaterial(pm.RawMaterialId);
                
                if (tempMaterial != null)
                {
                    tempMaterial.ReturnQuantity += materialInspectResult.InspectionFailQuantity * materialInspectResult.MaterialPerPackage;
                    pm.ReturnQuantity = materialInspectResult.InspectionFailQuantity * materialInspectResult.MaterialPerPackage;
                    _unitOfWork.WarehouseMaterialRepo.Update(tempMaterial);
                    _unitOfWork.PurchaseMaterialRepo.Update(pm);
                }
            }
            _unitOfWork.PurchasingOrderRepo.Update(PO);
            _unitOfWork.InspectionFormRepo.Update(existingInspectionForm);
            _unitOfWork.DeliveryStageRepo.Update(subDs);
            _unitOfWork.DeliveryStageRepo.Update(deliveryStage);

            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }

        public async Task<byte[]> GenerateInspectionFormExcelFile(int inspectionFormId, byte[] templateFileBytes)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


            using (var stream = new MemoryStream(templateFileBytes))
            {
                using (var output = new MemoryStream())
                {
                    using (ExcelPackage package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];

                        var inspectionForm = await _unitOfWork.InspectionFormRepo.GetByIdWithDetailAsync(inspectionFormId);

                        worksheet.Cells[4, 3].Value = inspectionForm.CreatedDate;

                        worksheet.Cells[6, 3].Value = inspectionForm.POCode;
                        worksheet.Cells[6, 7].Value = inspectionForm.ResultCode;

                        worksheet.Cells[8, 3].Value = inspectionForm.InspectionRequest.RequestInspectDate;

                        worksheet.Cells[9, 3].Value = inspectionForm.InspectLocation;

                        worksheet.Cells[11, 3].Value = inspectionForm.ResultNote;

                        // Define the dropdown list values
                        var dropdownList = new string[] { "Processing", "Executed" };

                        var count = 1;
                        foreach (var material in inspectionForm.MaterialInspectResults)
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
                            worksheet.Cells[14 + count, 8].Value = material.InspectionPassQuantity;
                            worksheet.Cells[14 + count, 9].Value = material.InspectionFailQuantity;
                            worksheet.Cells[14 + count, 10].Value = material.Note;

                            if (inspectionForm.MaterialInspectResults.Count > count) worksheet.Cells[$"A{15 + count}"].Insert(eShiftTypeInsert.EntireRow);
                            count++;
                        }

                        worksheet.Cells[17 + count, 8].Value = inspectionForm.InspectorName;

                        // Convert the workbook to a byte array
                        package.SaveAs(output);
                        var content = output.ToArray();

                        return content;
                    }
                }
            }
        }

        public async Task<List<InspectionFormVM>> GetByPOCodeAsync(string poCode)
        {
            var itemList = await _unitOfWork.InspectionFormRepo.GetAllAsync();
            var item = itemList.Where(x => x.POCode == poCode);
            var result = _mapper.Map<List<InspectionFormVM>>(item);

            return result;
        }
    }
}
