using Domain.Entities;

namespace Application.IRepos
{
    public interface INotificationRepo : IGenericRepo<Notification>
    {
        Task<List<Notification>> GetByUserId(int userId);
        Task<List<Notification>> GetUnreadNotificationByUserId(int userId);
        Task<List<Notification>> GetReadNotificationByUserId(int userId);
    }
}
