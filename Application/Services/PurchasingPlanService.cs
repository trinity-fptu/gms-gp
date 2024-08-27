using Application.Exceptions;
using Application.IServices;
using Application.Utils;
using Application.ViewModels.PurchasingPlan;
using Application.ViewModels.RawMaterial;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System.Net;

namespace Application.Services
{
    public class PurchasingPlanService : IPurchasingPlanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IProductionPlanService _productionPlanService;
        private readonly IClaimsService _claimsService;

        public PurchasingPlanService(IUnitOfWork unitOfWork, IMapper mapper, IProductionPlanService productionPlanService, IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productionPlanService = productionPlanService;
            _claimsService = claimsService;
        }

        public async Task ApproveAsync(ApproveEnum approvingStatus, int id)
        {
            var itemToApprove = await _unitOfWork.PurchasingPlanRepo.GetByIdAsync(id);
            if (itemToApprove == null) throw new APIException(HttpStatusCode.NotFound,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            if (itemToApprove.ApproveStatus == ApproveEnum.Approved)
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.REQUEST_APPROVED), ExceptionMessage.REQUEST_APPROVED);


            itemToApprove.ApproveStatus = approvingStatus;

            if (approvingStatus == ApproveEnum.Approved)
            {
                itemToApprove.ProcessStatus = ProcessStatus.Processing;
            }
            else
            {
                itemToApprove.ProcessStatus = ProcessStatus.Rejected;
            }

            _unitOfWork.PurchasingPlanRepo.Update(itemToApprove);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest,
                nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }

        public async Task CreateAsync(PurchasingPlanAddVM model)
        {
            var createdItem = _mapper.Map<PurchasingPlan>(model);
            createdItem.ProcessStatus = ProcessStatus.Pending;
            // Validate information
            var existedProductionPlan = await _unitOfWork.ProductionPlanRepo.GetByIdWithDetailsAsync(createdItem.ProductionPlanId);

            var planDuration = (createdItem.EndDate.Value.Date - createdItem.StartDate.Value.Date).TotalDays;

            if (createdItem.StartDate.Value.Date < DateTime.Today)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + " - Start date must after today");
            }

            if (createdItem.EndDate.HasValue)
            {
                var planStartDate = createdItem.StartDate;
                var planEndDate = createdItem.EndDate;

                if (planStartDate.Value.Date > planEndDate.Value.Date)
                {
                    throw new APIException(HttpStatusCode.BadRequest,
                        nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + " - Start date must before end date");
                }
            }
            if (planDuration < 30)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                                       nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + " - Purchasing plan duration must be at least 30 days");
            }

            await CheckPurchasingPlanEndDateWithProductionPlanStartDateAsync(createdItem);

            var createUser = await _unitOfWork.UserRepo.GetByIdAsync(_claimsService.GetCurrentUserId);
            if (createUser != null)
                createdItem.PurchasingManagerId = createUser.PurchasingManagerId;
            await CheckDuplicateMaterialInPurchasingPlan(createdItem);

            // Check if countable raw material is count with integer, not double
            foreach (var material in createdItem.PurchaseTasks)
            {
                var rawMaterial = await _unitOfWork.RawMaterialRepo.GetByIdAsync(material.RawMaterialId);
                material.TaskStatus = PurchasingTaskEnum.PurchasingTaskStatus.Pending;
                if (rawMaterial.Unit == Domain.Enums.RawMaterialEnum.RawMaterialUnitEnum.Piece)
                {
                    if (material.Quantity != Math.Truncate(material.Quantity))
                        throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + $" - Raw material with {rawMaterial.Name} is countable, quantity must be integer");
                }
            }

            createdItem.PlanCode = $"PurP-{StringUtils.GenerateRandomNumberString(6)}";

            // Add item to database
            await InitialQuantityForPurchasingTasks(createdItem);
            await _unitOfWork.PurchasingPlanRepo.AddAsync(createdItem);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest,
                nameof(ExceptionMessage.ENTITY_CREATE_ERROR), ExceptionMessage.ENTITY_CREATE_ERROR);

        }

        public async Task DeleteAsync(int id)
        {
            var itemToDelete = await _unitOfWork.PurchasingPlanRepo.GetByIdAsync(id);
            if (itemToDelete == null)
                throw new APIException(HttpStatusCode.NotFound,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            if (itemToDelete.ApproveStatus == ApproveEnum.Approved)
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.REQUEST_APPROVED), ExceptionMessage.REQUEST_APPROVED);

            _unitOfWork.PurchasingPlanRepo.SoftRemove(itemToDelete);
            if (itemToDelete.PurchaseTasks != null)
            {
                foreach (var purchasingTask in itemToDelete.PurchaseTasks)
                {
                    _unitOfWork.PurchasingTaskRepo.SoftRemove(purchasingTask);
                }
            }
            

            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.ENTITY_DELETE_ERROR), ExceptionMessage.ENTITY_DELETE_ERROR);
        }

        public async Task<List<PurchasingPlanVM>> GetAllAuthorizeAsync()
        {
            var userRole = _claimsService.GetCurrentRoleId;
            var userId = _claimsService.GetCurrentUserId;
            var loginUser = await _unitOfWork.UserRepo.GetByIdAsync(userId);

            if (userId == -1)
            {
                throw new APIException(HttpStatusCode.Unauthorized,
                    nameof(ExceptionMessage.USER_UNAUTHORIZE), ExceptionMessage.USER_UNAUTHORIZE);
            }
            //if (userRole == 1 || userRole == 7)
            //{
            //    var items = await _unitOfWork.PurchasingPlanRepo.GetAllWithDetailAsync();
            //    var result = _mapper.Map<List<PurchasingPlanVM>>(items); 
            //    return result;
            //}
            //else if (userRole == 2)
            //{
            //    var items = await _unitOfWork.PurchasingPlanRepo.GetAllByIdPmanagerId(loginUser.PurchasingManagerId.Value);
            //    var result = _mapper.Map<List<PurchasingPlanVM>>(items);
            //    return result;
            //}

            var items = await _unitOfWork.PurchasingPlanRepo.GetAllWithDetailAsync();
            var result = _mapper.Map<List<PurchasingPlanVM>>(items);
            return result;
        }

        public async Task<List<PurchasingPlanVM>> GetAllApprovedAsync()
        {
            var items = await _unitOfWork.PurchasingPlanRepo.GetAllApprovedWithDetailAsync();
            var result = _mapper.Map<List<PurchasingPlanVM>>(items);
            return result;
        }
        public async Task<PurchasingPlanVM> GetByIdAsync(int id)
        {
            var userRole = _claimsService.GetCurrentRoleId;
            var userId = _claimsService.GetCurrentUserId;
            var item = await _unitOfWork.PurchasingPlanRepo.GetByIdWithDetailAsync(id);
            #region validation role
            //var purchasingManagerId = item != null ? item.PurchasingManagerId : null;
            //User userPmanager = purchasingManagerId.HasValue ? 
            //    await _unitOfWork.UserRepo.GetByPurchasingManagerId(item.PurchasingManagerId.Value) : null;
            //var userPmanagerId = userPmanager != null ? userPmanager.Id : -1;
            //if (userPmanagerId != userId && (userRole != 1 && userRole != 7)) {
            //    throw new APIException(HttpStatusCode.NotFound,
            //        nameof(ExceptionMessage.NOT_ALLOW), ExceptionMessage.NOT_ALLOW);
            //}
            //if (item == null)
            //    throw new APIException(HttpStatusCode.NotFound,
            //        nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            #endregion
            var result = _mapper.Map<PurchasingPlanVM>(item);
            return result;
        }

        public async Task UpdateAsync(PurchasingPlanUpdateVM model)
        {
            var itemToUpdate = await _unitOfWork.PurchasingPlanRepo.GetByIdAsync(model.Id);
            if (itemToUpdate == null) throw new APIException(HttpStatusCode.NotFound,
                nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            // Validate information
            if (model.EndDate.HasValue)
            {
                var planStartDate = model.StartDate;
                var planEndDate = model.EndDate;
                if (planStartDate > planEndDate)
                {
                    throw new APIException(HttpStatusCode.BadRequest,
                        nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + " - Start date must before end date");
                }
            }

            _mapper.Map(model, itemToUpdate);
            // await CheckDuplicateMaterialInPurchasingPlan(itemToUpdate);

            itemToUpdate.ApproveStatus = ApproveEnum.Pending;
            _unitOfWork.PurchasingPlanRepo.Update(itemToUpdate);
            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }



        private async Task CheckPurchasingPlanEndDateWithProductionPlanStartDateAsync(PurchasingPlan item)
        {
            const int ACCEPTABLE_DAYS = 30;
            var productionPlan = await _productionPlanService.GetByIdAsync(item.ProductionPlanId);

            var productionPlanStartDate = productionPlan.PlanStartDate;
            var purchasingPlanEndDate = item.EndDate.Value;
            var differenceInDays = (productionPlanStartDate - purchasingPlanEndDate).TotalDays;
            if (differenceInDays < ACCEPTABLE_DAYS)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + $" - Purchasing plan end date must be 30 days before production plan start date ({productionPlanStartDate.AddDays(-ACCEPTABLE_DAYS).ToString("dd/MM/yyyy")})");
            }
        }

        private async Task CheckDuplicateMaterialInPurchasingPlan(PurchasingPlan purchasingPlan)
        {
            var duplicateId = purchasingPlan.PurchaseTasks
                .Where(x => x.IsDeleted == false)
                .GroupBy(e => e.RawMaterialId)
                .Where(g => g.Count() > 1)
                .Select(x => x.Key);

            if (duplicateId.Count() > 0)
            {
                var idString = "";
                foreach (var materialId in duplicateId)
                {
                    idString += materialId + ", ";
                }

                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + $" - Duplicate material id {idString} in purchasing plan");
            }
        }

        private async Task InitialQuantityForPurchasingTasks(PurchasingPlan purchasingPlan)
        {
            foreach (var task in purchasingPlan.PurchaseTasks)
            {
                task.FinishedQuantity = 0;
                task.RemainedQuantity = task.Quantity;
                task.ProcessedQuantity = 0;
            }
        }

    }
}
