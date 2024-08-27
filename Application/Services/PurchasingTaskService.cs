using Application.Exceptions;
using Application.IServices;
using Application.ViewModels.PurchasingTask;
using AutoMapper;
using Domain.Enums;
using System.Net;
using static Domain.Enums.PurchasingTaskEnum;

namespace Application.Services
{
    public class PurchasingTaskService : IPurchasingTaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;

        public PurchasingTaskService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsService = claimsService;
        }

        public async Task<PurchasingTaskVM> GetByIdAsync(int id)
        {
            var item = await _unitOfWork.PurchasingTaskRepo.GetByIdWithDetailAsync(id);
            if (item == null)
                throw new APIException(HttpStatusCode.NotFound,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            var result = _mapper.Map<PurchasingTaskVM>(item);
            return result;
        }

        public async Task<List<PurchasingTaskVM>> GetPurchasingTaskByPurchasingPlanId(int purchasingPlanId)
        {
            var purchasingTasks = await _unitOfWork.PurchasingTaskRepo.GetPurchasingTaskByPurchasingPlanId(purchasingPlanId);
            if (purchasingTasks == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }
            var result = _mapper.Map<List<PurchasingTaskVM>>(purchasingTasks);
            return result;
        }

        public async Task<List<PurchasingTaskVM>> GetPurchasingTaskByPurchasingStaffId(int purchasingStaffId)
        {
            var purchasingTasks = await _unitOfWork.PurchasingTaskRepo.GetPurchasingTaskByPurchasingStaffId(purchasingStaffId);
            if (purchasingTasks == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }
            var result = _mapper.Map<List<PurchasingTaskVM>>(purchasingTasks);
            return result;
        }

        public async Task<List<PurchasingTaskVM>> GetAllAsync()
        {
            var items = await _unitOfWork.PurchasingTaskRepo.GetAllAsync();
            var result = _mapper.Map<List<PurchasingTaskVM>>(items);
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            var itemToDelete = await _unitOfWork.PurchasingTaskRepo.GetByIdWithDetailAsync(id);

            _unitOfWork.PurchasingTaskRepo.SoftRemove(itemToDelete);

            if (itemToDelete == null)
                throw new APIException(HttpStatusCode.NotFound,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            if (itemToDelete.PurchasingPlan.ApproveStatus == Domain.Enums.ApproveEnum.Approved)
                throw new APIException(HttpStatusCode.BadRequest,
                                       nameof(ExceptionMessage.REQUEST_APPROVED), ExceptionMessage.REQUEST_APPROVED);

            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.ENTITY_DELETE_ERROR), ExceptionMessage.ENTITY_DELETE_ERROR);
        }

        public async Task AssignAsync(PurchasingTaskAssignVM updateTask)
        {
            var existingTask = await _unitOfWork.PurchasingTaskRepo.GetByIdAsync(updateTask.Id);
            if (existingTask == null) throw new APIException(HttpStatusCode.NotFound,
                nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            var existingStaff = await _unitOfWork.PurchasingStaffRepo.GetByIdAsync(updateTask.PurchasingStaffId);
            if (existingStaff == null) throw new APIException(HttpStatusCode.NotFound,
                nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            if (existingTask.TaskStatus != null && existingTask.TaskStatus != PurchasingTaskStatus.Pending)
                throw new APIException(HttpStatusCode.BadRequest,
                                       nameof(ExceptionMessage.TASK_ASSIGNED), ExceptionMessage.TASK_ASSIGNED);

            var purchasingPlan = await _unitOfWork.PurchasingPlanRepo.GetByIdAsync(existingTask.PurchasingPlanId);

            if (purchasingPlan.StartDate > updateTask.TaskStartDate)
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + "Task start date must after Plan start date");
            if (purchasingPlan.EndDate < updateTask.TaskEndDate)
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + "Task end date must before Plan end date");
            updateTask.RemainedQuantity = existingTask.Quantity;

            _mapper.Map(updateTask, existingTask);
            existingTask.TaskStatus = PurchasingTaskStatus.Assigned;

            _unitOfWork.PurchasingTaskRepo.Update(existingTask);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest,
                nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }
    }
}