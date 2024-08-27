using Application.Exceptions;
using Application.IServices;
using Application.Utils;
using Application.ViewModels.User;
using Application.ViewModels.User.Supplier;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.UserRole;
using Domain.Enums.User;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IClaimsService _claimsService;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _claimsService = claimsService;
        }

        public async Task DeleteAsync(int id)
        {
            var deleteItem = await _unitOfWork.UserRepo.GetByIdAsync(id);
            if (deleteItem == null) throw new APIException(HttpStatusCode.NotFound,
                nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            _unitOfWork.UserRepo.SoftRemove(deleteItem);

            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest,
                ExceptionMessage.ENTITY_DELETE_ERROR, ExceptionMessage.ENTITY_DELETE_ERROR);
        }

        public async Task BanUserAsync(int id)
        {
            var banItem = await _unitOfWork.UserRepo.GetByIdAsync(id);
            if (banItem == null) throw new APIException(HttpStatusCode.NotFound,
                nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            banItem.AccountStatus = UserStatusEnum.Inactive;

            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.NotFound,
                nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }

        public async Task UnbanUserAsync(int id)
        {
            var banItem = await _unitOfWork.UserRepo.GetByIdAsync(id);
            if (banItem == null) throw new APIException(HttpStatusCode.NotFound,
                nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            banItem.AccountStatus = UserStatusEnum.Active;
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.NotFound,
                nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }

        public async Task<List<UserVM>> GetAllAsync()
        {
            var itemList = await _unitOfWork.UserRepo.GetAllAsync();
            var itemListVM = _mapper.Map<List<UserVM>>(itemList);
            return itemListVM;
        }

        public async Task<UserVM> GetByIdAsync(int id)
        {
            var item = await _unitOfWork.UserRepo.GetByIdAsync(id);
            if (item == null) throw new APIException(HttpStatusCode.NotFound,
                nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            var itemVM = _mapper.Map<UserVM>(item);
            return itemVM;
        }

        public async Task<string> LoginAsync(UserLoginVM model)
        {
            var user = await _unitOfWork.UserRepo.Login(model.Email, model.HashedPassword.Hash());
            //var user = await _unitOfWork.UserRepo.Login(model.Email, model.HashedPassword);

            // Check condition
            if (user != null && user.IsDeleted == true) throw new APIException(HttpStatusCode.NotFound,
                nameof(ExceptionMessage.USER_NOT_FOUND), ExceptionMessage.USER_NOT_FOUND);
            if (user != null && user.AccountStatus == UserStatusEnum.Inactive) throw new APIException(HttpStatusCode.NotFound,
                nameof(ExceptionMessage.USER_NOT_FOUND), ExceptionMessage.USER_NOT_FOUND);
            if (user != null && user.AccountStatus == UserStatusEnum.IsBlocked) throw new APIException(HttpStatusCode.NotFound,
                nameof(ExceptionMessage.USER_NOT_FOUND), ExceptionMessage.USER_NOT_FOUND);

            if (user == null) throw new APIException(HttpStatusCode.NotFound,
                nameof(ExceptionMessage.USER_LOGIN_ERROR), ExceptionMessage.USER_LOGIN_ERROR);

            // Generate token after condition is passed
            var token = user.GenerateJsonWebToken(_configuration["Jwt:Key"]!, _configuration);
            return token;
        }

        public async Task UpdateAsync(UserUpdateVM model)
        {
            // Find existed entity
            var updateItem = await _unitOfWork.UserRepo.GetByIdAsync(model.Id);
            if (updateItem == null) throw new APIException(HttpStatusCode.NotFound,
                nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            // Map data from model to entity
            _mapper.Map(model, updateItem);
            _unitOfWork.UserRepo.Update(updateItem);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.NotFound,
                nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }

        public async Task UpdateAsync(UserSupplierUpdateVM model)
        {
            // Find existed entity
            var updateItem = await _unitOfWork.UserRepo.GetByIdAsync(model.Id);
            if (updateItem == null) throw new APIException(HttpStatusCode.NotFound,
                nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            // Map data from model to entity
            _mapper.Map(model, updateItem);
            _unitOfWork.UserRepo.Update(updateItem);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest,
                nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }

        public async Task CreateAsync(UserAddVM model)
        {
            var createdItem = _mapper.Map<User>(model);

            var role = await _unitOfWork.RoleRepo.GetByIdAsync(model.RoleId.Value);
            if (role == null) throw new APIException(HttpStatusCode.NotFound,
                nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            switch (role.Name)
            {
                case "Manager":
                    createdItem.Manager = new Manager();
                    break;
                case "Purchasing Manager":
                    createdItem.PurchasingManager = new PurchasingManager();
                    break;
                case "Purchasing Staff":
                    createdItem.PurchasingStaff = new PurchasingStaff();
                    break;
                case "Warehouse Staff":
                    createdItem.WarehouseStaff = new WarehouseStaff();
                    break;
                case "Inspector":
                    createdItem.Inspector = new Inspector();
                    break;
            }

            createdItem.StaffCode = await GenerateUserCode(createdItem);
            createdItem.HashedPassword = model.HashedPassword.Hash();

            await _unitOfWork.UserRepo.AddAsync(createdItem);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.NotFound,
                nameof(ExceptionMessage.ENTITY_CREATE_ERROR), ExceptionMessage.ENTITY_CREATE_ERROR);
        }

        public async Task CreateAsync(UserSupplierAddVM model)
        {
            var createdItem = _mapper.Map<User>(model);
            createdItem.HashedPassword = model.HashedPassword.Hash();

            await _unitOfWork.UserRepo.AddAsync(createdItem);
            var role = await _unitOfWork.RoleRepo.GetAllAsync();
            createdItem.RoleId = role.FirstOrDefault(x => x.Name == "Supplier")?.Id;
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest,
                nameof(ExceptionMessage.ENTITY_CREATE_ERROR), ExceptionMessage.ENTITY_CREATE_ERROR);
        }

        public async Task<UserVM> GetBySupplierId(int supplierId)
        {
            var item = await _unitOfWork.UserRepo.GetBySupplierId(supplierId);
            if (item == null) throw new APIException(HttpStatusCode.NotFound,
                nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            var itemVM = _mapper.Map<UserVM>(item);
            return itemVM;
        }

        public async Task<UserVM> GetByManagerId(int managerId)
        {
            var item = await _unitOfWork.UserRepo.GetByManagerId(managerId);
            if (item == null) throw new APIException(HttpStatusCode.NotFound,
                nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            var itemVM = _mapper.Map<UserVM>(item);
            return itemVM;
        }

        public async Task<UserVM> GetByPurchasingManagerId(int purchasingManagerId)
        {
            var item = await _unitOfWork.UserRepo.GetByPurchasingManagerId(purchasingManagerId);
            if (item == null) throw new APIException(HttpStatusCode.NotFound,
                nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            var itemVM = _mapper.Map<UserVM>(item);
            return itemVM;
        }

        public async Task<UserVM> GetByPurchasingStaffId(int purchasingStaffId)
        {
            var item = await _unitOfWork.UserRepo.GetByPurchasingStaffId(purchasingStaffId);
            if (item == null) throw new APIException(HttpStatusCode.NotFound,
                nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            var itemVM = _mapper.Map<UserVM>(item);
            return itemVM;
        }

        public async Task<UserVM> GetByWarehouseStaffId(int warehouseStaffId)
        {
            var item = await _unitOfWork.UserRepo.GetByWarehouseStaffId(warehouseStaffId);
            if (item == null) throw new APIException(HttpStatusCode.NotFound,
                nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            var itemVM = _mapper.Map<UserVM>(item);
            return itemVM;
        }

        public async Task<UserVM> GetByInspectorId(int inspectorId)
        {
            var item = await _unitOfWork.UserRepo.GetByInspectorId(inspectorId);
            if (item == null) throw new APIException(HttpStatusCode.NotFound,
                nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            var itemVM = _mapper.Map<UserVM>(item);
            return itemVM;
        }

        public async Task<List<UserVM>> GetAllByRoleAsync(int roleId)
        {
            var itemList = await _unitOfWork.UserRepo.GetAllByRoleId(roleId);
            var itemListVM = _mapper.Map<List<UserVM>>(itemList);

            return itemListVM;
        }

        public async Task<UserVM> GetByStaffCodeAsync(string code)
        {
            var item = _unitOfWork.UserRepo.GetByStaffCodeAsync(code);
            if (item == null) throw new APIException(HttpStatusCode.NotFound,
                nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            var itemVM = _mapper.Map<UserVM>(item);
            return itemVM;

        }

        private async Task<String> GenerateUserCode(User user)
        {
            var userList = await _unitOfWork.UserRepo.GetAllAsync();
            string userCode = null;

            if (user.Manager != null) userCode = $"M{StringUtils.GenerateRandomNumberString(6)}";
            if (user.PurchasingManager != null) userCode = $"PM{StringUtils.GenerateRandomNumberString(6)}";
            if (user.PurchasingStaff != null) userCode = $"PS{StringUtils.GenerateRandomNumberString(6)}";
            if (user.WarehouseStaff != null) userCode = $"WS{StringUtils.GenerateRandomNumberString(6)}";
            if (user.Inspector != null) userCode = $"I{StringUtils.GenerateRandomNumberString(6)}";
            return userCode;
        }
    }
}
