using Application.Exceptions;
using Application.IServices;
using Application.ViewModels.ProductCategory;
using Application.ViewModels.WarehouseMaterial;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductCategoryService : IProductCategoryService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IClaimsService _claimsService;

        public ProductCategoryService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _claimsService = claimsService;
        }

        public async Task CreateAsync(ProductCategoryAddVM productCategoryAddVM)
        {
            var createdProductCategory = _mapper.Map<ProductCategory>(productCategoryAddVM);
            await _unitOfWork.ProductCategoryRepo.AddAsync(createdProductCategory);
            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_CREATE_ERROR), ExceptionMessage.ENTITY_CREATE_ERROR);
        }

        public async Task<ProductCategoryVM> GetByIdAsync(int id)
        {
            var productCategory = await _unitOfWork.ProductCategoryRepo.GetByIdAsync(id);

            if (productCategory == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }

            var result = _mapper.Map<ProductCategoryVM>(productCategory);
            return result;
        }

        public async Task<List<ProductCategoryVM>> GetAllAsync()
        {
            try
            {
                var productCategories = await _unitOfWork.ProductCategoryRepo.GetAllAsync();
                var result = _mapper.Map<List<ProductCategoryVM>>(productCategories);
                return result;
            }
            catch (Exception ex)
            {
                throw new APIException(HttpStatusCode.InternalServerError, nameof(ExceptionMessage.ENTITY_RETRIEVED_ERROR), ExceptionMessage.ENTITY_RETRIEVED_ERROR, ex);
            }
        }

        public async Task UpdateAsync(ProductCategoryUpdateVM productCategoryUpdateVM)
        {
            var existingProductCategory = await _unitOfWork.ProductCategoryRepo.GetByIdAsync(productCategoryUpdateVM.Id);
            if (existingProductCategory == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }

            _mapper.Map(productCategoryUpdateVM, existingProductCategory);
            _unitOfWork.ProductCategoryRepo.Update(existingProductCategory);
            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }

        public async Task DeleteAsync(int id)
        {
            var itemToDelete = await _unitOfWork.ProductCategoryRepo.GetByIdAsync(id);
            if (itemToDelete == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }

            _unitOfWork.ProductCategoryRepo.SoftRemove(itemToDelete);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_DELETE_ERROR), ExceptionMessage.ENTITY_DELETE_ERROR);
        }
    }
}
