
using Application.Exceptions;
using Application.IServices.WarehousingServices;
using Application.ViewModels.MainWarehouse;
using Application.ViewModels.WarehouseFormMaterial;
using AutoMapper;
using Domain.Enums;
using Domain.Enums.Warehousing;
using System.Net;

namespace Application.Services.WarehousingServices
{
    public class WarehouseFormMaterialService : IWarehouseFormMaterialService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;

        public WarehouseFormMaterialService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsService = claimsService;
        }

        public async Task<List<WarehouseFormMaterialVM>> GetAllByWarehouseFormIdAsync(int warehouseFormId)
        {
            var itemList = await _unitOfWork.WarehouseFormMaterialRepo.GetAllByWarehouseFormIdAsync(warehouseFormId);
            var result = _mapper.Map<List<WarehouseFormMaterialVM>>(itemList);

            return result;
        }
        
        public async Task UpdateStatus(int warehouseFormMaterialId, WarehouseFormStatusEnum status)
        {
            var item = await _unitOfWork.WarehouseFormMaterialRepo.GetByIdAsync(warehouseFormMaterialId);
            if (item == null) throw new APIException(HttpStatusCode.NotFound,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            if (item.FormStatus == WarehouseFormStatusEnum.Executed)
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.REQUEST_APPROVED), ExceptionMessage.REQUEST_APPROVED);


            item.FormStatus = status;
            _unitOfWork.WarehouseFormMaterialRepo.Update(item);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest,
                nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }
    }
}
