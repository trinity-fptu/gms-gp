using Application.Exceptions;
using Application.IServices;
using Application.ViewModels.Warehouse;
using Application.ViewModels.WarehouseMaterial;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Warehousing;
using Domain.Enums.Warehousing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;

        public WarehouseService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsService = claimsService;
        }

        public async Task CreateAsync(WarehouseAddVM warehouseAddVM)
        {
            var createdWarehouse = _mapper.Map<Warehouse>(warehouseAddVM);
            await _unitOfWork.WarehouseRepo.AddAsync(createdWarehouse);
            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_CREATE_ERROR), ExceptionMessage.ENTITY_CREATE_ERROR);
        }

        public async Task<WarehouseVM> GetByIdAsync(int id)
        {
            var warehouse = await _unitOfWork.WarehouseRepo.GetByIdWithWarehouseMaterialsAsync(id);

            if (warehouse == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }

            //foreach (var warehouseMaterial in warehouse.WarehouseMaterials)
            //{
            //    if (warehouseMaterial.RawMaterial != null && warehouseMaterial.RawMaterial.EstimatePrice.HasValue)
            //    {
            //        warehouseMaterial.TotalPrice = warehouseMaterial.Quantity * warehouseMaterial.RawMaterial.EstimatePrice.Value;
            //    }
            //}

            //await _unitOfWork.SaveChangesAsync();

            var result = _mapper.Map<WarehouseVM>(warehouse);
            return result;
        }

        public async Task<List<WarehouseVM>> GetAllAsync()
        {
            try
            {
                var warehouses = await _unitOfWork.WarehouseRepo.GetAllWithWarehouseMaterialAsync();
                //var result = _mapper.Map<List<WarehouseVM>>(warehouse);
                //foreach (var warehouse in warehouses)
                //{
                //    foreach (var warehouseMaterial in warehouse.WarehouseMaterials)
                //    {
                //        if (warehouseMaterial.RawMaterial != null && warehouseMaterial.RawMaterial.EstimatePrice.HasValue)
                //        {
                //            warehouseMaterial.TotalPrice = warehouseMaterial.Quantity * warehouseMaterial.RawMaterial.EstimatePrice.Value;
                //        }
                //    }
                //}

                //await _unitOfWork.SaveChangesAsync();

                var result = _mapper.Map<List<WarehouseVM>>(warehouses);
                return result;
            }
            catch (Exception ex)
            {
                throw new APIException(HttpStatusCode.InternalServerError, nameof(ExceptionMessage.ENTITY_RETRIEVED_ERROR), ExceptionMessage.ENTITY_RETRIEVED_ERROR, ex);
            }
        }

        public async Task UpdateAsync(WarehouseUpdateVM warehouseUpdateVM)
        {
            var existingWarehouse = await _unitOfWork.WarehouseRepo.GetByIdAsync(warehouseUpdateVM.Id);
            if (existingWarehouse == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }
            var updatedWarehouse = _mapper.Map<Warehouse>(warehouseUpdateVM);
            _unitOfWork.WarehouseRepo.Update(updatedWarehouse);
            _mapper.Map(warehouseUpdateVM, existingWarehouse);
            _unitOfWork.WarehouseRepo.Update(existingWarehouse);
            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }

        public async Task DeleteAsync(int id)
        {
            var itemToDelete = await _unitOfWork.WarehouseRepo.GetByIdAsync(id);
            if (itemToDelete == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }

            _unitOfWork.WarehouseRepo.SoftRemove(itemToDelete);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_DELETE_ERROR), ExceptionMessage.ENTITY_DELETE_ERROR);
        }

        public async Task UpdateMissingWarehouseMaterial()
        {
            var warehouseList = await _unitOfWork.WarehouseRepo.GetAllAsync();
            var tempWarehouse = warehouseList.FirstOrDefault(x => x.WarehouseType == Domain.Enums.Warehousing.WarehouseTypeEnum.TempWarehouse);
            var mainWarehouse = warehouseList.FirstOrDefault(x => x.WarehouseType == Domain.Enums.Warehousing.WarehouseTypeEnum.MainWarehouse);


            var warehouseMaterialList = await _unitOfWork.WarehouseMaterialRepo.GetAllAsync();
            var tempMaterialList = warehouseMaterialList.Where(x => x.WarehouseId == tempWarehouse.Id);
            var mainMaterialList = warehouseMaterialList.Where(x => x.WarehouseId == mainWarehouse.Id);
            var rawMaterialList = await _unitOfWork.RawMaterialRepo.GetAllAsync();

            foreach (var rawMaterial in rawMaterialList)
            {
                if (!tempMaterialList.Any(x => x.RawMaterialId == rawMaterial.Id))
                {
                    var warehouseMaterial = new WarehouseMaterial
                    {
                        RawMaterialId = rawMaterial.Id,
                        Quantity = 0,
                        WarehouseId = tempWarehouse.Id
                    };
                    await _unitOfWork.WarehouseMaterialRepo.AddAsync(warehouseMaterial);
                }
                if (!mainMaterialList.Any(x => x.RawMaterialId == rawMaterial.Id))
                {
                    var warehouseMaterial = new WarehouseMaterial
                    {
                        RawMaterialId = rawMaterial.Id,
                        Quantity = 0,
                        WarehouseId = mainWarehouse.Id
                    };
                    await _unitOfWork.WarehouseMaterialRepo.AddAsync(warehouseMaterial);
                }

                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<List<TempWarehouseMaterialVM>> GetPurchaseMaterialListInTempWarehouse(int warehouseId, int rawMaterialId)
        {
            var warehouse = await _unitOfWork.WarehouseRepo.GetByIdWithWarehouseMaterialsAsync(warehouseId);
            if (warehouse == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND + " - Warehouse");
            }

            if (warehouse.WarehouseType != WarehouseTypeEnum.TempWarehouse)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + " - Not a temp warehouse");
            }

            var purchaseMaterialList = await _unitOfWork.PurchaseMaterialRepo.GetPurchaseMaterialListInTempWarehouse(rawMaterialId);

            var result = purchaseMaterialList.Select(x => new TempWarehouseMaterialVM
            {
                RawMaterialId = x.RawMaterialId,
                RawMaterialName = x.MaterialName,
                StageOrder = x.DeliveryStage.StageOrder,
                Quantity = (x.WarehouseStatus == Domain.Enums.DeliveryStage.DeliveryStageStatusEnum.TempWarehouseExported 
                || x.WarehouseStatus == Domain.Enums.DeliveryStage.DeliveryStageStatusEnum.MainWarehouseImported) ? 0 : x.DeliveredQuantity * x.MaterialPerPackage,
                ReturnQuantity = x.ReturnQuantity * x.MaterialPerPackage,
                PMCode = x.Code,
                POCode = x.DeliveryStage.PurchasingOrder.POCode,
                Unit = x.Unit
            }).ToList();

            return result;
        }

        public async Task<List<MainWarehouseMaterialVM>> GetPurchaseMaterialListInMainWarehouse(int warehouseId, int rawMaterialId)
        {
            var warehouse = await _unitOfWork.WarehouseRepo.GetByIdWithWarehouseMaterialsAsync(warehouseId);
            if (warehouse == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND + " - Warehouse");
            }

            if (warehouse.WarehouseType != WarehouseTypeEnum.MainWarehouse)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + " - Not a mains warehouse");
            }

            var purchaseMaterialList = await _unitOfWork.PurchaseMaterialRepo.GetPurchaseMaterialListInMainWarehouse(rawMaterialId);

            var result = purchaseMaterialList.Select(x => new MainWarehouseMaterialVM
            {
                RawMaterialId = x.RawMaterialId,
                RawMaterialName = x.MaterialName,
                StageOrder = x.DeliveryStage.StageOrder,
                Quantity = x.AfterInspectQuantity * x.MaterialPerPackage,
                PMCode = x.Code,
                POCode = x.DeliveryStage.PurchasingOrder.POCode,
                Unit = x.Unit
            }).ToList();

            return result;
        }
    }
}
