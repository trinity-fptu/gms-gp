using Application.Exceptions;
using Application.IServices;
using Application.ViewModels.MaterialCategory;
using AutoMapper;
using Domain.Entities;
using System.Net;

namespace Application.Services
{
    public class MaterialCategoryService : IMaterialCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;

        public MaterialCategoryService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsService = claimsService;
        }

        public async Task CreateAsync(MaterialCategoryAddVM materialCategoryAddVM)
        {
            var createItem = _mapper.Map<MaterialCategory>(materialCategoryAddVM);
            await _unitOfWork.MaterialCategoryRepo.AddAsync(createItem);

            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest, 
                nameof(ExceptionMessage.ENTITY_CREATE_ERROR), ExceptionMessage.ENTITY_CREATE_ERROR);
        }

        public async Task DeleteAsync(int id)
        {
            var itemToDelete = await _unitOfWork.MaterialCategoryRepo.GetByIdAsync(id);
            if (itemToDelete == null)
            {
                throw new APIException(HttpStatusCode.NotFound, 
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }

            _unitOfWork.MaterialCategoryRepo.SoftRemove(itemToDelete);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest,
                nameof(ExceptionMessage.ENTITY_DELETE_ERROR), ExceptionMessage.ENTITY_DELETE_ERROR);
        }

        public async Task<List<MaterialCategoryVM>> GetAllAsync()
        {
            try
            {
                var itemList = await _unitOfWork.MaterialCategoryRepo.GetAllAsync();
                var result = _mapper.Map<List<MaterialCategoryVM>>(itemList);
                return result;
            }
            catch (Exception ex)
            {
                throw new APIException(HttpStatusCode.InternalServerError,
                    nameof(ExceptionMessage.ENTITY_RETRIEVED_ERROR), ExceptionMessage.ENTITY_RETRIEVED_ERROR);
            }
        }

        public async Task<MaterialCategoryVM> GetByIdAsync(int id)
        {

            var item = await _unitOfWork.MaterialCategoryRepo.GetByIdAsync(id);

            if (item == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            }
            var result = _mapper.Map<MaterialCategoryVM>(item);
            return result;
        }

        public async Task UpdateAsync(MaterialCategoryVM materialCategoryVM)
        {
            var existingItem = await _unitOfWork.MaterialCategoryRepo.GetByIdAsync(materialCategoryVM.Id);
            if (existingItem == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }

            _mapper.Map(materialCategoryVM, existingItem);
            _unitOfWork.MaterialCategoryRepo.Update(existingItem);

            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }
    }
}
