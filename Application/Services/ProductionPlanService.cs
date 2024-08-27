using Application.Exceptions;
using Application.IServices;
using Application.Utils;
using Application.ViewModels.ProductionPlan;
using Application.ViewModels.RawMaterial;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.Globalization;
using System.Net;

namespace Application.Services
{
    public class ProductionPlanService : IProductionPlanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;

        public ProductionPlanService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsService = claimsService;
        }

        public async Task<ProductionPlanVM> GetByIdAsync(int id)
        {
            var item = await _unitOfWork.ProductionPlanRepo.GetByIdWithDetailsAsync(id);

            if (item == null)
            {
                throw new APIException(HttpStatusCode.NotFound,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            }
            var result = _mapper.Map<ProductionPlanVM>(item);
            return result;
        }

        public async Task CreateAsync(ProductionPlanAddVM productionPlanAddVM)
        {
            var createItem = _mapper.Map<ProductionPlan>(productionPlanAddVM);

            createItem.ManagerId = _claimsService.GetCurrentUserId != -1 ? _claimsService.GetCurrentUserId : null;
            await ValidateData(createItem);

            // Validate that coutable material in production plan must have integer quantity
            foreach (var material in createItem.ExpectedMaterials)
            {
                var rawMaterial = await _unitOfWork.RawMaterialRepo.GetByIdAsync(material.RawMaterialId);
                if (rawMaterial.Unit == Domain.Enums.RawMaterialEnum.RawMaterialUnitEnum.Piece)
                {
                    if (material.RequireQuantity != Math.Truncate(material.RequireQuantity))
                        throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + $" - Quantity of material {rawMaterial.Name} must be integer");
                }
            }

            createItem.ProductionPlanCode = $"PrdP-{StringUtils.GenerateRandomNumberString(6)}";
            // save item to db
            await _unitOfWork.ProductionPlanRepo.AddAsync(createItem);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest,
                nameof(ExceptionMessage.ENTITY_CREATE_ERROR), ExceptionMessage.ENTITY_CREATE_ERROR);
        }

        public async Task<ProductionPlanVM> ImportProductionPlanFile(IFormFile formFile)
        {
            if (formFile == null || formFile.Length <= 0) throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.FILE_EMPTY), ExceptionMessage.FILE_EMPTY);

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase)) throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.FILE_EMPTY), ExceptionMessage.FILE_EMPTY);
            ProductionPlan importPlanDTO = new ProductionPlan();
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream);
                ExcelPackage.LicenseContext = LicenseContext.Commercial;

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    List<ProductInPlan> productInPlans = new List<ProductInPlan>();
                    List<ExpectedMaterial> PlanMaterials = new List<ExpectedMaterial>();
                    try
                    {

                        importPlanDTO.Name = worksheet.Cells[2, 1].Value.ToString().Trim();
                        importPlanDTO.PlanStartDate = DateTime.ParseExact(worksheet.Cells[5, 2].Value.ToString().Trim()
                            , "d/M/yyyy", CultureInfo.InvariantCulture);
                        importPlanDTO.PlanEndDate = DateTime.ParseExact(worksheet.Cells[5, 5].Value.ToString().Trim()
                            , "d/M/yyyy", CultureInfo.InvariantCulture);
                        importPlanDTO.Note = worksheet.Cells[7, 2] is null ? worksheet.Cells[7, 2].Value.ToString().Trim() : "";
                    }
                    catch
                    {
                        throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.FILE_INVALID_INPUT), ExceptionMessage.FILE_INVALID_INPUT);
                    }
                    importPlanDTO.CreatedDate = DateTime.Now;
                    // get list user in excel file
                    for (int row = 10; row <= rowCount; row++)
                    {
                        if (worksheet.Cells[row, 1].Value is null)
                        {
                            break;
                        }

                        var productCode = worksheet.Cells[row, 2].Value.ToString().Trim();
                        var product = await _unitOfWork.ProductRepo.GetByCodeAsync(productCode);

                        if (product == null)
                        {
                            throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.INVALID_INFORMATION), $"{ExceptionMessage.INVALID_INFORMATION} - Product code {productCode} not found");
                        }

                        var productQuantity = double.Parse(worksheet.Cells[row, 4].Value.ToString().Trim());
                        ProductInPlan productInPlan = new ProductInPlan()
                        {
                            ProductId = product.Id,
                            Quantity = productQuantity,
                        };
                        productInPlans.Add(productInPlan);
                    }
                    importPlanDTO.ProductInPlans = productInPlans;
                }
            }
            await ValidateData(importPlanDTO);

            return _mapper.Map<ProductionPlanVM>(importPlanDTO);
            //await _unitOfWork.ProductionPlanRepo.AddAsync(importPlanDTO);
            //await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<ProductionPlanVM>> GetAllAsync()
        {
            try
            {
                var items = await _unitOfWork.ProductionPlanRepo.GetAllWithDetailsAsync();
                var result = _mapper.Map<List<ProductionPlanVM>>(items);
                return result;
            }
            catch (Exception ex)
            {
                throw new APIException(HttpStatusCode.InternalServerError,
                    nameof(ExceptionMessage.ENTITY_RETRIEVED_ERROR), ExceptionMessage.ENTITY_RETRIEVED_ERROR, ex);
            }
        }

        public async Task<List<ProductionPlanVM>> GetAllPlanWithoutPurchasingPlanAsync()
        {
            try
            {
                var items = await _unitOfWork.ProductionPlanRepo.GetAllWithoutPurchasingPlanAsync();
                var result = _mapper.Map<List<ProductionPlanVM>>(items);
                return result;
            }
            catch (Exception ex)
            {
                throw new APIException(HttpStatusCode.InternalServerError,
                    nameof(ExceptionMessage.ENTITY_RETRIEVED_ERROR), ExceptionMessage.ENTITY_RETRIEVED_ERROR, ex);
            }
        }
        public async Task DeleteAsync(int id)
        {
            var itemToDelete = await _unitOfWork.ProductionPlanRepo.GetByIdAsync(id);
            if (itemToDelete == null) throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            _unitOfWork.ProductionPlanRepo.SoftRemove(itemToDelete);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_DELETE_ERROR), ExceptionMessage.ENTITY_DELETE_ERROR);
        }

        private async Task ValidateData(ProductionPlan productionPlan)
        {
            if (productionPlan.PlanStartDate > productionPlan.PlanEndDate)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + $"Production plan start date must after end date");
            }

            const int ACCEPTABLE_DAYS = 90;

            var productionPlanStartDate = productionPlan.PlanStartDate;
            var today = DateTime.Today;
            var differenceInDays = (productionPlanStartDate - today).TotalDays;
            if (differenceInDays < ACCEPTABLE_DAYS)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + $" - production plan start date must be 90 days after this current day ({today.AddDays(ACCEPTABLE_DAYS).ToString("dd/MM/yyyy")})");
            }
            // Validate that product in production plan must have integer quantity
            foreach (var product in productionPlan.ProductInPlans)
            {
                if (product.Quantity != Math.Truncate(product.Quantity))
                {
                    var productEntity = await _unitOfWork.ProductRepo.GetByIdAsync(product.ProductId);
                    throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + $" - Quantity of product {productEntity.Name} must be integer");
                }
            }

            // Validate duration
            var productionPlanDuration = (productionPlan.PlanEndDate - productionPlan.PlanStartDate).Value.TotalDays;
            if (productionPlanDuration < 80 || productionPlanDuration > 100)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + $" - Production plan duration must between 80 and 100 days");
            }
        }
    }
}
