

using Application.ViewModels.Notification;
using Domain.Entities;

namespace Application.IServices
{
    public interface INotificationService
    {
        Task<List<NotificationVM>> GetByUserId(int userId);
        Task<List<NotificationVM>> GetAllAsync();
        Task<List<NotificationVM>> GetUnreadNotificationByUserId(int userId);
        Task<List<NotificationVM>> GetReadNotificationByUserId(int userId);
        Task AddAsync(NotificationAddVM model);
        Task<NotificationVM> GetByIdAsync(int id);
        Task ReadNotificationAsync(int id);
        
    }
}
