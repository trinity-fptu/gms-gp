using Application.Exceptions;
using Application.IServices;
using Application.ViewModels.DeliveryStage.PurchaseMaterial;
using Application.ViewModels.PurchasingOrder.OrderMaterial;
using AutoMapper;
using Domain.Enums;
using Domain.Enums.Warehousing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PurchaseMaterialService : IPurchaseMaterialService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;

        public PurchaseMaterialService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsService = claimsService;
        }

        public Task ChangeWarehouseStatusAsync(int id, WarehouseStatusEnum status)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            var itemToDelete = await _unitOfWork.PurchaseMaterialRepo.GetByIdAsync(id);

            if (itemToDelete == null)
                throw new APIException(HttpStatusCode.NotFound,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            _unitOfWork.PurchaseMaterialRepo.SoftRemove(itemToDelete);

            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.ENTITY_DELETE_ERROR), ExceptionMessage.ENTITY_DELETE_ERROR);
        }

        public async Task<List<PurchaseMaterialVM>> GetAllAsync()
        {
            var items = await _unitOfWork.PurchaseMaterialRepo.GetAllAsync();
            var result = _mapper.Map<List<PurchaseMaterialVM>>(items);
            return result;
        }

        public async Task<PurchaseMaterialVM> GetByIdAsync(int id)
        {
            var item = await _unitOfWork.PurchaseMaterialRepo.GetByIdAsync(id);
            if (item == null)
                throw new APIException(HttpStatusCode.NotFound,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            var result = _mapper.Map<PurchaseMaterialVM>(item);
            return result;
        }
    }
}
