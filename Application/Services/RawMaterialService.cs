using Application.Exceptions;
using Application.IServices;
using Application.Utils;
using Application.ViewModels.RawMaterial;
using AutoMapper;
using Domain.Entities;
using Domain.Enums.Warehousing;
using System.Net;

namespace Application.Services
{
    public class RawMaterialService : IRawMaterialService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;

        public RawMaterialService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsService = claimsService;
        }

        public async Task CreateAsync(RawMaterialAddVM rawMaterialAddVM)
        {
            var createdRawMaterial = _mapper.Map<RawMaterial>(rawMaterialAddVM);
            createdRawMaterial.Code = await GenerateMaterialCode();
            
            var tempWarehouse = await _unitOfWork.WarehouseRepo.GetByTypeAsync(WarehouseTypeEnum.TempWarehouse);  
            var mainWarehouse = await _unitOfWork.WarehouseRepo.GetByTypeAsync(WarehouseTypeEnum.MainWarehouse);

            createdRawMaterial.WarehouseMaterials = new List<WarehouseMaterial>();
            createdRawMaterial.WarehouseMaterials.Add(new WarehouseMaterial
            {
                WarehouseId = tempWarehouse.Id,
                Quantity = 0
            });
            createdRawMaterial.WarehouseMaterials.Add(new WarehouseMaterial
            {
                WarehouseId = mainWarehouse.Id,
                Quantity = 0
            });
            await _unitOfWork.RawMaterialRepo.AddAsync(createdRawMaterial);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(
                HttpStatusCode.BadRequest, 
                nameof(ExceptionMessage.ENTITY_CREATE_ERROR), 
                ExceptionMessage.ENTITY_CREATE_ERROR);
        }

        public async Task<RawMaterialVM> GetByIdAsync(int id)
        {
            var rawMaterial = await _unitOfWork.RawMaterialRepo.GetByIdAsync(id);

            if (rawMaterial == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION);

            }
            var result = _mapper.Map<RawMaterialVM>(rawMaterial);
            return result;
        }

        public async Task<List<RawMaterialVM>> GetRawMaterialByMaterialCategoryId(int materialCategoryId)
        {
            var rawMaterials = await _unitOfWork.RawMaterialRepo.GetRawMaterialByMaterialCategoryId(materialCategoryId);
            if (rawMaterials == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }
            var result = _mapper.Map<List<RawMaterialVM>>(rawMaterials);
            return result;
        }

        public async Task<List<RawMaterialVM>> GetAllAsync()
        {
            try
            {
                var rawMaterial = await _unitOfWork.RawMaterialRepo.GetAllAsync();
                var result = _mapper.Map<List<RawMaterialVM>>(rawMaterial);
                return result;
            }
            catch (Exception ex)
            {
                throw new APIException(HttpStatusCode.InternalServerError, "ENTITY_RETRIEVE", "Error when retrieving raw materials", ex);
            }
        }

        public async Task UpdateAsync(RawMaterialVM rawMaterialVM)
        {
            var existingRawMaterial = await _unitOfWork.RawMaterialRepo.GetByIdAsync(rawMaterialVM.Id);
            if (existingRawMaterial == null)
            {
                throw new APIException(HttpStatusCode.NotFound, "ENTITY_NOTFOUND", "Error when finding raw material with given id");
            }

            _mapper.Map(rawMaterialVM, existingRawMaterial);
            _unitOfWork.RawMaterialRepo.Update(existingRawMaterial);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest, "ENTITY_CREATE", "Error when update raw material");
        }

        public async Task DeleteAsync(int id)
        {
            var itemToDelete = await _unitOfWork.RawMaterialRepo.GetByIdAsync(id);
            if (itemToDelete == null)
            {
                throw new APIException(HttpStatusCode.NotFound, "ENTITY_NOTFOUND", "Error when finding raw material with given id");
            }
            
            _unitOfWork.RawMaterialRepo.SoftRemove(itemToDelete);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest, "ENTITY_DELETE", "Error when deleting raw material");
        }


        private async Task<String> GenerateMaterialCode()
        {
            string materialCode = $"M{StringUtils.GenerateRandomNumberString(6)}";

            return materialCode;
        }
    }
}
