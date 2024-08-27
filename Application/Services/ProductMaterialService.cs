using Application.Exceptions;
using Application.IServices;
using Application.ViewModels.Product;
using Application.ViewModels.Product.ProductMaterial;
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
    public class ProductMaterialService : IProductMaterialService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IClaimsService _claimsService;

        public ProductMaterialService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _claimsService = claimsService;
        }

        public async Task<List<ProductMaterialVM>> GetAllAsync()
        {
            try
            {
                var productMaterials = await _unitOfWork.ProductMaterialRepo.GetAllAsync();
                var result = _mapper.Map<List<ProductMaterialVM>>(productMaterials);
                return result;
            }
            catch (Exception ex)
            {
                throw new APIException(HttpStatusCode.InternalServerError, nameof(ExceptionMessage.ENTITY_RETRIEVED_ERROR), ExceptionMessage.ENTITY_RETRIEVED_ERROR, ex);
            }
        }

        public async Task<List<ProductMaterialVM>> GetByProductIdAsync(int productId)
        {
            var item = await _unitOfWork.ProductMaterialRepo.GetByProductIdAsync(productId);

            if (item == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }

            var result = _mapper.Map<List<ProductMaterialVM>>(item);

            return result;
        }
    }
}
