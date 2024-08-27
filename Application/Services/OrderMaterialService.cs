using Application.Exceptions;
using Application.IServices;
using Application.ViewModels.PurchasingOrder.OrderMaterial;
using AutoMapper;
using Domain.Enums;
using System.Net;

namespace Application.Services
{
    public class OrderMaterialService : IOrderMaterialService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;
        public OrderMaterialService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsService = claimsService;
        }

        public async Task<OrderMaterialVM> GetByIdAsync(int id)
        {
            var item = await _unitOfWork.OrderMaterialRepo.GetByIdAsync(id);
            if (item == null)
                throw new APIException(HttpStatusCode.NotFound,
                                       nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            var result = _mapper.Map<OrderMaterialVM>(item);
            return result;
        }

        public async Task<List<OrderMaterialVM>> GetOrderMaterialByPOId(int purchasingOrderId)
        {
            var orderMaterials = await _unitOfWork.OrderMaterialRepo.GetOrderMaterialByPOId(purchasingOrderId);
            if (orderMaterials == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }
            var result = _mapper.Map<List<OrderMaterialVM>>(orderMaterials);
            return result;
        }

        public async Task<List<OrderMaterialVM>> GetAllAsync()
        {
            var items = await _unitOfWork.OrderMaterialRepo.GetAllAsync();
            var result = _mapper.Map<List<OrderMaterialVM>>(items);
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            var itemToDelete = await _unitOfWork.OrderMaterialRepo.GetByIdAsync(id);

            if (itemToDelete == null)
                throw new APIException(HttpStatusCode.NotFound,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            var purchaseOrder = await _unitOfWork.PurchasingOrderRepo.GetByIdWithDetailAsync(itemToDelete.PurchasingOrderId);
            if (purchaseOrder.ManagerApproveStatus == ApproveEnum.Approved && purchaseOrder.SupplierApproveStatus == ApproveEnum.Approved)
                throw new APIException(HttpStatusCode.BadRequest,
                                       nameof(ExceptionMessage.REQUEST_APPROVED), ExceptionMessage.REQUEST_APPROVED);

            _unitOfWork.OrderMaterialRepo.SoftRemove(itemToDelete);


            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.ENTITY_DELETE_ERROR), ExceptionMessage.ENTITY_DELETE_ERROR);
        }
    }
}
