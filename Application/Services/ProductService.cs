using Application.Exceptions;
using Application.IServices;
using Application.Utils;
using Application.ViewModels.Product;
using Application.ViewModels.PurchasingOrder;
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
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IClaimsService _claimsService;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _claimsService = claimsService;
        }

        public async Task AddAsync(ProductAddVM product)
        {

            var createItem = _mapper.Map<Product>(product);
            await CheckDuplicateMaterialInProduct(createItem);
            createItem.Code = await GenerateProductCode();

            await _unitOfWork.ProductRepo.AddAsync(createItem);
            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_CREATE_ERROR), ExceptionMessage.ENTITY_CREATE_ERROR);
        }

        public async Task DeleteAsync(int id)
        {
            var itemToDelete = await _unitOfWork.ProductRepo.GetByIdAsync(id);
            if (itemToDelete == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), $"{ExceptionMessage.NOT_FOUND} - Product");
            }

            _unitOfWork.ProductRepo.SoftRemove(itemToDelete);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_DELETE_ERROR), $"{ExceptionMessage.ENTITY_DELETE_ERROR} - Product");
        }

        public async Task<List<ProductVM>> GetAllAsync()
        {
            try
            {
                var productCategories = await _unitOfWork.ProductRepo.GetAllWithDetailAsync();

                var result = _mapper.Map<List<ProductVM>>(productCategories);
                return result;
            }
            catch (Exception ex)
            {
                throw new APIException(HttpStatusCode.InternalServerError, nameof(ExceptionMessage.ENTITY_RETRIEVED_ERROR), ExceptionMessage.ENTITY_RETRIEVED_ERROR, ex);
            }
        }

        public async Task<ProductVM> GetByCodeAsync(string code)
        {
            var item = await _unitOfWork.ProductRepo.GetByCodeAsync(code);

            if (item == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }

            var result = _mapper.Map<ProductVM>(item);
            return result;
        }

        public async Task<ProductVM> GetByIdAsync(int id)
        {
            var item = await _unitOfWork.ProductRepo.GetByIdWithDetailAsync(id);

            if (item == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }

            var result = _mapper.Map<ProductVM>(item);
            return result;
        }

        public async Task UpdateAsync(ProductUpdateVM product)
        {
            var existingItem = await _unitOfWork.ProductRepo.GetByIdAsync(product.Id);
            if (existingItem == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }

            _mapper.Map(product, existingItem);
            await CheckDuplicateMaterialInProduct(existingItem);
            _unitOfWork.ProductRepo.Update(existingItem);
            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }


        private async Task CheckDuplicateMaterialInProduct(Product product)
        {
            var duplicateId = product.ProductMaterials
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
                    nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + $" - Duplicate material id {idString} in product");
            }
        }


        private async Task<String> GenerateProductCode()
        {
            string productCode = $"P{StringUtils.GenerateRandomNumberString(6)}";
            return productCode;
        }
    }
}
