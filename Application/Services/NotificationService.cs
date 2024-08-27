using Application.Exceptions;
using Application.IServices;
using Application.ViewModels.Notification;
using Application.ViewModels.User;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;

        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsService = claimsService;
        }

        public async Task AddAsync(NotificationAddVM model)
        {
            var item = _mapper.Map<Notification>(model);
            await _unitOfWork.NotificationRepo.AddAsync(item);

            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_CREATE_ERROR), ExceptionMessage.ENTITY_CREATE_ERROR);
        }

        public async Task<List<NotificationVM>> GetAllAsync()
        {
            var items = await _unitOfWork.NotificationRepo.GetAllAsync();
            var result = _mapper.Map<List<NotificationVM>>(items);

            return result;

        }

        public async Task<NotificationVM> GetByIdAsync(int id)
        {
            var item = await _unitOfWork.NotificationRepo.GetByIdAsync(id);
            var result = _mapper.Map<NotificationVM>(item);

            return result;
        }

        public async Task<List<NotificationVM>> GetByUserId(int userId)
        {
            var item = await _unitOfWork.NotificationRepo.GetByUserId(userId);

            if (item == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            }
            var result = _mapper.Map<List<NotificationVM>>(item);

            return result;
        }

        public async Task<List<NotificationVM>> GetReadNotificationByUserId(int userId)
        {
            var item = await _unitOfWork.NotificationRepo.GetReadNotificationByUserId(userId);
            var result = _mapper.Map<List<NotificationVM>>(item);

            return result;
        }

        public async Task<List<NotificationVM>> GetUnreadNotificationByUserId(int userId)
        {
            var item = await _unitOfWork.NotificationRepo.GetUnreadNotificationByUserId(userId);
            var result = _mapper.Map<List<NotificationVM>>(item);

            return result;
        }

        public async Task ReadNotificationAsync(int id)
        {
            var item = await _unitOfWork.NotificationRepo.GetByIdAsync(id);

            if (item == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            }

            item.Status = NotificationStatusEnum.Read;
            _unitOfWork.NotificationRepo.Update(item);


            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }
    }
}
