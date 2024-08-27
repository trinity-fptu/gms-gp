using Application.Exceptions;
using Application.IServices;
using Application.ViewModels.Role;
using AutoMapper;
using Domain.Entities;
using System.Net;

namespace Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsService = claimsService;
        }

        public async Task CreateAsync(RoleAddVM roleAddVM)
        {
            var createdRole = _mapper.Map<Role>(roleAddVM);
            await _unitOfWork.RoleRepo.AddAsync(createdRole);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_CREATE_ERROR), ExceptionMessage.ENTITY_CREATE_ERROR);
        }

        public async Task<RoleVM> GetByIdAsync(int id)
        {
            var role = await _unitOfWork.RoleRepo.GetByIdAsync(id);

            if (role == null)
            {
               throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }
            var result = _mapper.Map<RoleVM>(role); 
            return result;
        }

        public async Task<List<RoleVM>> GetAllAsync()
        {
            try
            {
                var role = await _unitOfWork.RoleRepo.GetAllAsync();
                var result = _mapper.Map<List<RoleVM>>(role);
                return result;
            }
            catch (Exception ex)
            {
                throw new APIException(HttpStatusCode.InternalServerError, nameof(ExceptionMessage.ENTITY_RETRIEVED_ERROR), ExceptionMessage.ENTITY_RETRIEVED_ERROR, ex);
            }
        }

        public async Task UpdateAsync(RoleVM roleVM)
        {
            var existingRole = await _unitOfWork.RoleRepo.GetByIdAsync(roleVM.Id);
            if (existingRole == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }

            var updatedRole = _mapper.Map<Role>(roleVM);
            _unitOfWork.RoleRepo.Update(updatedRole);

            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_CREATE_ERROR), ExceptionMessage.ENTITY_CREATE_ERROR);

        }
    }
}
