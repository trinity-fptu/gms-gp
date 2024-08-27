using Application.Exceptions;
using Application.IServices;
using Application.Utils;
using Application.ViewModels.SupplierAccountRequest;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.UserRole;
using Domain.Enums;
using Domain.Enums.User;
using System.Net;

namespace Application.Services
{
    public class SupplierAccountRequestService : ISupplierAccountRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;

        public SupplierAccountRequestService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsService = claimService;
        }

        public async Task ChangeApprovalStatus(int id, ApproveEnum approvalStatus)
        {
            var existedAccountRequest = await _unitOfWork.SupplierAccountRequestRepo.GetByIdAsync(id);

            if (existedAccountRequest == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }

            if (existedAccountRequest.ApproveStatus != ApproveEnum.Pending)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.REQUEST_APPROVED), ExceptionMessage.REQUEST_APPROVED);
            }

            var userList = await _unitOfWork.UserRepo.GetAllAsync();
            var existedUser = userList.FirstOrDefault(x => x.Email.ToLower().Equals(existedAccountRequest.Email.ToLower()));
            if (existedUser != null)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION+ " - User with this email is existed");
            }

            // Update the approval status
            existedAccountRequest.ApproveStatus = approvalStatus;
            _unitOfWork.SupplierAccountRequestRepo.Update(existedAccountRequest);

            // Add a new user if the request is approved
            if (approvalStatus == ApproveEnum.Approved)
            {
                var roleList = await _unitOfWork.RoleRepo.GetAllAsync();
                var supplierRole = roleList.FirstOrDefault(x => x.Name.ToLower().Equals("supplier"));

                var newUser = new User
                {
                    Email = existedAccountRequest.Email,
                    HashedPassword = existedAccountRequest.HashedPassword,
                    FullName = existedAccountRequest.FullName,
                    Address = existedAccountRequest.Address,
                    AccountStatus = UserStatusEnum.Active,
                    DOB = existedAccountRequest.DOB,
                    ProfilePictureUrl = existedAccountRequest.ProfilePictureUrl,
                    PhoneNumber = existedAccountRequest.PhoneNumber,
                    IsDeleted = false,
                    RoleId = supplierRole.Id
                };

                newUser.Supplier = new Supplier
                {
                    CompanyName = existedAccountRequest.CompanyName,
                    CompanyAddress = existedAccountRequest.CompanyAddress,
                    CompanyEmail = existedAccountRequest.CompanyEmail,
                    CompanyPhone = existedAccountRequest.CompanyPhone,
                    CompanyTaxCode = existedAccountRequest.CompanyTaxCode,
                };
                await _unitOfWork.UserRepo.AddAsync(newUser);
            }

            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }

        public async Task CreateAsync(SupplierAccountRequestAddVM supplierAccountRequestAddVM)
        {
            var purchasingStaff = await _unitOfWork.PurchasingStaffRepo.GetByIdAsync(supplierAccountRequestAddVM.RequestStaffId);
            if (purchasingStaff == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND + "Purchasing Staff");
            }

            var createdSupplierAccount = _mapper.Map<SupplierAccountRequest>(supplierAccountRequestAddVM);
            createdSupplierAccount.HashedPassword = createdSupplierAccount.HashedPassword.Hash();
            createdSupplierAccount.IsDeleted = false;
            createdSupplierAccount.ApproveStatus = ApproveEnum.Pending;
            createdSupplierAccount.RequestDate = DateTime.Now;
            await _unitOfWork.SupplierAccountRequestRepo.AddAsync(createdSupplierAccount);

            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_CREATE_ERROR), ExceptionMessage.ENTITY_CREATE_ERROR);
        }

        public async Task DeleteAsync(int id)
        {
            var itemToDelete = await _unitOfWork.SupplierAccountRequestRepo.GetByIdAsync(id);
            if (itemToDelete == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }
            if (itemToDelete.ApproveStatus == ApproveEnum.Approved)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.REQUEST_APPROVED), ExceptionMessage.REQUEST_APPROVED);
            }

            _unitOfWork.SupplierAccountRequestRepo.SoftRemove(itemToDelete);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_DELETE_ERROR), ExceptionMessage.ENTITY_DELETE_ERROR);
        }

        public async Task<List<SupplierAccountRequestVM>> GetAllAsync()
        {
            try
            {
                var supplierAccountRequest = await _unitOfWork.SupplierAccountRequestRepo.GetAllAsync();
                var result = _mapper.Map<List<SupplierAccountRequestVM>>(supplierAccountRequest);
                return result;
            }
            catch (Exception ex)
            {
                throw new APIException(HttpStatusCode.InternalServerError, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }
        }

        public async Task<SupplierAccountRequestVM> GetByIdAsync(int id)
        {
            var supplierAccountRequest = await _unitOfWork.SupplierAccountRequestRepo.GetByIdAsync(id);

            if (supplierAccountRequest == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }
            var result = _mapper.Map<SupplierAccountRequestVM>(supplierAccountRequest);
            return result;
        }

        public async Task<List<SupplierAccountRequestVM>> GetSupplierAccountRequestByPurchasingStaffId(int purchasingStaffId)
        {
            var supplierAccountRequests = await _unitOfWork.SupplierAccountRequestRepo.GetSupplierAccountRequestByPurchasingStaffId(purchasingStaffId);
            if (supplierAccountRequests == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }
            var result = _mapper.Map<List<SupplierAccountRequestVM>>(supplierAccountRequests);
            return result;
        }

        public async Task UpdateAsync(SupplierAccountRequestUpdateVM supplierAccountRequestUpdateVM)
        {
            var existingSupplierAccountRequest = await _unitOfWork.SupplierAccountRequestRepo.GetByIdAsync(supplierAccountRequestUpdateVM.Id);
            if (existingSupplierAccountRequest == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }
            if (existingSupplierAccountRequest.ApproveStatus == ApproveEnum.Approved)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.REQUEST_APPROVED), ExceptionMessage.REQUEST_APPROVED);
            }

            _mapper.Map(supplierAccountRequestUpdateVM, existingSupplierAccountRequest);
            existingSupplierAccountRequest.HashedPassword = existingSupplierAccountRequest.HashedPassword.Hash();

            _unitOfWork.SupplierAccountRequestRepo.Update(existingSupplierAccountRequest);
            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }
    }
}
