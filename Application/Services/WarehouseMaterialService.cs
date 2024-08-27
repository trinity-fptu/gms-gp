using Application.Exceptions;
using Application.IServices;
using Application.ViewModels.Warehouse;
using Application.ViewModels.WarehouseMaterial;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Warehousing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class WarehouseMaterialService : IWarehouseMaterialService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;

        public WarehouseMaterialService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsService = claimsService;
        }
        public async Task CreateAsync(WarehouseMaterialAddVM warehouseMaterialAddVM)
        {
            var createdWarehouseMaterial = _mapper.Map<WarehouseMaterial>(warehouseMaterialAddVM);
            await _unitOfWork.WarehouseMaterialRepo.AddAsync(createdWarehouseMaterial);
            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_CREATE_ERROR), ExceptionMessage.ENTITY_CREATE_ERROR);
        }

        public async Task<WarehouseMaterialVM> GetByIdAsync(int id)
        {
            var warehouseMaterial = await _unitOfWork.WarehouseMaterialRepo.GetByIdWithRawMaterialAsync(id);

            if (warehouseMaterial == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }

            //if (warehouseMaterial.RawMaterial != null && warehouseMaterial.RawMaterial.EstimatePrice.HasValue)
            //{
            //    warehouseMaterial.TotalPrice = warehouseMaterial.Quantity * warehouseMaterial.RawMaterial.EstimatePrice.Value;
            //}

            //await _unitOfWork.SaveChangesAsync();

            var result = _mapper.Map<WarehouseMaterialVM>(warehouseMaterial);
            return result;
        }

        public async Task<List<WarehouseMaterialVM>> GetAllAsync()
        {
            try
            {
                var warehouseMaterials = await _unitOfWork.WarehouseMaterialRepo.GetAllWithRawMaterialsAsync();
                //var result = _mapper.Map<List<WarehouseMaterialVM>>(warehouseMaterials);
                //foreach (var warehouseMaterial in warehouseMaterials)
                //{
                //    if (warehouseMaterial.RawMaterial != null && warehouseMaterial.RawMaterial.EstimatePrice.HasValue)
                //    {
                //        warehouseMaterial.TotalPrice = warehouseMaterial.Quantity * warehouseMaterial.RawMaterial.EstimatePrice.Value;
                //    }
                //}
                //await _unitOfWork.SaveChangesAsync();

                var result = _mapper.Map<List<WarehouseMaterialVM>>(warehouseMaterials);
                return result;
            }
            catch (Exception ex)
            {
                throw new APIException(HttpStatusCode.InternalServerError, nameof(ExceptionMessage.ENTITY_RETRIEVED_ERROR), ExceptionMessage.ENTITY_RETRIEVED_ERROR, ex);
            }
        }

        public async Task UpdateAsync(WarehouseMaterialUpdateVM warehouseMaterialUpdateVM)
        {
            var existingWarehouseMaterial = await _unitOfWork.WarehouseMaterialRepo.GetByIdAsync(warehouseMaterialUpdateVM.Id);
            if (existingWarehouseMaterial == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }

            _mapper.Map(warehouseMaterialUpdateVM, existingWarehouseMaterial);
            _unitOfWork.WarehouseMaterialRepo.Update(existingWarehouseMaterial);
            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }

        public async Task DeleteAsync(int id)
        {
            var itemToDelete = await _unitOfWork.WarehouseMaterialRepo.GetByIdAsync(id);
            if (itemToDelete == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }

            _unitOfWork.WarehouseMaterialRepo.SoftRemove(itemToDelete);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_DELETE_ERROR), ExceptionMessage.ENTITY_DELETE_ERROR);
        }

        public async Task<List<WarehouseMaterialVM>> GetByWarehouseIdAsync(int warehouseId)
        {
            var warehouseMaterials = await _unitOfWork.WarehouseMaterialRepo.GetWarehouseMaterialsByWarehouseIdAsync(warehouseId);
            if (warehouseMaterials == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }

            //foreach (var warehouseMaterial in warehouseMaterials)
            //{
            //    if (warehouseMaterial.RawMaterial != null && warehouseMaterial.RawMaterial.EstimatePrice.HasValue)
            //    {
            //        warehouseMaterial.TotalPrice = warehouseMaterial.Quantity * warehouseMaterial.RawMaterial.EstimatePrice.Value;
            //    }
            //}
            //await _unitOfWork.SaveChangesAsync();

            var result = _mapper.Map<List<WarehouseMaterialVM>>(warehouseMaterials);
            return result;
        }
    }
}
